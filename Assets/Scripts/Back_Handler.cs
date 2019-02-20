using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Back_Handler : MonoBehaviour, IPointerClickHandler{

	public void OnPointerClick(PointerEventData data){
        // if clicked at button
        // if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        if(gameObject.tag == "b"){
            SceneManager.LoadScene("New_Menu");
        }
    }
}
