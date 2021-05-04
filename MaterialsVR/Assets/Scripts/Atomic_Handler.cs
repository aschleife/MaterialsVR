using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class Atomic_Handler : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
	private Renderer _renderer;
	private Dictionary<Color, List<Renderer>> elements;
	private Color original;
	private GameObject[] array;
	private GameObject molecule;
	private TextMeshPro atomText;
	private int count;
    private string atomName;

    // Use this for initialization
    void Start () {
		count = 0;
		Molecular_info temp = new Molecular_info();
		elements = temp.load_info();
		//array = GameObject.FindGameObjectsWithTag("edmc");
		_renderer = gameObject.GetComponent<Renderer>();
		original = _renderer.material.GetColor("_Color");
        atomName = _renderer.material.name.Substring(0, _renderer.material.name.Length - 11);
	}

	public void Update () {
		
	}

	public void Construct(GameObject molecule, TextMeshPro atomText)
    {
		this.molecule = molecule;
		this.atomText = atomText;
	}

	public void OnFocusEnter(FocusEventData eventData)
    {
		atomText.text = atomName;
		atomText.color = original;
		if (_renderer.material.color == original)
		    _renderer.material.color = Color.cyan;
//		Debug.Log(gameObject);
	}

	public void OnFocusExit(FocusEventData eventData)
    {
        if (_renderer.material.color == Color.cyan)
            _renderer.material.color = original;
//		Debug.Log(gameObject);
	}

	public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
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

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
}