using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject []myMolecule = GameObject.FindGameObjectsWithTag("mc"); 
        foreach (GameObject i in myMolecule) 
            i.transform.Rotate(Vector3.down * Time.deltaTime * 10.0f);
	}
}
