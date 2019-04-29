using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
	Handler for both xyz and wrl models rotation. With help or objMessage to transfer rotation status.
**/
// may transfer to scroll controller later
public class Rotation_Handler : MonoBehaviour, IPointerClickHandler {
	private GameObject[] array;
	// use one button toggled
	public GameObject ON_OFF_Button;

	// Use this for initialization
	public void Start () {
		ON_OFF_Button = GameObject.Find("Rotation_Controller");
		GameObject[] xyz = GameObject.FindGameObjectsWithTag("xyz");
		GameObject[] wrl = GameObject.FindGameObjectsWithTag("wrl");
		array = xyz.Concat(wrl).ToArray();
	}

	public void Update(){
		if(array.Length == 0){
			GameObject[] xyz = GameObject.FindGameObjectsWithTag("xyz");
			GameObject[] wrl = GameObject.FindGameObjectsWithTag("wrl");
			array = xyz.Concat(wrl).ToArray();
		}
	}
	
	public void OnPointerClick(PointerEventData data) {
		if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Rotation Mode OFF"){ // rotating
			objMessage.pause();
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Rotation Mode ON";
		}
		else if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Rotation Mode ON"){
			objMessage.revolve();
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Rotation Mode OFF";
		}
	}
}
