using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Quit_Handler : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{

    /*
    [SerializeField]
    private TestButton button = null;

    private void Awake()
    {
        button.Activated += OnButtonPressed;
    }

    */

    public void OnFocusEnter(FocusEventData eventData)
    {
        gameObject.GetComponentInChildren<Button>().Select();
    }
    public void OnFocusExit(FocusEventData eventData) { }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Quit hit");
        // if clicked at button
        // if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
}
