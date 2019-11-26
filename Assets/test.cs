using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Input;

public class test : MonoBehaviour, IMixedRealityPointerHandler
{

    // maybe use GvrControllerInput.ClickButtonDown later
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Click registered");
        
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