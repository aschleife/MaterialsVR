using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;

public class ToggleTag : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    [SerializeField] GameObject canvas;
    [SerializeField] string switch_to_tag;

    private void Start()
    {
        canvas = GameObject.Find("Menu_Canvas");
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        gameObject.GetComponentInChildren<Button>().Select();
    }

    public void OnFocusExit(FocusEventData eventData) { }

    // maybe use GvrControllerInput.ClickButtonDown later
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        canvas.GetComponent<AddMRTKButtons>().setActiveTag(switch_to_tag);
        Debug.Log("Tag Toggled");
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
