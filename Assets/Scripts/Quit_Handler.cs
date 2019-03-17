using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Quit_Handler : MonoBehaviour, IPointerClickHandler {
	public void OnPointerClick(PointerEventData data){
        // if clicked at button
        // if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        if(gameObject.tag == "b"){
            // clean cache before quit, make sure we can download it next time; for convenience of new Assetbunld
            if (Caching.CleanCache()){
                Debug.Log("Successfully cleaned the cache.");
            }
           else{
                Debug.Log("Cache is being used.");
            }
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
