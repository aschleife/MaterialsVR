using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene_Handler : MonoBehaviour, IMixedRealityFocusHandler
{
    public static bool tutorial_enabled;
    public void OnFocusEnter(FocusEventData eventData)
    {
        gameObject.GetComponentInChildren<Button>().Select();
    }
    public void OnFocusExit(FocusEventData eventData) { }

    public void StartScene(bool tutorial_enabled)
    {
        StartScene_Handler.tutorial_enabled = tutorial_enabled;
        SceneManager.LoadScene("Main");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
