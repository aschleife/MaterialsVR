using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class AddPlane_Handler : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject PlanePrefab;
    private GameObject molecule;
    private GameObject[] array;

    public void Start()
    {
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
        Debug.Log("Plane Spawn");
        //if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        Spawn(eventData);
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

    public void Spawn(MixedRealityPointerEventData eventData)
    {
        if (PlanePrefab != null)
        {
            var result = eventData.Pointer.Result;
            Instantiate(PlanePrefab, molecule.transform.position, Quaternion.LookRotation(result.Details.Normal));
            Debug.Log(molecule.transform.position);
        }
    }
}
