using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Molecular_Handler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	public void OnGrab(){
		Transform pointerTransform = GvrPointerInputModule.Pointer.PointerTransform;
		transform.SetParent(pointerTransform, true);
	}

	public void OnRelease(){
		transform.SetParent(null, true);
	}
}
