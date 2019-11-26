using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class AddPlaneHandler : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject PlanePrefab;

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
            Instantiate(PlanePrefab, result.Details.Point, Quaternion.LookRotation(result.Details.Normal));
        }
    }
}
