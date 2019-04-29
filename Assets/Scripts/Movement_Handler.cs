﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
	Handler for both xyz and wrl models movement. 
	Basically, enable molecular handler or disable it with help of sphere collider. Also toggle the button text.
**/
public class Movement_Handler : MonoBehaviour, IPointerClickHandler {
	private string keyword = "Ball";
	private GameObject[] array = null;
	// use one button toggled
	public GameObject ON_OFF_Button;

	// Use this for initialization
	public void Start(){
		ON_OFF_Button = GameObject.Find("Movement_Controller");
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
		// GvrControllerInput.AppButton
		if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Movement Mode ON"){ // not able to move
			foreach(GameObject molecule in array){
				// remove old Sphere Collider
				MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer i in objects){
					GameObject atom = i.gameObject;
					Destroy(atom.GetComponent<SphereCollider>());
				}
	        	// add Sphere Collider
				SphereCollider collider_molecule = molecule.AddComponent<SphereCollider>() as SphereCollider;
	    		collider_molecule.radius = 10;
				ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Movement Mode OFF";
			}
		}
		else if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Movement Mode OFF"){
			foreach(GameObject molecule in array){
				// remove old Sphere Collider
				Destroy(molecule.GetComponent<SphereCollider>());
				// add Sphere Collider 
				MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer i in objects){
					GameObject atom = i.gameObject;
					if(atom.ToString().Contains(keyword)){
						SphereCollider collider_atom = atom.AddComponent<SphereCollider>() as SphereCollider;
					}
				}
				ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Movement Mode ON";
			}
		}
    }
}
