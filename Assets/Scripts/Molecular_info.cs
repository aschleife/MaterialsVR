using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Collect color and element information of xyz models. Used in atomic handler to carry color-element info.
**/
public class Molecular_info {
	private Dictionary<Color, List<Renderer>> elements;
	private GameObject molecule;

	public Molecular_info(){
		elements = new Dictionary<Color, List<Renderer>>();
		molecule = GameObject.FindGameObjectsWithTag("xyz")[0];
		Renderer[] objects = molecule.GetComponentsInChildren<Renderer>();
		foreach(Renderer i in objects){
			Color color = i.material.GetColor("_Color");
			if(elements.ContainsKey(color)){
				List<Renderer> atoms = elements[color];
				atoms.Add(i);
			}
			else{ // didn't have this color yet
				List<Renderer> atoms = new List<Renderer>();
				atoms.Add(i);
				elements.Add(color, atoms);
			}
		}
		// output color -> element info
		// foreach(var pair in elements){
		// 	Debug.Log(pair.Key, pair.Value[0].name);  // color, element name
		// }
	}

	public Dictionary<Color, List<Renderer>> load_info(){
		return elements;
	}
}
