using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour {

	float rayLenght = 2f;
	public float attackProgress = 0;
	RaycastHit2D hit;
	Animator animator;
	bool lastAttack = true;
	public bool canAttack = true;
	public GameObject hpo;

	public int _hp;
	public int hp
	{
		get { return _hp; }
        set { _hp = value;
			foreach (Transform t in GameObject.Find("hpholder").transform)
				Destroy(t.gameObject);
			if (value>0) {
				for(int i = 0; i < value; i++) {
					Instantiate(hpo, GameObject.Find("hpholder").transform);
				} 
			} else {
				gameOver();
			}
		}
		
     }

	 void gameOver() {
		 SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	 }

	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space") && attackProgress <= 0) {
			if (canAttack) {
			StartCoroutine(Attack());
			} else {
				Debug.Log("You cant attack");
			}
		}
	}

	void DebugRay() {
		
		if(transform.localScale.x < 0) {
         if (hit.collider) { 
				Debug.DrawRay(transform.position, -transform.right * rayLenght, Color.green);
         } else {
				Debug.DrawRay(transform.position, -transform.right * rayLenght, Color.red);
			}
		} else {
			if (hit.collider) {
				Debug.DrawRay(transform.position, transform.right * rayLenght, Color.green);
			} else {
				Debug.DrawRay(transform.position, transform.right * rayLenght, Color.red);
			}
		}
	}

    IEnumerator Attack() {
			StopCoroutine(Attack());
		 	attackProgress = 0.3f;
			lastAttack = !lastAttack;
			animator.SetBool("attackType", lastAttack);
			animator.SetFloat("attack", attackProgress);
			while (attackProgress > 0f)
			{
					if(transform.localScale.x < 0) {
							hit = Physics2D.Raycast(transform.position, -transform.right, rayLenght, 1 << 9);
							if (hit.collider)
								hit.collider.gameObject.GetComponent<Death>().Kill();
					} else {
							hit = Physics2D.Raycast(transform.position, transform.right, rayLenght, 1 << 9);
							if (hit.collider)
								hit.collider.gameObject.GetComponent<Death>().Kill();
					}

				

					DebugRay();
					
					attackProgress -= Time.deltaTime;
					yield return null;
			}
			animator.SetFloat("attack", attackProgress);
    }

}
		
