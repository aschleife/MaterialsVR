using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Back_Handler : MonoBehaviour, IPointerClickHandler{

	public void OnPointerClick(PointerEventData data){
        // if clicked at button
        // if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        if(gameObject.tag == "b"){
            Application.LoadLevel("New_Menu");
        }
    }
}
