using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour {

	SacrificesManager sm;
	public int AltarId;
	public bool ready = true;

		void Start () {
			sm = GameObject.Find("Player").GetComponent<SacrificesManager>();
		}

		private void OnTriggerEnter2D (Collider2D other)
		{
			if(ready)
				sm.AltarEnterExit (this, true);
		}
		private void OnTriggerExit2D (Collider2D other)
		{
				sm.AltarEnterExit (null, false);
		}
	
}
