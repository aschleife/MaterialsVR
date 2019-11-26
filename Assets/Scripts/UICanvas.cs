﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour {

    private Canvas canvas;

    // Use this for initialization
    void Start () {
        canvas = GetComponent<Canvas>();
        //canvas.worldCamera = GameObject.FindGameObjectWithTag("MixedRealityPlayspace").GetComponent<Camera>();
        canvas.worldCamera = GameObject.Find("UIRaycastCamera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
