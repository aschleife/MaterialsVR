using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAbout : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    private GameObject aboutCanvas;

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        aboutCanvas.SetActive(true);
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

    public void OnFocusEnter(FocusEventData eventData)
    {
        gameObject.GetComponentInChildren<Button>().Select();
    }
    public void OnFocusExit(FocusEventData eventData) { }

    // Start is called before the first frame update
    void Start()
    {
        aboutCanvas = GameObject.Find("AboutCanvas");
        aboutCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
