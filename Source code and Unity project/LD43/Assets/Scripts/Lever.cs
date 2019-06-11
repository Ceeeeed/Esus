using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	bool once = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	private void OnTriggerStay2D (Collider2D other)
	{
		if(once && Input.GetKeyDown("e")) {
			transform.rotation = new Quaternion(0,0,-70,0);
			GameObject.Find("blokada").GetComponent<Death>().Kill();
			Destroy(transform.GetChild(0).gameObject);
			once = false;
		}
			
	}
}
