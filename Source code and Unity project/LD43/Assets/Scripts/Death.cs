using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	public Material mat;
	public GameObject hp;
	public bool spawnHp;

	void Awake() {
    	SpriteRenderer sr = GetComponent<SpriteRenderer>();
		mat = sr.material;
		
	}
	// Use this for initialization
	public void Kill() {
		mat.SetFloat("_Threshold", 0);
		StartCoroutine(SelfDestroy());
	}

	IEnumerator SelfDestroy() {

		foreach(Collider2D c in GetComponents<Collider2D> ()) {
        	c.enabled = false;
     	}
		foreach(Rigidbody2D c in GetComponents<Rigidbody2D> ()) {
        	c.bodyType = RigidbodyType2D.Static;
     	}

		float x = 1;
		while(x > 0f) {
			x -= Time.deltaTime*2;
			mat.SetColor("_Color", new Color(1, 1, 1, x));
			yield return null;
    	}
		if (spawnHp)
			Instantiate(hp, transform.position, Quaternion.identity);

		Destroy(gameObject);
		GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
	}
}
