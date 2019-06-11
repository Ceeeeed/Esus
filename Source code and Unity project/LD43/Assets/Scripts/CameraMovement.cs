using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour {


	Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + 2f, player.position.y + 1.5f, transform.position.z), .05f);
		if (player.position.x < 3.1f)
			transform.position = new Vector3(5.5f, transform.position.y, transform.position.z);

		if (player.position.y < -40f)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}
}
