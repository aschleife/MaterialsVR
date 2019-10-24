using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour, IPointerClickHandler{

    // maybe use GvrControllerInput.ClickButtonDown later
	public void OnPointerClick(PointerEventData data){
        //if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        if(gameObject.tag == "b")
        {
            objMessage.loadMessage(gameObject.name);
            SceneManager.LoadScene("SPIN6.26");
            objMessage.revolve();
        }
    }
}
