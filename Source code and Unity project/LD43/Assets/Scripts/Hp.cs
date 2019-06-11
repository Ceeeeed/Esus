using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour {

	PlayerAttack pa;
	bool one = true;
	// Use this for initialization
	void Start () {
		pa = GameObject.Find("Player").GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	private void OnTriggerEnter2D (Collider2D other)
    {
		if(other.gameObject.name == "Player") {
			if(one) {
				pa.hp++;
				GetComponent<Death>().Kill();
				one = false;
			}
		}

    }
}
