using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{
    // absolute path storing the assetbundle manifest
    private string manifest = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules.manifest";
    // number of loaded molecules in assetbundle 
    public int count = 0;
    // list of loaded molecules name, note static
    public static List<string> moleculeNames = new List<string>();

    // Use this for initialization
    public IEnumerator Start(){
        // start a download in the background by calling WWW(url) which returns a new WWW object
        UnityWebRequest www = UnityWebRequest.Get(manifest);
        yield return www.SendWebRequest();
        // www.error may return null or empty string
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("There was a problem loading asset bundles.");
        }
        else{
            string stringFromFile = www.downloadHandler.text;
            string begLine = "- Assets/Molecules/";
            stringFromFile = www.downloadHandler.text;
            // split text into string list
            List<string> lines = new List<string>(stringFromFile.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
           foreach(var manifestLine in lines){
                if (manifestLine.Contains(begLine)){
                    count++;
                    Debug.Log(count);
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

}
