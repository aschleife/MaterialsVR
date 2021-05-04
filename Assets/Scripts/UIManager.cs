using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit.UI;
using SFB;
using TMPro;
using SimpleFileBrowser;

public class UIManager : MonoBehaviour
{
    public static UIManager UIMan;

    public static string molecule_url = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/WSAPlayer/molecules";
    public static string chgcar_url = "http://web.engr.illinois.edu/~schleife/vr_app/charge_density/";
    // absolute path storing the assetbundle manifest
    private string manifest = molecule_url + ".manifest";
    // asset bundle
    private AssetBundle assetBundle;
    // number of loaded molecules in assetbundle 
    public int count = 0;
    // list of loaded molecules name, note static
    public static List<string> moleculeNames = new List<string>();
    // index of list where CHGCAR files start 
    public int isosurfaceStart = 0;
    // true if initialized
    public bool init = false;
    // active list tag
    public static string activeTag = "b_mol";
    // menu
    //public static GameObject menu;
    //private Vector3 menu_scale;
    // loader
    public GameObject loader;
    //
    private GameObject objectContainer;
    // Tooltip List
    private ToolTipSpawner [] toolTipSpawners;
    // transform used for cross section and spawning
    public Transform crossSectionTransform;
    public Vector3 planePosition;

    [SerializeField]
    private GameObject moleculePrefab;

    private void Awake()
    {
        if (UIMan != null)
        {
            Destroy(this);
        }
        else
        {
            UIMan = this;
        }
    }

    // Use this for initialization
    public IEnumerator Start(){
        //menu = GameObject.Find("Menu_Canvas");
        //menu_scale = menu.transform.localScale;
        loader = GameObject.Find("Loader");
        objectContainer = GameObject.Find("ObjectContainer");
        crossSectionTransform = GameObject.Find("PlanePrefab").transform;

        // Find all tooltip spawners
        toolTipSpawners = FindObjectsOfType<ToolTipSpawner>();
        foreach (ToolTipSpawner t in toolTipSpawners)
        {
            t.FocusEnabled = false;
        }

        // start a download in the background by calling WWW(url) which returns a new WWW object
        UnityWebRequest uwr = UnityWebRequest.Get(manifest);
        yield return uwr.SendWebRequest();
        // www.error may return null or empty string
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log("There was a problem loading asset bundles.");
        }
        else
        {
            string stringFromFile = uwr.downloadHandler.text;
            string begLine = "- Assets/Molecules/";
        
            // split text into string list
            List<string> lines = new List<string>(stringFromFile.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
           foreach(var manifestLine in lines){
                if (manifestLine.Contains(begLine)){
                    count++;
                    Debug.Log("Manifest Read:" + count);
                    string line = manifestLine.Remove(manifestLine.Length - ".fbx".Length);
                    // add new name at the end of the list
                    moleculeNames.Add(line.Remove(0, begLine.Length));
                }
            }
        }
        // Load AssetBundle

        // may need unload assetbundle somewhere
        while (!Caching.ready)
            yield return null;
        Caching.ClearCache();
        while (!Caching.ready)
            yield return null;
        // Load AssetBundle from remote
        using (uwr = UnityWebRequestAssetBundle.GetAssetBundle(molecule_url, 1, 0))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
        }

        /*
        // load assetBundle from local path
        string url = Application.dataPath + "/../AssetBundles/Android/molecules.manifest";
        string stringFromFile = System.IO.File.ReadAllText(@url);
        if (string.IsNullOrEmpty(stringFromFile)) {
            Debug.Log("Error in retrieving manifest file.");
        }
        else{
            string begLine = "- Assets/Molecules/";
            // split text into string list
            List<string> lines = new List<string>(stringFromFile.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
           foreach(var manifestLine in lines){
                if (manifestLine.Contains(begLine)){
                    count++;
                    string line = manifestLine.Remove(manifestLine.Length - ".fbx".Length);
                    // add new name at the end of the list
                    moleculeNames.Add(line.Remove(0, begLine.Length));
                }
            }
        }
        yield return stringFromFile;
        */

        // Load Isosurface
        isosurfaceStart = count;
        uwr = UnityWebRequest.Get(chgcar_url + "filelist.txt"); ;
        yield return uwr.SendWebRequest();
        // www.error may return null or empty string
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log("There was a problem loading chgcar files.");
        }
        else
        {
            string stringFromFile = uwr.downloadHandler.text;
            string begLine = "CHGCAR: ";
            // Save file
            string local_path = Application.persistentDataPath + "/CHGCAR";
            if (!Directory.Exists(local_path))
                Directory.CreateDirectory(local_path);
            string savePath = local_path + "filelist.txt";
            File.WriteAllText(savePath, uwr.downloadHandler.text);
            // Read filenames
            List<string> lines = new List<string>(stringFromFile.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
            foreach (string line in lines)
            {
                if (line.Contains(begLine))
                {
                    count++;
                    Debug.Log("CHGCAR Read:" + count);
                    string fname = line.Remove(line.Length - ".vasp".Length);

                    // add new name at the end of the list
                    moleculeNames.Add(fname.Remove(0, begLine.Length));
                }
            }

        }

        // load from local
            //string isoPath = Application.dataPath + "/Isosurface/CHGCAR/";
            //string[] fileList = Directory.GetFiles(isoPath, "*.vasp");
            //foreach (String file in fileList)
            //{
            //    count++;
            //    Debug.Log("CHGCAR Read:" + count);
            //    // add new name at the end of the list
            //    string line = file.Remove(file.Length - ".vasp".Length);
            //    moleculeNames.Add(line.Remove(0, isoPath.Length));
            //}

            init = true;
    }

    // Update is called once per frame
    void Update(){
        // Update Cross Section
        UpdateCrossSection();
    }

    //Reloads the Level
    public void Reload(){
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //
    //private async void ToggleIndicator(GameObject obj)
    //{
    //    await indicator.AwaitTransitionAsync();

    //    switch (indicator.State)
    //    {
    //        case ProgressIndicatorState.Closed:
    //            obj.SetActive(false);
    //            indicatorObject.SetActive(true);
    //            await indicator.OpenAsync();
    //            break;

    //        case ProgressIndicatorState.Open:
    //            await indicator.CloseAsync();
    //            indicatorObject.SetActive(false);
    //            obj.SetActive(true);
    //            break;
    //    }
    //}

    // Button Functions

    public void ToggleTag(AddMRTKButtons addMRTKButtons, string switch_to_tag)
    {
        addMRTKButtons.setActiveTag(switch_to_tag);
        Debug.Log("Menu Toggled");
    }

    public void ToggleRotate(Interactable button)
    {
        if (button.IsToggled)
            objMessage.pause();
        else
            objMessage.revolve();
    }

    public void ToggleTooltip(Interactable button)
    {
        foreach (ToolTipSpawner t in toolTipSpawners)
        {
            t.FocusEnabled = button.IsToggled;
        }
    }

    public void LoadObject(GameObject card)
    {
        LoadObject(card.GetComponentInChildren<TextMeshPro>().text, card.tag);
    }

    public void LoadObject(string objectName, string tag, bool loadFromLocal = false)
    {
        Debug.Log("Loading: " + objectName);
        GameObject new_loader = Instantiate(objectContainer);
        new_loader.transform.position = loader.transform.position;
        new_loader.SetActive(true);
        StartCoroutine(new_loader.GetComponentInChildren<Loader>().LoadObject(objectName, tag, loadFromLocal));
    }


    public GameObject LoadAssetFromBundle(string objectName)
    {
        return Instantiate(assetBundle.LoadAsset(objectName + ".fbx")) as GameObject;
    }

    public void SetCrossSectionPlane(Transform plane)
    {
        crossSectionTransform = plane;
    }

    private void UpdateCrossSection()
    {
        Shader.SetGlobalVector("_PlanePosition", crossSectionTransform.position);
        Shader.SetGlobalVector("_PlaneNormal", crossSectionTransform.forward);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }

    public void OpenFile()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Allow multiple selection: true
        // Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, true, null, "Load File", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);
            string filePath = FileBrowser.Result[0];
            string fileName = Path.GetFileName(filePath);
            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

            objMessage.loadMessage(fileName, gameObject.tag);
            objMessage.revolve();
            LoadObject(filePath, "b_iso", true);
        }
    }
}
