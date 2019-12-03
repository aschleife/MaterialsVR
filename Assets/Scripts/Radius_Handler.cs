using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Radius_Handler : MonoBehaviour, IMixedRealityPointerHandler
{
    private string keyword = "Ball";
    private GameObject[] array;
    private GameObject molecule;
    // use one button toggled
    public GameObject ON_OFF_Button;

    // Start is called before the first frame update
    public void Start()
    {
        array = GameObject.FindGameObjectsWithTag("edmc");
    }

    // Update is called once per frame
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

    public void OnPointerClicked(MixedRealityPointerEventData data)
    {
        Vector3 delta = new Vector3(0.05f, 0.05f, 0.05f);
        MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();

        if (ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Atom Size +")
        {
            foreach (MeshRenderer i in objects)
            {
                GameObject atom = i.gameObject;
                if (atom.ToString().Contains(keyword))
                {
                    if (atom.transform.localScale[0] < 2.0f)
                        atom.transform.localScale += delta;
                    //Debug.Log(atom.transform.localScale);
                }
            }
        }
        else if (ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Atom Size -")
        {
            foreach (MeshRenderer i in objects)
            {
                GameObject atom = i.gameObject;
                if (atom.ToString().Contains(keyword))
                {
                    if (atom.transform.localScale[0] > 0.1f)
                        atom.transform.localScale -= delta;
                    //Debug.Log(atom.transform.localScale);
                }
            }
        }

        //if (ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Change Atom Size OFF")
        //{
        //    foreach (MeshRenderer i in objects)
        //    {
        //        GameObject atom = i.gameObject;
        //        if (atom.ToString().Contains(keyword))
        //            atom.transform.localScale += delta;
        //    }
        //    ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Change Atom Size ON";
        //}
        //else if (ON_OFF_Button.GetComponentsInChildren<Text>()[0].text == "Change Atom Size ON")
        //{
        //    foreach (MeshRenderer i in objects)
        //    {
        //        GameObject atom = i.gameObject;
        //        if (atom.ToString().Contains(keyword))
        //            atom.transform.localScale -= delta;
        //    }
        //    ON_OFF_Button.GetComponentsInChildren<Text>()[0].text = "Change Atom Size OFF";
        //}
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
}
