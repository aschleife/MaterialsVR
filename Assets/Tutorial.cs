using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField] private Text tutorial_text;
    [SerializeField] private UIManager ui_manager;
    [SerializeField] private MoleculeLoader molecule_loader;
    [SerializeField] private Text atom_text;
    [SerializeField] private Text button_text;
    [SerializeField] private PinchSlider atom_slider;
    [SerializeField] private PinchSlider iso_slider;
    private bool inactive;
    public int step;
    private Vector3 vec3;
    string str;
    float f;
    bool boolean;
    // Start is called before the first frame update
    void Start()
    {
        inactive = true;
        SetTutorialText("Welcome to MaterialsVR");
        button_text.text = "Tutorial";
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        ToggleTutorial();
    }

    private void ToggleTutorial()
    {
        if (step == -1)
        {
            inactive = false;
            step = 0;
            button_text.text = "End";
        }
        else
        {
            inactive = true;
            step = -1;
            button_text.text = "Tutorial";
        }
    }

    IEnumerator DisplayMessage(string msg)
    {
        inactive = true;
        tutorial_text.text = msg;
        tutorial_text.color = Color.green;
        yield return new WaitForSeconds(2.5f);
        inactive = false;
    }

    void SetTutorialText(string text)
    {
        tutorial_text.text = text;
        tutorial_text.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (inactive) return;
        switch (step)
        {
            case 0:
                SetTutorialText("Click on a Molecule.");
                ui_manager.Menu();
                step++;
                break;
            case 1:
                if (objMessage.unLoadIsosurface() == false && molecule_loader.transform.parent.localScale == Vector3.one)
                {
                    StartCoroutine(DisplayMessage("You got it!"));
                    step++;
                }
                break;
            case 2:
                SetTutorialText("Try to grab and move it.");
                vec3 = molecule_loader.gameObject.transform.position;
                step++;
                break;
            case 3:
                if (molecule_loader.gameObject.transform.position != vec3)
                {
                    StartCoroutine(DisplayMessage("You're pretty good at this!"));
                    step++;
                }
                
                break;
            case 4:
                SetTutorialText("Click on Rotation.");
                boolean = objMessage.loadBoolean();
                step++;
                break;
            case 5:
                if (boolean != objMessage.loadBoolean())
                {
                    StartCoroutine(DisplayMessage("Rotation Toggled!"));
                    step++;
                }
                break;
            case 6:
                SetTutorialText("Pinch the corner to resize it.");
                vec3 = molecule_loader.gameObject.transform.parent.localScale;
                step++;
                break;
            case 7:
                if (molecule_loader.gameObject.transform.parent.localScale != vec3)
                {
                    StartCoroutine(DisplayMessage("Well done!"));
                    step++;
                }
                break;
            case 8:
                SetTutorialText("Click on Movable.");
                boolean = molecule_loader.GetComponentInParent<ObjectManipulator>().enabled;
                step++;
                break;
            case 9:
                if (boolean != molecule_loader.GetComponentInParent<ObjectManipulator>().enabled)
                {
                    StartCoroutine(DisplayMessage("Object won't move now."));
                    step++;
                }
                break;
            case 10:
                SetTutorialText("Click on Atom to show info.");
                str = atom_text.text;
                step++;
                break;
            case 11:
                if (str != atom_text.text)
                {
                    StartCoroutine(DisplayMessage("The Atom name is shown."));
                    step++;
                }
                break;
            case 12:
                SetTutorialText("Use the Slider to change Atom size");
                f = atom_slider.SliderValue;
                step++;
                break;
            case 13:
                if (f != atom_slider.SliderValue)
                {
                    StartCoroutine(DisplayMessage("Well done."));
                    step++;
                }
                break;
            case 14:
                SetTutorialText("Select \"Add a Plane\" from the menu");
                step++;
                break;
            case 15:
                if (GameObject.FindGameObjectWithTag("plane") != null)
                {
                    StartCoroutine(DisplayMessage("Good job!"));
                    step++;
                }
                step++;
                break;
            case 16:
                SetTutorialText("Click on Menu to go back.");
                step++;
                break;
            case 17:
                if (molecule_loader.transform.parent.localScale == Vector3.zero)
                {
                    StartCoroutine(DisplayMessage("Nice!"));
                    step++;
                }
                break;
            case 18:
                SetTutorialText("Click on \"Switch to Isosurfaces\".");
                step++;
                break;
            case 19:
                if (UIManager.activeTag == "b_iso")
                {
                    StartCoroutine(DisplayMessage("Now showing Isosurface files."));
                    step++;
                }
                break;
            case 20:
                SetTutorialText("Select an Isosurface file.");
                step++;
                break;
            case 21:
                if (objMessage.unLoadIsosurface() == true && molecule_loader.transform.parent.localScale == Vector3.one)
                {
                    StartCoroutine(DisplayMessage("That was easy!"));
                    step++;
                }
                break;
            case 22:
                SetTutorialText("Use the 2nd Slider to change surface level");
                f = iso_slider.SliderValue;
                step++;
                break;
            case 23:
                if (f != iso_slider.SliderValue)
                {
                    StartCoroutine(DisplayMessage("Pretty awesome huh."));
                    step++;
                }
                break;
            case 24:
                StartCoroutine(DisplayMessage("Tutorial Complete!"));
                step++;
                break;
            case 25:
                SetTutorialText("Welcome to MaterialsVR");
                ToggleTutorial();
                break;

            default:
                break;

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
