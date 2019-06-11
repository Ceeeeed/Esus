using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class SacrificesManager : MonoBehaviour {

	public Sacrifice[] sacrifices;

	PostProcessingProfile profile;

	GameObject buttonsParent;
	public Sprite offAltar;

	public bool onAltar;
	public Altar currentAltar;

	public GameObject platform;

	public VO vo;

	public GameObject kap, pow;


	public class Sacrifice {
		public string name;
		public bool sacrificed = false;
		public Button button;


		public Sacrifice(string _name) {
			name = _name;
		}
	}

	public void AltarEnterExit (Altar _currentAltar, bool _onAltar) {
		currentAltar = _currentAltar;
		onAltar = _onAltar;
		foreach (Sacrifice b in sacrifices) {
			b.button.interactable = onAltar;
		}
	}

	// Use this for initialization
	void Start () {
		profile = new PostProcessingProfile();
		GameObject.Find("Main Camera").GetComponent<PostProcessingBehaviour>().profile = profile;
		buttonsParent = GameObject.Find("Buttons");
		vo = GameObject.Find("VO").GetComponent<VO>();
		SacInit();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void UseSacrifice (int index) {
		sacrifices[index].sacrificed = true;
		sacrifices[index].button.interactable = false;
		sacrifices[index].button.enabled = false;
		sacrifices[index].button.gameObject.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.5f);

		foreach (Sacrifice b in sacrifices) {
			b.button.interactable = false;
		}


		if (index == 0) {
			if (sacrifices[0].sacrificed && sacrifices[1].sacrificed)
				StartCoroutine(EyeTwo());
			else
				StartCoroutine(EyeOne());

		} else if (index == 1) {
			if (sacrifices[0].sacrificed && sacrifices[1].sacrificed)
				StartCoroutine(EyeTwo());
			else
				StartCoroutine(EyeOne());

		} else if (index == 2) {
			GetComponent<PlayerMovement>().canWalkLeft = false;
			vo.PlayClip(1);

		} else if (index == 3) {
			GetComponent<PlayerMovement>().canJump = false;
			vo.PlayClip(10);

		} else if (index == 4) {
			if (sacrifices[4].sacrificed && sacrifices[5].sacrificed)
				vo.PlayClip(12);
			else
				vo.PlayClip(11);

		} else if (index == 5) {
			if (sacrifices[4].sacrificed && sacrifices[5].sacrificed)
				vo.PlayClip(12);
			else
				vo.PlayClip(11);

		} else if (index == 6) {
			vo.PlayClip(8);
		} else if (index == 7) {
			vo.PlayClip(9);
		} else if (index == 8) {
			GetComponent<PlayerAttack>().canAttack = false;
			vo.PlayClip(0);
		} else if (index == 9) {
			vo.PlayClip(15);
		} else if (index == 10) {
			vo.PlayClip(16);
		}

		currentAltar.ready = false;
		currentAltar.transform.GetComponent<SpriteRenderer>().sprite = offAltar;
		foreach (Transform t in currentAltar.transform)
			t.gameObject.SetActive(false);

		if (currentAltar.AltarId == 1) {
			GameObject.Find("papuga").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 2) {
			GameObject.Find("magictree").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 3) {
			GameObject.Find("leszychild").SetActive(false);
			GameObject.Find("Leszy").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 4) {
			GameObject.Find("typo").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 5) {
			GameObject.Find("slup").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 6) {
			GameObject.Find("ciernie").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 7) {
			GameObject.Find("totem").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 8) {
			GameObject.Find("scianka").GetComponent<Death>().Kill();
		} else if (currentAltar.AltarId == 9) {
			platform.SetActive(true);
		} else if (currentAltar.AltarId == 10) {
			if(index == 0 || index == 1) { //oczy
				vo.PlayClip(3);
				transform.position = new Vector3(375.06f, -13.42f, 0f);
				Instantiate(kap);
			} else if(index == 8) {//atak
				vo.PlayClip(5);
				transform.position = new Vector3(375.06f, -13.42f, 0f);
				Instantiate(kap);
			} else if(index == 10 || index == 9) {//life
				vo.PlayClip(6);
				transform.position = new Vector3(375.06f, -13.42f, 0f);
				Instantiate(kap);
			} else if(index == 7) {//friend
				vo.PlayClip(7);
				transform.position = new Vector3(375.06f, -13.42f, 0f);
				Instantiate(kap);
			} else {
				vo.PlayClip(3);
				transform.position = new Vector3(375.06f, -13.42f, 0f);
				Instantiate(pow);
			}
			transform.gameObject.SetActive(false);
		}


	}

	void SacInit() {
		sacrifices = new Sacrifice[11];
		sacrifices[0] = new Sacrifice("Eye - you can see");
		sacrifices[1] = new Sacrifice("Eye - you can see");
		sacrifices[2] = new Sacrifice("Going back - A");
		sacrifices[3] = new Sacrifice("Jump - W");
		sacrifices[4] = new Sacrifice("Ear - you can hear");
		sacrifices[5] = new Sacrifice("Ear - you can hear");
		sacrifices[6] = new Sacrifice("Interaction - E");
		sacrifices[7] = new Sacrifice("Friend");
		sacrifices[8] = new Sacrifice("Attack - Space");
		sacrifices[9] = new Sacrifice("Life - you can try again if you have a life");
		sacrifices[10] = new Sacrifice("Death - R");

		for (int i = 0; i < sacrifices.Length; i++) {
			sacrifices[i].button = buttonsParent.transform.GetChild(i).GetComponent<Button>();
		}
	}

	IEnumerator EyeOne() {
		vo.PlayClip(14);
		profile.colorGrading.enabled = true;
		ColorGradingModel.Settings cg = profile.colorGrading.settings;
		cg.tonemapping.tonemapper = ColorGradingModel.Tonemapper.None;

		for (float i = cg.basic.saturation; i > 0; i -= Time.deltaTime/5) {
			cg.basic.saturation = i;
			profile.colorGrading.settings = cg;
			yield return null;
		}

		cg.basic.saturation = 0;
		profile.colorGrading.settings = cg;
	}

	public IEnumerator EyeTwo() {
		vo.PlayClip(13);
		ColorGradingModel.Settings cg = profile.colorGrading.settings;

		for (float i = cg.colorWheels.linear.gain.r; i < 1; i += Time.deltaTime/5) {
			cg.colorWheels.linear.gain = new Color(i, i, i, -i);
			profile.colorGrading.settings = cg;
		 	yield return null;
		}

		cg.colorWheels.linear.gain = new Color(1, 1, 1, -1);
		profile.colorGrading.settings = cg;
	}

}
