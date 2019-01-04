using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Forward_Handler : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData data){
		if(gameObject.tag == "b"){
			Application.LoadLevel("AssetBundleBuild");
		}
	}
}
