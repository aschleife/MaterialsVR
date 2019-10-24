using HoloToolkit.Unity.InputModule.Tests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Back_Handler : MonoBehaviour, IPointerClickHandler
{
    private GameObject MR;
    private GameObject UI;

    /*
    [SerializeField]
    private TestButton button = null;

    private void Awake()
    {
        button.Activated += OnButtonPressed;
    }

    

    private void OnButtonPressed(TestButton data) { 
        // if clicked at button
        // if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        SceneManager.LoadScene("New_Menu");
        
    }

    */


    private void Start()
    {
        MR = GameObject.Find("Microsoft Camera and Input Rig");
        UI = GameObject.Find("UIManager MR");
    }

    public void OnPointerClick(PointerEventData data)
    {
        // if clicked at button
        // if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        SceneManager.LoadScene("New_Menu_without_MR_Rig");
        //Destroy(MR);
        //Destroy(UI);
    }
}
