using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField] GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(GameObject PlanePrefab)
    {
        Instantiate(PlanePrefab);
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        
        Spawn(plane);
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
