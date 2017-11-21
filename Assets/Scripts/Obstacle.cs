using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Ball"))
		{
			//check valid ball.
			Ball ball = col.gameObject.GetComponent<Ball>();

			if (ball == null)
				return;

			//check ball position.
			Vector2 direction = (ball.transform.position - transform.position);

			//shoot
			ball.Shoot(direction.normalized * 5);
		}
	}
}
