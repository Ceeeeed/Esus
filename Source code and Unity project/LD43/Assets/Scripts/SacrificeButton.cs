using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SacrificeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	TMPro.TextMeshProUGUI text;
	SacrificesManager sm;
	public int index;
	bool mouseIn = false;

	void Start () {
		text = GameObject.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
		sm = GameObject.Find("Player").GetComponent<SacrificesManager>();
	}
	

	public void OnPointerEnter(PointerEventData pointerEventData)
    {
		if(GetComponent<Button>().interactable) {
        	text.text = sm.sacrifices[index].name;
			mouseIn = true;
		}
    }


    public void OnPointerExit(PointerEventData pointerEventData)
    {	
		if (mouseIn) {
			text.text = "What do you want to sacrifice?";
			mouseIn = false;
		}
    }

	public void OnClick() {
		sm.UseSacrifice(index);
	}
}
