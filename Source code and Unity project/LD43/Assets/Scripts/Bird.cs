using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	Animator animator;
	public float seeRange = 1f;
	public float attackPower = 10f;
	bool one = true;
	Transform player;

	bool altarOn = false;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		animator.SetBool("idle", true);

		player = GameObject.Find("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(player.position, transform.position);

		if (dist < seeRange && one) {
			print("X");
			animator.SetBool("idle", false);
			Invoke("EndAnim", 0.5f);
			one = false;
			player.GetComponent<PlayerMovement>().canMove = false;
			player.GetComponent<Rigidbody2D>().velocity = new Vector2 (-attackPower, attackPower);
		}
	}

	void EndAnim() {
		animator.SetBool("idle", true);
		one = true;
		player.GetComponent<PlayerMovement>().canMove = true;
		if(!altarOn) {
			GameObject.Find("Altar (2)").GetComponent<Altar>().ready = true;
			altarOn = true;
		}
	}
}
