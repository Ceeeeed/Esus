using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Animator animator;
	Rigidbody2D rb;
	float jumpPower = 20;
	float fallMultiplier = 2.5f;
	float lowJumpMultiplier = 5f;

	float moveSpeed = 5f;
	float rayLenght = 1.5f;
	RaycastHit2D hit;
	RaycastHit2D hit2;

	public bool canWalkLeft = true;
	public bool canJump = true;

	public bool canMove = true;

	void Start () {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		//Physics2D.gravity = new Vector2(0f, -13f);
	}
	
	void Update () {
		if(canMove) {
		
			if (Input.GetKeyDown("w")) {
				if(canJump) {
					hit = Physics2D.Raycast(transform.position - new Vector3 (.3f, 0, 0), Vector2.down, rayLenght,1 << 8);
					hit2 = Physics2D.Raycast(transform.position + new Vector3 (.3f, 0, 0), Vector2.down, rayLenght,1 << 8);
					
					if (hit.collider || hit2.collider) {
						rb.velocity = new Vector2(rb.velocity.x, jumpPower);
					}
					DebugRay();
				} else {
					Debug.Log("You cant jump");
				}
			}
			
			if (rb.velocity.y < 0) {
				rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
				
			} else if (rb.velocity.y > 0 && !Input.GetKey("w")) {
				rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
			}

			if (Input.GetKey("a")) {
				if(canWalkLeft) {
				rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
				Vector3 scale = new Vector3(-7, transform.localScale.y, transform.localScale.z);
				transform.localScale = scale;
				} else {
					Debug.Log("You cant walk left");
				}

			} else if (Input.GetKey("d")) {
				rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
				Vector3 scale = new Vector3(7, transform.localScale.y, transform.localScale.z);
				transform.localScale = scale;
			}
		}


		animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
		animator.SetFloat("velocityY", rb.velocity.y);
	}


	void DebugRay() {

            if (hit.collider || hit2.collider) {
		 		Debug.DrawRay(transform.position - new Vector3 (.3f, 0, 0), Vector2.down * rayLenght, Color.green, 2f);
				 Debug.DrawRay(transform.position + new Vector3 (.3f, 0, 0), Vector2.down * rayLenght, Color.green, 2f);
            } else {
			 	Debug.DrawRay(transform.position - new Vector3 (.3f, 0, 0), Vector2.down * rayLenght, Color.red, 2f);
				 Debug.DrawRay(transform.position + new Vector3 (.3f, 0, 0), Vector2.down * rayLenght, Color.red, 2f);
			}
	}
}
