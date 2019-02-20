using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe_Handler : MonoBehaviour {
	// GvrControllerInput inherited from MonoBehaviour
	private GameObject molecule;
	private GameObject[] array;
	private Vector3 offset;
	private Vector2 previous_scale;
	private string keyword = "Shape_IndexedFaceSet_MeshPart";
	private Queue<Renderer> isosurfaces;
	public UIManager uiManager;

	// Use this for initialization
	public void Start () {
		// wait for loading AssetBundle
		uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		array = GameObject.FindGameObjectsWithTag("edmc");
		isosurfaces = new Queue<Renderer>();
		// The touch position is given as a Vector2 where X and Y range from 0 to 1. (0, 0) is the top left of the touchpad and (1, 1) is the bottom right of the touchpad
		if(GvrControllerInput.IsTouching){
			previous_scale = GvrControllerInput.TouchPos;
		}
	}
	
	// Update is called once per frame
	public void Update () {
		// get molecule
		if(array == null || array.Length == 0){
			array = GameObject.FindGameObjectsWithTag("edmc");
		}
		if(array != null & array.Length != 0 & !molecule){
			molecule = array[0];
		}
		if(array == null || array.Length == 0 || !molecule){  // need re-assignment
			return;
		}
		Renderer[] objects = molecule.GetComponentsInChildren<Renderer>();
		foreach(Renderer i in objects){
			if(i.gameObject.ToString().Contains(keyword))
				isosurfaces.Enqueue(i);
		}
		bool beingScale = objMessage.loadScale();
		// click AppButton to toggle beingScale
		if(GvrControllerInput.AppButtonDown){
			if(beingScale){
				objMessage.disable_scale();
			}
			else{
				objMessage.enable_scale();
			}
		}
		// if beingScale
		if(beingScale && GvrControllerInput.IsTouching){
			Vector2 current_sclae = GvrControllerInput.TouchPos;
			offset = (convert(current_sclae) - convert(previous_scale)) * 7.0f;  // need a better scale function
			molecule.transform.localScale = molecule.transform.localScale + offset;
			previous_scale = current_sclae;
		}
		// if not beingScale
		else if(!beingScale && GvrControllerInput.IsTouching && isosurfaces.Count != 0){  // need testing
			Vector2 current_sclae = GvrControllerInput.TouchPos;
			float diff = convert2float(previous_scale) - convert2float(current_sclae);
			if(diff > 0.09f){
				Renderer i = isosurfaces.Dequeue();
				if(i.enabled == true){
					i.enabled = false;
				}
				else{
					i.enabled = true;
				}
				isosurfaces.Enqueue(i);
			}
		}
	}

	private Vector3 convert(Vector2 position){
		float scale = Mathf.Sqrt((position.x - 0.5f) * (position.x - 0.5f) +  Mathf.Abs(position.y - 0.5f) * Mathf.Abs(position.y - 0.5f));
		return new Vector3(scale, scale, scale);
	}

	private float convert2float(Vector2 position){
		float scale = Mathf.Sqrt((position.x - 0.5f) * (position.x - 0.5f) +  Mathf.Abs(position.y - 0.5f) * Mathf.Abs(position.y - 0.5f));
		return scale;
	}
}
