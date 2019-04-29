using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
    Load name-specified molecules for the main page. Get name and rotation status from objMessage.
    For xyz models, attach with movement handler and atomic handler.
    For wrl models, attach with movement handler. Need a way to turn opaque plates into transparent plates.
**/
public class rotational : MonoBehaviour
{
    private string url = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules";
    private string keyword1 = "Ball";
    private string keyword2 = "Shape_IndexedFaceSet";
    private string objectName;
    public GameObject myCanvas;
    public GameObject nameText;
    
    // Use this for initialization
    public IEnumerator Start(){
        objectName = objMessage.unLoadMessage();
        myCanvas = GameObject.Find("Canvas");
        nameText = GameObject.Find("Molecule_Name");
        nameText.GetComponent<Text>().text = objectName;
        yield return StartCoroutine(LoadObject());
    }

    public IEnumerator LoadObject(){
        // may need unload assetbundle somewhere
        while (!Caching.ready)
            yield return null;
        /*
        // load assetBundle from remote server
        WWW www = WWW.LoadFromCacheOrDownload(url, 1);
        yield return www;
        AssetBundle assetBundle = www.assetBundle;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("There was a problem loading asset bundles.");
        }
        */
        
        // load assetBundle from local path
        string url = Application.dataPath + "/../AssetBundles/Android/molecules";
        var assetBundle = AssetBundle.LoadFromFile(url);
        if (assetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
        }
        
        // process XYZ ones
        GameObject molecule1 = Instantiate(assetBundle.LoadAsset(objectName + "_xyz.fbx")) as GameObject;
        Vector3 slideRight = new Vector3(10.0f, 1.6f, 20.0f);
        processMolecule(molecule1, slideRight, "xyz");
        // process WRL ones
        GameObject molecule2 = Instantiate(assetBundle.LoadAsset(objectName + "_wrl.fbx")) as GameObject;
        Vector3 slideLeft = new Vector3(-10.0f, 1.6f, 20.0f);
        processMolecule(molecule2, slideLeft, "wrl");
        // unload assetBundle
        assetBundle.Unload(false);
        yield return assetBundle;
    }

    public void Update(){
        GameObject []xyz = GameObject.FindGameObjectsWithTag("xyz");
        if(objMessage.loadBoolean() == true){  // rotate
            foreach (GameObject i in xyz) 
                i.transform.Rotate(Vector3.down * Time.deltaTime * 10.0f);
        }
        else{  // freeze
            foreach (GameObject i in xyz) 
                i.transform.Rotate(Vector3.zero);
        }
        GameObject []wrl = GameObject.FindGameObjectsWithTag("wrl");
        if(objMessage.loadBoolean() == true){  // rotate
            foreach (GameObject i in wrl) 
                i.transform.Rotate(Vector3.down * Time.deltaTime * 10.0f);
        }
        else{  // freeze
            foreach (GameObject i in wrl) 
                i.transform.Rotate(Vector3.zero);
        }
    }

    public void processMolecule(GameObject molecule, Vector3 position, string tag){
        Vector3 size = new Vector3(0.7f, 0.7f, 0.7f);
        // coordinate with camera
        Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
        molecule.transform.localScale = size;
        molecule.transform.position = position;
        molecule.tag = tag;
        // set molecule parent and set relative position
        // molecule.transform.SetParent(myCanvas.transform, false);
        // add Molecular_Handler
        Molecular_Handler mh = molecule.AddComponent<Molecular_Handler>() as Molecular_Handler;
        // add EventTrigger
        EventTrigger trigger_molecule = molecule.AddComponent<EventTrigger>() as EventTrigger;
        // create Grab and Release events
        EventTrigger.Entry entry_grab = new EventTrigger.Entry();
        entry_grab.eventID = EventTriggerType.PointerDown;
        entry_grab.callback.AddListener((data) => { mh.OnGrab(); });
        trigger_molecule.triggers.Add(entry_grab);
        EventTrigger.Entry entry_release = new EventTrigger.Entry();
        entry_release.eventID = EventTriggerType.PointerUp;
        entry_release.callback.AddListener((data) => { mh.OnRelease(); });
        trigger_molecule.triggers.Add(entry_release);
        // for each atom, only for xyz
        if(tag == "xyz"){
            MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer i in objects){
                GameObject atom = i.gameObject;
                // add Sphere Collider
                if(atom.ToString().Contains(keyword1)){  // check if atoms
                    SphereCollider collider_atom = atom.AddComponent<SphereCollider>() as SphereCollider;
                    // add Atomic_Handler
                    Atomic_Handler ah = atom.AddComponent<Atomic_Handler>() as Atomic_Handler;
                    // add EventTrigger
                    EventTrigger trigger_atom = atom.AddComponent<EventTrigger>() as EventTrigger;
                    // create Enter and Exit events for components
                    EventTrigger.Entry entry_enter = new EventTrigger.Entry();
                    entry_enter.eventID = EventTriggerType.PointerEnter;
                    entry_enter.callback.AddListener((data) => { ah.OnEnter(); });
                    trigger_atom.triggers.Add(entry_enter);
                    EventTrigger.Entry entry_exit = new EventTrigger.Entry();
                    entry_exit.eventID = EventTriggerType.PointerExit;
                    entry_exit.callback.AddListener((data) => { ah.OnExit(); });
                    trigger_atom.triggers.Add(entry_exit);
                    EventTrigger.Entry entry_click = new EventTrigger.Entry();
                    entry_click.eventID = EventTriggerType.PointerClick;
                    entry_click.callback.AddListener((data) => { ah.OnClick(); });
                    trigger_atom.triggers.Add(entry_click);
                }
            }
        }
        // for each mirror, only for wrl
        // need more research on transparent/shader/render mode
        // if(tag == "wrl"){
        //     MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
        //     foreach (MeshRenderer i in objects){
        //         GameObject mirror = i.gameObject;
        //         // turn transparent
        //         if(mirror.ToString().Contains(keyword2)){
        //             Color color = mirror.GetComponent<MeshRenderer>().material.color;
        //             color.a = 0.25f;
        //             mirror.GetComponent<MeshRenderer>().material.color = color;
        //         }
        //     }
        // }
    }

}
