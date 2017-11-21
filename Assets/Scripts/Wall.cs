using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	private Ball ball;
	public WallState State;

	public void SettingToWall(WallState state)
	{
		State = state;
	}

	private void Start()
	{
		ball = FindObjectOfType(typeof(Ball)) as Ball;
	}


	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Ball"))
		{
			//check valid ball.
			if (ball == null)
				return;

			//check ball position.
			Vector2 direction = (ball.transform.position - transform.position);

			if (State == WallState.Left || State == WallState.Right)
				direction.y = 0;
			else if (State == WallState.Bottom)
				direction.x = 0;
			else if (State == WallState.Upper)
				direction = Vector2.zero;

			//shoot
			ball.Shoot(direction.normalized * 5);
		}
	}

	public enum WallState
	{
		Left,
		Right,
		Bottom,
		Upper
	}
}
