using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Input;

public class Forward_Handler : MonoBehaviour, IMixedRealityPointerHandler
{

	public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
		if(gameObject.tag == "b"){
			SceneManager.LoadScene("AssetBundleBuild");
		}
	}

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }
}
