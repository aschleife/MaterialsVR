using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
	Handler of back button on the main page. Lead user back to the menu page.
**/
public class Back_Handler : MonoBehaviour, IPointerClickHandler{

	public void OnPointerClick(PointerEventData data){
        if(gameObject.tag == "b"){
            SceneManager.LoadScene("New_Menu");
        }
    }
}
