using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;

// may transfer to scroll controller later
public class Rotation_Handler : MonoBehaviour, IMixedRealityPointerHandler
{
	private GameObject molecule;
	private GameObject[] array;
    // use one button toggled
    [SerializeField] TextMeshPro buttonText;

    /*

    [SerializeField]
    private TestButton button = null;

    private void Awake()
    {
        button.Activated += OnButtonPressed;
    }

    // Use this for initialization
    public void Start () {
		//ON_OFF_Button = GameObject.Find("Rotation_Controller");
		// yield return new WaitForSeconds(1);
		array = GameObject.FindGameObjectsWithTag("edmc");
	}

	public void Update(){
		if(array.Length == 0 && molecule == null){
			array = GameObject.FindGameObjectsWithTag("edmc");
		}
		else{
			molecule = array[0];
		}
	}

    private void OnButtonPressed(TestButton data)
    {
		if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Rotation Mode OFF"){ // rotating
			objMessage.pause();
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Rotation Mode ON";
		}
		else if(ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Rotation Mode ON"){
			objMessage.revolve();
			ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Rotation Mode OFF";
		}
	}

    */

    public void Start()
    {
        //ON_OFF_Button = GameObject.Find("Rotation_Controller");
        // yield return new WaitForSeconds(1);
        array = GameObject.FindGameObjectsWithTag("edmc");
    }

    public void Update()
    {
        if (array.Length == 0 && molecule == null)
        {
            array = GameObject.FindGameObjectsWithTag("edmc");
        }
        else
        {
            molecule = array[0];
        }
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {

        if (buttonText.text == "Rotation OFF")
        { // rotating
            objMessage.pause();
            buttonText.text = "Rotation ON";
        }
        else if (buttonText.text == "Rotation ON")
        {
            objMessage.revolve();
            buttonText.text = "Rotation OFF";
        }
    }
    public void OnPointerDown(MixedRealityPointerEventData eventData) {}

    public void OnPointerDragged(MixedRealityPointerEventData eventData) {}

    public void OnPointerUp(MixedRealityPointerEventData eventData) {}

}
