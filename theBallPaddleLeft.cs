using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theBallPaddleLeft : MonoBehaviour
{

	public KeyCode movePlayerUp = KeyCode.W;
	public KeyCode movePlayerDown = KeyCode.S;
	// ^ Set W and S for movement
	public float speed = 15.0f;
	public float AxisY = 8.8f;
	private Rigidbody2D rigidBody2D;

	// Use this for initialization
	void Start()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		var vel = rigidBody2D.velocity;
		if (Input.GetKey(movePlayerUp))
		{
			vel.y = speed;
		}
		else if (Input.GetKey(movePlayerDown))
		{
			vel.y = -speed;
		}
		else if (!Input.anyKey)
		{
			vel.y = 0;
		}
		rigidBody2D.velocity = vel;

		var pos = transform.position;
		if (pos.y > AxisY)
		{
			pos.y = AxisY;
		}
		else if (pos.y < -AxisY)
		{
			pos.y = -AxisY;
		}
		transform.position = pos;
	}
}