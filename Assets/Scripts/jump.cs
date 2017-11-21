using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour
{
	float y = 0.0f;
	public float gravity = 0.0f;     // 중력느낌용
	int direction = 0;       // 0:정지상태, 1:점프중, 2:다운중
	public int jumpStatus = 2;
	public bool isGround = false;
	// 설정값
	const float jump_speed = 0.26f;  // 점프속도
	const float jump_accell = 0.01f; // 점프가속
	const float y_base = -2.17f;      // 캐릭터가 서있는 기준점

	void Start()
	{
		y = y_base;
		Application.targetFrameRate = 30;
	}

	void Update()
	{
		JumpProcess();

		Vector3 pos = gameObject.transform.position;
		pos.y = y;
		gameObject.transform.position = pos;

		if (isGround == false && jumpStatus == 2 && gravity == jump_speed)
		{
			direction = 1;
			gravity -= 0.1f;
		}

	}




	public void DoJump() // 점프키 누를때 1회만 호출
	{
		if (isGround)
		{

			if (jumpStatus > 0)
			{

				direction = 1;
				gravity = jump_speed;
				jumpStatus--;


				if (jumpStatus == 0)
					isGround = false;

			}
		}
	}

	void JumpProcess()
	{
		switch (direction)
		{
			case 0: // 2단 점프시 처리
				{
					if (y > y_base)
					{
						if (y >= jump_accell)
						{
							//                        y -= jump_accell;
							y -= gravity;
						}
						else
						{
							y = y_base;
						}
					}

					break;
				}

			case 1: // up
				{
					y += gravity;

					if (gravity <= 0.0f)
					{
						direction = 2;
					}
					else
					{
						gravity -= jump_accell;
					}

					break;
				}


			case 2: // down
				{
					y -= gravity;

					if (y > y_base)
					{
						gravity += jump_accell;
					}
					else
					{
						direction = 0;
						y = y_base;
					}

					break;
				}

		}


	}

	void OnCollisionStay2D(Collision2D c)
	{

		if (c.transform.tag == "Ground")
		{

			jumpStatus = 2;
			isGround = true;

			if (c.transform.tag != "Ground")
			{

				isGround = false;
				//Destroy (c.gameObject);

			}

		}

	}

}