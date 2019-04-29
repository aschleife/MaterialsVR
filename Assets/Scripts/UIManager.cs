using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/**
    Read manifest from local path or remote server, extract molecule names.
**/
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
        // use HashSet to avoid duplicates
        HashSet<string> temp = new HashSet<string>();

        /*
        // start a download in the background by calling WWW(url) which returns a new WWW object
        WWW www = new WWW(manifest);
        yield return www;
        // www.error may return null or empty string
        if(!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("There was a problem loading asset bundles.");
        }
        else{
            string stringFromFile = www.text;
            string begLine = "- Assets/Molecules/";
            stringFromFile = www.text;
            // split text into string list
            List<string> lines = new List<string>(stringFromFile.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
           foreach(var manifestLine in lines){
                if (manifestLine.Contains(begLine)){
                    string line = manifestLine.Remove(manifestLine.Length - 8);  // 8 = "_xyz.fbx".Length or "_wrl.fbx".Length 
                    // add new name at the end of the list
                    temp.Add(line.Remove(0, begLine.Length));
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
            string begLine = "- Assets/Molecules/";
            // split text into string list
            List<string> lines = new List<string>(stringFromFile.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
           foreach(var manifestLine in lines){
                if (manifestLine.Contains(begLine)){
                    string line = manifestLine.Remove(manifestLine.Length - 8);  // 8 = "_xyz.fbx".Length or "_wrl.fbx".Length 
                    // add new name at the end of the list
                    temp.Add(line.Remove(0, begLine.Length));
                }
            }
        }
        yield return stringFromFile;
        
        // convert temporary HashSet to List
        count = temp.Count;
        moleculeNames = temp.ToList();
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
