﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class MoleculeLoader : MonoBehaviour
{
    private string url = UIManager.molecule_url;
    private string keyword_atom = "Ball";
    private string keyword_poly = "Shape_IndexedFaceSet";
    private string objectName;
    //[SerializeField] ToggleIndicator indicator;
    [SerializeField] Loader loader;

    private AssetBundle assetBundle;
    private GameObject molecule;
    private MeshRenderer[] mesh_array;

    // Use this for initialization
    public IEnumerator Start(){

        // may need unload assetbundle somewhere
        while (!Caching.ready)
            yield return null;
        Caching.ClearCache();
        while (!Caching.ready)
            yield return null;
        // Load AssetBundle from remote
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, 1, 0))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
        }
    }

    public IEnumerator Load(string objectName)
    {
        if (assetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
        }
        else
        {
            Debug.Log("Loading molecule");
            Loader.UpdateProgress("dl", 1.0f);
        }
        
        molecule = Instantiate(assetBundle.LoadAsset(objectName + ".fbx")) as GameObject;
        molecule.transform.SetParent(transform);
        //indicator.Toggle(molecule);
        Vector3 size = new Vector3(0.15f, 0.15f, 0.15f);
        // coordinate with camera
        Vector3 slideRight = new Vector3(0, 0.3f, 3.11f);
        molecule.transform.localScale = size;
        molecule.transform.position = slideRight;
        molecule.tag = "edmc";
        // set molecule parent and set relative position
        // molecule.transform.SetParent(myCanvas.transform, false);
        // add Molecular_Handler
        //Molecular_Handler mh = molecule.AddComponent<Molecular_Handler>() as Molecular_Handler;
        // add EventTrigger
        //EventTrigger trigger_molecule = molecule.AddComponent<EventTrigger>() as EventTrigger;
        // create Grab and Release events
        //EventTrigger.Entry entry_grab = new EventTrigger.Entry();
        //entry_grab.eventID = EventTriggerType.PointerDown;
        //entry_grab.callback.AddListener((data) => { mh.OnGrab(); });
        //trigger_molecule.triggers.Add(entry_grab);
        //EventTrigger.Entry entry_release = new EventTrigger.Entry();
        //entry_release.eventID = EventTriggerType.PointerUp;
        //entry_release.callback.AddListener((data) => { mh.OnRelease(); });
        //trigger_molecule.triggers.Add(entry_release);
        // for each atom
        mesh_array = molecule.GetComponentsInChildren<MeshRenderer>();
        Bounds bbox = mesh_array[0].bounds;

        for (int i = 0; i < mesh_array.Length; i++) {
            MeshRenderer m = mesh_array[i]; 

            GameObject atom = m.gameObject;
            // add Atomic_Handler
            Atomic_Handler ah = atom.AddComponent<Atomic_Handler>() as Atomic_Handler;
            // add EventTrigger
            //EventTrigger trigger_atom = atom.AddComponent<EventTrigger>() as EventTrigger;
            // create Enter and Exit events for components
            //EventTrigger.Entry entry_enter = new EventTrigger.Entry();
            //entry_enter.eventID = EventTriggerType.PointerEnter;
            //entry_enter.callback.AddListener((data) => { ah.OnEnter(); });
            //trigger_atom.triggers.Add(entry_enter);
            //EventTrigger.Entry entry_exit = new EventTrigger.Entry();
            //entry_exit.eventID = EventTriggerType.PointerExit;
            //entry_exit.callback.AddListener((data) => { ah.OnExit(); });
            //trigger_atom.triggers.Add(entry_exit);
            //EventTrigger.Entry entry_click = new EventTrigger.Entry();
            //entry_click.eventID = EventTriggerType.PointerClick;
            //entry_click.callback.AddListener((data) => { ah.OnClick(); });
            //trigger_atom.triggers.Add(entry_click);
            // add Sphere Collider
            if(atom.ToString().Contains(keyword_atom)){
                SphereCollider collider_atom = atom.AddComponent<SphereCollider>() as SphereCollider;
                collider_atom.center = Vector3.zero;
                collider_atom.radius = 0.01f;
                bbox.Encapsulate(m.bounds);
            }
            
            Loader.UpdateProgress("rd", i / (mesh_array.Length - 1));
            yield return null;
        }
        UIManager.loader.GetComponent<Loader>().DrawBoxCollider(bbox.center - transform.position, bbox.size);
    }

    public void RadiusUpdate(float radius_scale)
    {
        if (mesh_array == null) return;
        foreach (MeshRenderer i in mesh_array)
        {
            GameObject atom = i.gameObject;
            if (atom.ToString().Contains(keyword_atom))
            {
                atom.transform.localScale = radius_scale * Vector3.one;
            }
        }
    }

    public void TogglePoly(Interactable button)
    {
        if (mesh_array == null) return;
        foreach (MeshRenderer i in mesh_array)
        {
            if (i.gameObject.ToString().Contains(keyword_poly))
                // show plates
                i.enabled = !button.IsToggled;
        }
    }

    public void Unload()
    {
        StopAllCoroutines();
        Destroy(molecule);
    }

    public void Update(){
    }

}
