using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Input;

public class Handler : MonoBehaviour, IMixedRealityPointerHandler
{

    // maybe use GvrControllerInput.ClickButtonDown later
	public void OnPointerClicked(MixedRealityPointerEventData eventData){
        Debug.Log("Click registered");
        //if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        if(gameObject.tag == "b")
        {
            objMessage.loadMessage(gameObject.name);
            SceneManager.LoadScene("SPIN6.26");
            objMessage.revolve();
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Click registered");
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }
}
