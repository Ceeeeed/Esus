using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VO : MonoBehaviour {

	public AudioClip[] clips;
	AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	public void PlayClip(int id) {
		audio.clip = clips[id];
		audio.Play();
	}
}
