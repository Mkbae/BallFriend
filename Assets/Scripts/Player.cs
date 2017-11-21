using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
	private static Player _instance;
	public static Player Instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType(typeof(Player)) as Player;
			return _instance;
		}
	}
	
	public float imageOffset = 0.1f;

	public float moveSpeed = 5;

	public PlayerController controller { get; private set; }

	private void Awake()
	{
		controller = GetComponent<PlayerController>();
		transform.position = Vector2.one * (-Camera.main.orthographicSize + imageOffset);
	}

	private void Update()
	{
		//Move.
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
		{
			//input값. 키보드 방향키. GetAxisRaw -> not smoothing (키입력 해제시 바로 동작 정지)
			Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
			//input 값 방향벡터로 정규화.
			Vector2 moveVelocity = moveInput.normalized * moveSpeed;
			controller.Move(moveVelocity);
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			controller.Move(Vector2.zero);
		}

		//Jump.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			controller.Jump(transform.position.y);
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Ball"))
		{
			controller.Shoot();
		}
	}
}
