﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	// Use this for initialization
	void Awake () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();    
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") {
			Destroy(gameObject);
		}	
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		} else if (Input.GetButtonUp ("Jump")) 
		{
			if (velocity.y > 0) {
				velocity.y = velocity.y * 0.5f;
			}
		}

		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (flipSprite) 
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;
	}
}