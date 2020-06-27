using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit.UI;

public class UIManager : MonoBehaviour
{
    // absolute path storing the assetbundle manifest
    private string manifest = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/WSAPlayer/molecules.manifest";
    // number of loaded molecules in assetbundle 
    public int count = 0;
    // list of loaded molecules name, note static
    public static List<string> moleculeNames = new List<string>();
    // index of list where CHGCAR files start 
    public int isosurfaceStart = 0;
    // true if initialized
    public bool init = false;
    // Progress indicator
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject indicatorObject;
    private IProgressIndicator indicator;
    // active list tag
    public static string activeTag = "b_mol";

    // Use this for initialization
    public IEnumerator Start(){

        indicator = indicatorObject.GetComponent<IProgressIndicator>();
        ToggleIndicator(Canvas);
        // start a download in the background by calling WWW(url) which returns a new WWW object
        UnityWebRequest www = UnityWebRequest.Get(manifest);
        yield return www.SendWebRequest();
        // www.error may return null or empty string
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("There was a problem loading asset bundles.");
        }
        else
        {
            string stringFromFile = www.downloadHandler.text;
            string begLine = "- Assets/Molecules/";
            stringFromFile = www.downloadHandler.text;

            // using www
        //    WWW www = new WWW(manifest);
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.LogError("There was a problem loading asset bundles.");
        //    yield return null;
        //}
        //else
        //{
        //    string stringFromFile = www.text;
        //    string begLine = "- Assets/Molecules/";
        //    stringFromFile = www.text;
        
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
        string isoPath = Application.dataPath + "/Isosurface/CHGCAR/";
        string[] fileList = Directory.GetFiles(isoPath, "*.vasp");
        foreach (String file in fileList)
        {
            count++;
            Debug.Log("CHGCAR Read:" + count);
            // add new name at the end of the list
            string line = file.Remove(file.Length - ".vasp".Length);
            moleculeNames.Add(line.Remove(0, isoPath.Length));
        }

        init = true;
        ToggleIndicator(Canvas);
    }

    // Update is called once per frame
    void Update(){

    }

    //Reloads the Level
    public void Reload(){
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //loads inputted level
    public void LoadLevel(string moleculeName){
        SceneManager.LoadScene("SPIN6.26");
    }

    public void LoadMLevel(BaseEventData data){

    }

    //
    private async void ToggleIndicator(GameObject obj)
    {
        await indicator.AwaitTransitionAsync();

        switch (indicator.State)
        {
            case ProgressIndicatorState.Closed:
                obj.SetActive(false);
                indicatorObject.SetActive(true);
                await indicator.OpenAsync();
                break;

            case ProgressIndicatorState.Open:
                await indicator.CloseAsync();
                indicatorObject.SetActive(false);
                obj.SetActive(true);
                break;
        }
    }

}
