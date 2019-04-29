using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoleculeRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] xyz = GameObject.FindGameObjectsWithTag("xyz");
		GameObject[] wrl = GameObject.FindGameObjectsWithTag("wrl");
		GameObject[] myMolecule = xyz.Concat(wrl).ToArray();
        foreach (GameObject i in myMolecule) 
            i.transform.Rotate(Vector3.down * Time.deltaTime * 10.0f);
	}
}
