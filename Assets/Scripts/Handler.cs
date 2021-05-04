using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Input;

public class Handler : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    void Start()
    {
        UIManager uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        if (uIManager == null)
            Debug.LogError("UIManager not found!");
    }
    public void OnFocusEnter(FocusEventData eventData)
    {
        gameObject.GetComponent<Button>().Select();
    }

    public void OnFocusExit(FocusEventData eventData) {}

    // maybe use GvrControllerInput.ClickButtonDown later
    public void OnPointerClicked(MixedRealityPointerEventData eventData){
        //if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        if(gameObject.tag == UIManager.activeTag)
        {
            objMessage.loadMessage(gameObject.name, gameObject.tag);
            //SceneManager.LoadScene("SPIN6.26");
            objMessage.revolve();
            //StartCoroutine(UIManager.UIMan.loader.LoadObject(gameObject.name, gameObject.tag));
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }
}
