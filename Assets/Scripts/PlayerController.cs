using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	public Ball ball;

	private enum JumpState : int
	{
		Idle,
		Up,
		Down
	}

	private Vector2 moveVelocity;
	private Rigidbody2D myRigidbody;

	public float jumpPower;
	private JumpState jumpState;

	private void Awake()
	{
		myRigidbody = GetComponent<Rigidbody2D>();

		ball.gameObject.SetActive(false);
	}

	public void StateReset()
	{
		ball.gameObject.SetActive(true);
		ball.StateReset();
	}

	public void Move(Vector2 _velocity)
	{
		myRigidbody.MovePosition(myRigidbody.position + _velocity * Time.fixedDeltaTime);
	}

	public void Shoot()
	{
		//check valid ball.
		if (ball == null)
			return;

		float ball_distance = Vector2.Distance(transform.position, ball.transform.position);

		if (ball_distance > 1)
			return;

		//check ball position.
		Vector2 direction = (ball.transform.position - transform.position);
		direction.y += 10;

		//shoot
		ball.Shoot(direction.normalized * 10);
	}

	public void Jump(float start_y)
	{
		if (jumpState != JumpState.Idle)
			return;
		
		jumpState = JumpState.Up;
		StartCoroutine(JumpProcess(start_y));
	}

	private IEnumerator JumpProcess(float start_y)
	{
		float yValue = start_y;
		float power = jumpPower;
		WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
		while (true)
		{
			if (jumpState == JumpState.Idle)
				break;

			if (jumpState == JumpState.Up)
			{
				yValue += power;

				if (power <= 0)
				{
					power = 0;
					jumpState = JumpState.Down;
				}
				else
				{
					power -= Time.fixedDeltaTime;
				}
			}
			else if (jumpState == JumpState.Down)
			{
				yValue -= power;

				if (power >= jumpPower)
				{
					yValue = start_y;
					jumpState = JumpState.Idle;
				}
				else
				{
					power += Time.fixedDeltaTime;
				}
			}

			Vector3 velocity = myRigidbody.position;
			velocity.y = yValue;
			myRigidbody.position = velocity;

			yield return fixedUpdate;
		}
	}
}