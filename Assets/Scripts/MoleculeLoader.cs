using System.Collections;
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
    private string url = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/WSAPlayer/molecules";
    private string keyword = "Ball";
    private string objectName;
    private bool isIsosurface;
    //[SerializeField] ToggleIndicator indicator;
    [SerializeField] Loader loader;

    private AssetBundle assetBundle;
    private GameObject molecule;
    private BoxCollider bc;

    // Use this for initialization
    public IEnumerator Start(){
        bc = loader.GetComponent<BoxCollider>() as BoxCollider;

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
        }
        
        molecule = Instantiate(assetBundle.LoadAsset(objectName + ".fbx")) as GameObject;
        molecule.transform.SetParent(transform);
        //indicator.Toggle(molecule);
        Vector3 size = new Vector3(0.15f, 0.15f, 0.15f);
        // coordinate with camera
        Vector3 slideRight = new Vector3(0, 0.3f, 3.11f);
        Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
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
        MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
        Bounds bbox = objects[0].bounds;
        
        //Vector3 minBound = objects[0].gameObject.transform.position;
        //Vector3 maxBound = objects[0].gameObject.transform.position;
        foreach (MeshRenderer i in objects){
            GameObject atom = i.gameObject;
            // add Atomic_Handler
            Atomic_Handler ah = atom.AddComponent<Atomic_Handler>() as Atomic_Handler;
            // add EventTrigger
            EventTrigger trigger_atom = atom.AddComponent<EventTrigger>() as EventTrigger;
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
            if(atom.ToString().Contains(keyword)){
                SphereCollider collider_atom = atom.AddComponent<SphereCollider>() as SphereCollider;
                bbox.Encapsulate(i.bounds);
                Debug.Log("Bounds" + bbox);
            }
            
            //for (int v=0; v<3; v++)
            //{
            //    if (atom.transform.position[v] < minBound[v])
            //        minBound[v] = atom.transform.position[v];
            //    if (atom.transform.position[v] > maxBound[v])
            //        maxBound[v] = atom.transform.position[v];
            //}
            Debug.Log("Drawing molecule" + i);
            yield return null;
        }
        bc.size = bbox.size;
        bc.center = bbox.center - transform.position;
        yield return null;
    }

    public void Unload()
    {
        Destroy(molecule);
    }

    public void Update(){
    }

}
