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
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;

public class Loader : MonoBehaviour
{
    [SerializeField] TextMeshPro moleculeName_text;
    private MoleculeLoader molecule_loader;
    private Isosurface isosurface;
    [SerializeField] GameObject optionBar;

    [SerializeField] GameObject isosurface_slider;
    [SerializeField] GameObject poly_button;
    
    [SerializeField] public float target_size = 100f;
    private Vector3 def_position;
    public BoxCollider bc;
    private Vector3 bc_center;
    private Vector3 bc_size;
    private BoundsControl bbox;
    public bool rotate;

    // movement components
    NearInteractionGrabbable nearInteractionGrabbable;
    BoundsControl boundsControl;

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
    void Awake()
    {
        transform.parent.localScale = Vector3.zero;
        molecule_loader = GetComponentInChildren<MoleculeLoader>();
        isosurface = GetComponentInChildren<Isosurface>();
        if (molecule_loader == null || isosurface == null)
            Debug.LogError("Loader modules not found!");

        def_position = transform.position;

        // Initialize progress
        //progressBar = GameObject.Find("ProgressIndicatorLoadingBar");
        //progressText = GameObject.Find("MessageText").GetComponent<TextMeshPro>();
        //progressIndicator = progressBar.GetComponent<IProgressIndicator>();
        progress = new Dictionary<string, float>();

        //progressBar.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.down, Time.deltaTime * 10.0f);
        }
    }

    public IEnumerator LoadObject(string objectName, string tag, bool loadFromLocal = false)
    {
        
        //UnLoadObject();

        // Init Progress Bar
        progress["dl"] = 0.0f;
        progress["rd"] = 0.0f;
        
        //progressIndicator.OpenAsync();
        UpdateProgress();

        // Init Loader
        isosurface.gameObject.SetActive(false);
        molecule_loader.gameObject.SetActive(false);
        isosurface_slider.SetActive(false);
        molecule_loader.atomName.SetActive(false);
        poly_button.SetActive(false);

        if (loadFromLocal)
        {
            moleculeName_text.text = System.IO.Path.GetFileNameWithoutExtension(objectName);
        }
        else
        {
            moleculeName_text.text = objectName;
        }
        
        transform.position = def_position;

        switch (tag)
        {
            case "b_iso":
                Debug.Log("Loading isosurface: " + objectName);
                isosurface.gameObject.SetActive(true);
                isosurface_slider.SetActive(true);
                yield return isosurface.Load(objectName, loadFromLocal);
                break;
            case "b_mol":
                Debug.Log("Loading molecule: " + objectName);
                molecule_loader.gameObject.SetActive(true);
                molecule_loader.atomName.SetActive(true);
                poly_button.SetActive(true);
                yield return molecule_loader.Load(objectName);
                break;
            default:
                Debug.LogError("Unrecognized tag");
                yield return null;
                break;
        }

        while (progress["all"] < 1.0f)
        {
            yield return null;
        }
        // Complete
        yield return new WaitForSeconds(0.1f);
        transform.parent.localScale = Vector3.one;
        Debug.Log("Loader Complete");
        DrawBBox();
        //progressIndicator.CloseAsync();

    }

    public static void UpdateProgress(string key, float value)
    {
        progress[key] = value;
        UpdateProgress();
    }

    private static void UpdateProgress()
    {
        progress["all"] = 0.4f * progress["dl"] + 0.6f * progress["rd"];
        //progressIndicator.Progress = progress["all"];
        //if (progress["dl"] < 1.0f)
        //    progressText.text = "Downloading...";
        //else
        //    progressText.text = "Loading...";
    }

    private void DrawBBox()
    {
        bbox = gameObject.AddComponent<BoundsControl>();
        //// Make the scale handles large
        //bbox.ScaleHandleSize = 0.08f;
        //bbox.BoxMaterial = BoxMaterial;
        //bbox.BoxGrabbedMaterial = BoxGrabbedMaterial;
        //bbox.HandleMaterial = HandleMaterial;
        //bbox.HandleGrabbedMaterial = HandleGrabbedMaterial;
    }

    public void SetBoxParam(Vector3 center, Vector3 size)
    {
        bc_center = center;
        bc_size = size;
    }

    public void ToggleMovement(Interactable button)
    {
        if (boundsControl == null)
            boundsControl = GetComponent<BoundsControl>();
        if (nearInteractionGrabbable == null)
            nearInteractionGrabbable = GetComponent<NearInteractionGrabbable>();
        
        boundsControl.enabled = !button.IsToggled;
        nearInteractionGrabbable.enabled = !button.IsToggled;
    }

    public void SetOptionBarTransform()
    {
        //optionBar.transform.localPosition = bc_center + (Vector3.down + Vector3.forward) * bc_size.magnitude / 2;
    }

        public void RadiusSliderUpdate(SliderEventData eventData)
    {
        float radius_scale = eventData.NewValue * 2;
        isosurface.RadiusUpdate(radius_scale);
        molecule_loader.RadiusUpdate(radius_scale);
    }
}
