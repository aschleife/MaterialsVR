using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBar : MonoBehaviour
{
    [SerializeField]
    private Loader loader;
    [SerializeField]
    private GameObject followObject;
    [SerializeField]
    [Range(0f, 10f)]
    private float followLerpTime = 6f;

    private Transform buttons;
    private Transform sliders;
    private static GameObject planePrefab;
    // Start is called before the first frame update
    void Start()
    {
        buttons = transform.Find("ButtonCollection");
        sliders = transform.Find("Sliders");
        if (planePrefab == null)
            planePrefab = GameObject.Find("PlanePrefab");
    }

    void Update()
    {
        BoxCollider bc = followObject.GetComponent<BoxCollider>();
        if (bc != null)
        {
            Vector3 destination = followObject.transform.position + new Vector3(0, 0.5f * followObject.transform.localScale.y * bc.size.y + 0.05f, -0.5f * followObject.transform.localScale.z * bc.size.z);
            transform.position = Vector3.Lerp(transform.position, destination, followLerpTime * Time.deltaTime);
        }
            
    }

    // Update is called once per frame
    public void ButtonRotate(Interactable button)
    {
        loader.rotate = button.IsToggled;
    }

    public void ButtonPlane()
    {
        if (planePrefab == null)
        {
            Debug.LogError("Plane Prefab not found.");
            return;
        }
        GameObject plane;
        plane = Instantiate(planePrefab, loader.transform.position, Quaternion.identity);
        plane.tag = "plane";
        
        plane.transform.parent = loader.transform;
    }

    public void ButtonSlider(Interactable button)
    {
        buttons.gameObject.SetActive(!button.IsToggled);
        sliders.gameObject.SetActive(button.IsToggled);
    }

    public void ButtonCrossSec()
    {
        if (UIManager.UIMan.crossSectionTransform == followObject.transform)
        {
            UIManager.UIMan.SetCrossSectionPlane(planePrefab.transform);
        }
        else
        {
            UIManager.UIMan.SetCrossSectionPlane(followObject.transform);
        }
    }

    public void ButtonDelete()
    {
        if (followObject.tag != "plane" || UIManager.UIMan.crossSectionTransform == followObject.transform)
        {
            UIManager.UIMan.SetCrossSectionPlane(planePrefab.transform);
        }
        Destroy(transform.parent.gameObject);
    }
}
