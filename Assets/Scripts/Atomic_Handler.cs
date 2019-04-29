using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
	Handler for xyz models. Ability to deal with atom colors and element information.
	Get color-element from Molecular info object.
**/
public class Atomic_Handler : MonoBehaviour {
	private Renderer _renderer;
	private Dictionary<Color, List<Renderer>> elements;
	private Color original;
	private GameObject[] array;
	private GameObject molecule;
	private static int count;

	// Use this for initialization
	void Start () {
		count = 0;
		Molecular_info temp = new Molecular_info();
		elements = temp.load_info();
		array = GameObject.FindGameObjectsWithTag("xyz");
		_renderer = gameObject.GetComponent<Renderer>();
		original = _renderer.material.GetColor("_Color");
	}

	public void Update () {
		if(array == null || molecule == null){
			array = GameObject.FindGameObjectsWithTag("xyz");
		}
		else{
			molecule = array[0];
			_renderer = gameObject.GetComponent<Renderer>();
			original = _renderer.material.GetColor("_Color");
		}
	}
	public void OnEnter(){
		_renderer.material.color = Color.cyan;
		//Debug.Log(gameObject.name);
	}

	public void OnExit(){
		_renderer.material.color = original;
		//Debug.Log(gameObject.name);
	}

	public void OnClick(){
		List<Renderer> sameColor = elements[original];
		if(count % 2 == 0){
			// output color -> element info
			Debug.Log(original);
			Debug.Log(sameColor[0].name);
			foreach(Renderer i in sameColor){
				i.material.color = Color.magenta;
			}
		}
		else{
			foreach(Renderer i in sameColor){
				i.material.color = original;
			}
		}
		count++;
	}
}