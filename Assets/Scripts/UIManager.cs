using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;

public class UIManager : MonoBehaviour
{
    // absolute path storing the assetbundle manifest
    private string manifest = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules.manifest";
    // number of loaded molecules in assetbundle 
    public int count = 0;
    // list of loaded molecules name, note static
    public static List<string> moleculeNames = new List<string>();
    public GameObject player;

    // Use this for initialization
    public void Start(){
        // My thought: this two lines may generate errors
        // player = GameObject.Find("Player");
        // DontDestroyOnLoad(player);
        // start a new sequential processing/function/subroutine (only one is executing at any given time)
        StartCoroutine(ReadManifest());
    }

    public IEnumerator ReadManifest(){
        /*
        // start a download in the background by calling WWW(url) which returns a new WWW object
        WWW www = new WWW(manifest);
        yield return www;
        Debug.Log(string.IsNullOrEmpty(www.error));
        // www.error may return null or empty string
        if(string.IsNullOrEmpty(www.error)){
            Debug.Log("Error in retrieving manifest file.");
        }
        else{
            count = 0;
            string begLine = "- Assets/Molecules/";
            string stringFromFile = www.text;
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
        */
        // load assetBundle from local path
        string url = Application.dataPath + "/../AssetBundles/Android/molecules.manifest";
        string stringFromFile = System.IO.File.ReadAllText(@url);
        if (string.IsNullOrEmpty(stringFromFile)) {
            Debug.Log("Error in retrieving manifest file.");
        }
        else{
            count = 0;
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
        
    }

    // Update is called once per frame
    void Update(){

    }

    //Reloads the Level
    public void Reload(){
        Application.LoadLevel(Application.loadedLevel);
    }

    //loads inputted level
    public void LoadLevel(string moleculeName){
        Application.LoadLevel("SPIN6.26");
    }

    public void LoadMLevel(BaseEventData data){

    }

}
