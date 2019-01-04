using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class rotational : MonoBehaviour
{
    private string url = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules";
    private string keyword = "Ball";
    private string objectName;
    public GameObject myCanvas;
    public GameObject nameText;
    public GameObject cube;
    
    // Use this for initialization
    public IEnumerator Start(){
        cube = GameObject.Find("Cube");
        objectName = objMessage.unLoadMessage();
        myCanvas = GameObject.Find("Canvas");
        nameText = GameObject.Find("Molecule_Name");
        nameText.GetComponent<Text>().text = objectName;
        yield return StartCoroutine(LoadObject());
    }

    public IEnumerator LoadObject(){
        // load assetBundle from remote server
        WWW www = new WWW(url);
        yield return www;
        AssetBundle assetBundle = www.assetBundle;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("There was a problem loading asset bundles.");
        }
        /*
        // load assetBundle from local path
        string url = Application.dataPath + "/../AssetBundles/Android/molecules";
        var assetBundle = AssetBundle.LoadFromFile(url);
        if (assetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
        }
        */
        GameObject molecule = Instantiate(assetBundle.LoadAsset(objectName + ".fbx")) as GameObject;
        Vector3 size = new Vector3(1f, 1f, 1f);
        // coordinate with camera
        Vector3 slideRight = new Vector3(0.0f, 1.6f, 20.0f);
        Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
        molecule.transform.localScale = size;
        molecule.transform.position = slideRight;
        molecule.tag = "edmc";
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
        // for each atom
        MeshRenderer[] objects = molecule.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer i in objects){
            GameObject atom = i.gameObject;
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
            // add Sphere Collider
            if(atom.ToString().Contains(keyword)){
                SphereCollider collider_atom = atom.AddComponent<SphereCollider>() as SphereCollider;
            }
        }
        assetBundle.Unload(false);
        yield return assetBundle;
    }

    public void Update(){
        GameObject []copy = GameObject.FindGameObjectsWithTag("edmc");
        if(objMessage.loadBoolean() == true){
            foreach (GameObject i in copy) 
                i.transform.Rotate(Vector3.down * Time.deltaTime * 10.0f);
        }
        else{
            foreach (GameObject i in copy) 
                i.transform.Rotate(Vector3.zero);
        }
    }
}
