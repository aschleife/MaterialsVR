using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using System.Reflection;
using System.Data;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using TMPro;

public class Loader : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] MoleculeLoader molecule_loader;
    [SerializeField] Isosurface isosurface;
    [SerializeField] GameObject isosurface_slider;
    [SerializeField] public float target_size = 100f;
    private Vector3 def_position;
    private BoxCollider bc;
    private BoundingBox bbox;

    // progress bar
    private static GameObject progressBar;
    private static TextMeshPro progressText;
    private static IProgressIndicator progressIndicator;
    private static Dictionary<string, float> progress;

    // BoundingBox materials
    [SerializeField] Material BoxMaterial;
    [SerializeField] Material BoxGrabbedMaterial;
    [SerializeField] Material HandleMaterial;
    [SerializeField] Material HandleGrabbedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        def_position = transform.position;
        transform.localScale = Vector3.zero;
        progressBar = GameObject.Find("ProgressIndicatorLoadingBar");
        progressText = GameObject.Find("MessageText").GetComponent<TextMeshPro>();
        progressIndicator = progressBar.GetComponent<IProgressIndicator>();
        progress = new Dictionary<string, float>();

        progressBar.SetActive(false);
        bc = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objMessage.loadBoolean() == true)
        {   
            transform.Rotate(Vector3.down, Time.deltaTime * 10.0f);
        }
        else
        {
            transform.Rotate(Vector3.zero);
        }
    }

    public IEnumerator LoadObject()
    {
        
        UnLoadObject();

        // Init Progress Bar
        progress["dl"] = 0.0f;
        progress["rd"] = 0.0f;
        
        progressIndicator.OpenAsync();
        UpdateProgress();

        // Init Loader
        isosurface.gameObject.SetActive(false);
        molecule_loader.gameObject.SetActive(false);
        isosurface_slider.SetActive(false);
        
        string objectName = objMessage.unLoadMessage();
        nameText.text = objectName;
        transform.position = def_position;

        if (objMessage.unLoadIsosurface())
        {
            Debug.Log("Loading isosurface: " + objectName);
            isosurface.gameObject.SetActive(true);
            isosurface_slider.SetActive(true);
            yield return isosurface.Load(objectName);
        }
        else
        {
            Debug.Log("Loading molecule: " + objectName);
            molecule_loader.gameObject.SetActive(true);
            yield return molecule_loader.Load(objectName);
        }

        while (progress["all"] < 1.0f)
        {
            yield return null;
        }
        // Complete
        yield return new WaitForSeconds(0.5f);
        transform.localScale = Vector3.one;
        DrawBBox();
        progressIndicator.CloseAsync();
        
    }

    public static void UpdateProgress(string key, float value)
    {
        progress[key] = value;
        UpdateProgress();
    }

    private static void UpdateProgress()
    {
        progress["all"] = 0.4f * progress["dl"] + 0.6f * progress["rd"];
        progressIndicator.Progress = progress["all"];
        if (progress["dl"] < 1.0f)
            progressText.text = "Downloading...";
        else
            progressText.text = "Loading...";
    }

    private void DrawBBox()
    {
        bbox = gameObject.AddComponent<BoundingBox>();
        // Make the scale handles large
        bbox.ScaleHandleSize = 0.08f;
        bbox.BoxMaterial = BoxMaterial;
        bbox.BoxGrabbedMaterial = BoxGrabbedMaterial;
        bbox.HandleMaterial = HandleMaterial;
        bbox.HandleGrabbedMaterial = HandleGrabbedMaterial;
    }

    public void DrawBoxCollider(Vector3 center, Vector3 size)
    {
        bc = gameObject.AddComponent<BoxCollider>();
        bc.center = center;
        bc.size = size;
    }

    public void ToggleMovement(Interactable button)
    {
        if (bc != null)
            bc.enabled = button.IsToggled;
        GetComponent<NearInteractionGrabbable>().enabled = button.IsToggled;
        GetComponent<ObjectManipulator>().enabled = button.IsToggled;
        GetComponent<BoundingBox>().enabled = button.IsToggled;
    }

    public void UnLoadObject()
    {
        transform.localScale = Vector3.zero;
        StopAllCoroutines();
        isosurface.Unload();
        molecule_loader.Unload();
        if (bbox != null)
            Destroy(bbox);
        if (bc != null)
            Destroy(bc);
        foreach (Transform child in transform)
        {
            if (child.CompareTag("plane"))
                Destroy(child.gameObject);
        }
    }

        public void RadiusSliderUpdate(SliderEventData eventData)
    {
        float radius_scale = eventData.NewValue * 2;
        isosurface.RadiusUpdate(radius_scale);
        molecule_loader.RadiusUpdate(radius_scale);
    }
}
