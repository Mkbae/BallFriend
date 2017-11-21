using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	private Rigidbody2D myRigidbody;

	private void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
	}

	public void StateReset()
	{
		if (myRigidbody != null)
		{
			myRigidbody.velocity = Vector2.zero;
			myRigidbody.angularVelocity = 0;
		}

		transform.position = new Vector2(0, 3);
	}

	public void Shoot(Vector3 velocity)
	{
		myRigidbody.AddForce(velocity, ForceMode2D.Impulse);
	}
}
