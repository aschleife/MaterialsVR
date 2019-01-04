using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// may transfer to scroll controller later
public class Rotation_Handler : MonoBehaviour, IPointerClickHandler {
	private GameObject molecule;
	private GameObject[] array;
	// use one button toggled
	public GameObject ON_OFF_Button;

	// Use this for initialization
	public void Start () {
		ON_OFF_Button = GameObject.Find("Rotation_Controller");
		// yield return new WaitForSeconds(1);
		array = GameObject.FindGameObjectsWithTag("edmc");
	}

	public void Update(){
		if(array.Length == 0 && molecule == null){
			array = GameObject.FindGameObjectsWithTag("edmc");
		}
		else{
			molecule = array[0];
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
