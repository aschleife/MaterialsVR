﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
	Hander for polyhedral hide button on the main page. Deal with polyhedral structure of wrl models.
**/
public class HideHandler : MonoBehaviour, IPointerClickHandler {
	private string keyword = "Shape_IndexedFaceSet";
	private GameObject molecule;
	private GameObject[] array;
	// use one button toggled
	public GameObject ON_OFF_Button;

	public void Start(){
		ON_OFF_Button = GameObject.Find("Polyhedral_Controller");
		array = GameObject.FindGameObjectsWithTag("wrl");
	}

	public void Update(){
		if(array.Length == 0 && molecule == null){
			array = GameObject.FindGameObjectsWithTag("wrl");
		}
		else{
			molecule = array[0];
		}
	}

	public void OnPointerClick(PointerEventData data){
		MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
		if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Polyhedral OFF"){ // plates are on
			foreach (MeshRenderer i in objects){
				if(i.gameObject.ToString().Contains(keyword))
	            	// hide paltes
					i.enabled = false;
			}
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Polyhedral ON";
		}
		else if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Polyhedral ON"){
			foreach (MeshRenderer i in objects){
				if(i.gameObject.ToString().Contains(keyword))
	            	// show plates
					i.enabled = true;
			}
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Polyhedral OFF";
		}
    }
}
