using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atomic_Handler : MonoBehaviour {
	private Renderer _renderer;
	private Dictionary<Color, List<Renderer>> elements;
	private Color original;
	private GameObject[] array;
	private GameObject molecule;
	private static int count;
    private string atomName;

    // Use this for initialization
    void Start () {
		count = 0;
		Molecular_info temp = new Molecular_info();
		elements = temp.load_info();
		array = GameObject.FindGameObjectsWithTag("edmc");
		_renderer = gameObject.GetComponent<Renderer>();
		original = _renderer.material.GetColor("_Color");
        atomName = _renderer.material.name.Substring(0, _renderer.material.name.Length - 11);
	}

	public void Update () {
		if(array == null || molecule == null){
			array = GameObject.FindGameObjectsWithTag("edmc");
		}
		else{
			molecule = array[0];
			_renderer = gameObject.GetComponent<Renderer>();
			original = _renderer.material.GetColor("_Color");
		}
	}
	public void OnEnter(){
		_renderer.material.color = Color.cyan;
//		Debug.Log(gameObject);
	}

	public void OnExit(){
		_renderer.material.color = original;
//		Debug.Log(gameObject);
	}

	public void OnClick(){
        GameObject atomText = GameObject.Find("Atom_Name");
        atomText.GetComponent<Text>().text = atomName;
        atomText.GetComponent<Text>().color = original;

        List <Renderer> sameColor = elements[original];
		if(count % 2 == 0){
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