using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
	Handler for quit button on the main page. Ability to quit the program after clicking.
**/
public class Quit_Handler : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData data){
        if(gameObject.tag == "b"){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
