using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/**
    Create buttons on the menu page. Get information from UImanager object.
**/
public class AddButtons : MonoBehaviour {
    // origial button
    public GameObject ButtonPrefab;
    public GameObject newButton;
    public GameObject myCanvas;
    public UIManager uiManager;
    //private Handler _Handler;
    private bool built = false;

	// Use this for initialization
	public void Start(){
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
	}

	// Update is called once per frame
	void Update(){
        if(uiManager.count > 0 & !built){
            ButtonPrefab = GameObject.Find("ButtonPrefab");
            myCanvas = GameObject.Find("Menu_Canvas");
            //_Handler = GameObject.Find("_Handler").GetComponent<Handler>();
            // in unity, -y upwards, +y downwards
            // scale : 4
            Vector3 moveButtonDown = new Vector3(-53.0f, 70.0f, -200.0f);
            for(int i = 0; i < uiManager.count; i++){
                // copy a button and set property
                newButton = Instantiate(ButtonPrefab) as GameObject;
                //Handler handler = newButton.AddComponent<Handler>() as Handler;
                moveButtonDown += new Vector3(-3f, -20f, 0f);
                newButton.transform.position = moveButtonDown;
                newButton.transform.localScale = new Vector3 (1f, 1f, 1f);
                Button b = newButton.GetComponent<Button>();
                b.GetComponentInChildren<Text>().text = UIManager.moleculeNames[i];
                b.name = UIManager.moleculeNames[i];
                // we can use tag to create click event
                b.tag = "b";
                // set buttons parent and set relative position
                newButton.transform.SetParent(myCanvas.transform, false);
            }
            Destroy(ButtonPrefab);
            // maybe a better way: DontDestoryOnLoad(Menu_Canvas)
            DontDestroyOnLoad(uiManager);
            built = true;
        }
        else if(uiManager.count <= 0){
            uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        }
    }
}
