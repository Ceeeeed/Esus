using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float moveSpeed;
	Animator animator;
	Rigidbody2D rb;
	Death death;
	Transform player;
	public float seeRange = 6f;
	public float attackRange = 3f;
	public float attackPower = 15f;

	bool isAttacking;

	bool run = false;

	void Start () {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		death = GetComponent<Death>();
		player = GameObject.Find("Player").GetComponent<Transform>();
		StartCoroutine(FindPlayer());
	}
	
	// Update is called once per frame
	void Update () {
		if (run) {
			if(player.position.x < transform.position.x) {
				rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
				transform.localScale = new Vector3 (7f, 7f, 7f);
			} else {
				rb.velocity = new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
				transform.localScale = new Vector3 (-7f, 7f, 7f);
			}

			float dist = Vector3.Distance(player.position, transform.position);
			if(dist < attackRange && !isAttacking)
				StartCoroutine(Attack());
		}

		animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
		
	}

	IEnumerator FindPlayer() {
		while(true) {
			float dist = Vector3.Distance(player.position, transform.position);
			if (dist < seeRange)
				run = true;
		
			yield return new WaitForSeconds(1f);
		}
	}

	IEnumerator Attack() {
		
			StopCoroutine(Attack());
			isAttacking = true;
			float attackProgress = 0.5f;
			if (player.GetComponent<PlayerAttack>().attackProgress <= 0f) {
				player.GetComponent<PlayerMovement>().canMove = false;
				if(transform.localScale.x < 0f) {
					player.GetComponent<Rigidbody2D>().velocity = new Vector2 (attackPower, attackPower);
				} else {
					player.GetComponent<Rigidbody2D>().velocity = new Vector2 (-attackPower, attackPower);
				}
				player.GetComponent<PlayerAttack>().hp--;
				animator.SetFloat("attack", attackProgress);
			}

			while (attackProgress > 0f)
			{
					
					attackProgress -= Time.deltaTime;
					yield return null;
			}
			player.GetComponent<PlayerMovement>().canMove = true;
			animator.SetFloat("attack", 0f);
			print(Vector3.Distance(player.position, transform.position));
			isAttacking = false;
		
    }
}
