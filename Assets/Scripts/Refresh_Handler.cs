using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
    Handler for refresh handler on the main page. Ability to clean cache, download a new assetbundle and return to the menu page.
    Only is necessary when assetbundle on the server is updated. For local update, do nothing.
    The reason that we need to clean cache is that we store assetbundle locally for fast loading speed. 
    However, if we need a newer one, the same version number of two assetbundle will crash. May use other method to solve the conflict. See documentation.
**/
public class Refresh_Handler : MonoBehaviour, IPointerClickHandler {
    private string url = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules";

	public void OnPointerClick(PointerEventData data){
        if(gameObject.tag == "b"){
            // clean cache before quit, make sure we can download it next time; for convenience of new Assetbunld
            if (Caching.CleanCache()){
                Debug.Log("Successfully cleaned the cache.");
            }
           else{
                Debug.Log("Cache is being used.");
            }
            /*
            // download a new AssetBundle from server
            WWW www = WWW.LoadFromCacheOrDownload(url, 1);
            Debug.Log("Successfully download Assetbunld.");
            */
            
            // return to the main menu
            SceneManager.LoadScene("New_Menu");
        }
    }
}
