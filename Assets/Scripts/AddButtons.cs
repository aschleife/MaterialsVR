using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class AddButtons : MonoBehaviour {
    // origial button
    public GameObject ButtonPrefab;
    public GameObject newButton;
    public GameObject myCanvas;
    public UIManager uiManager;
    //private Handler _Handler;
    private bool built = false;


    IEnumerator Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        // wait ReadManifest finish and update the count
        // better way: Setting isPlaying delays the result until after all script code has completed for this frame
        yield return uiManager.count;
        //ButtonPrefab = GameObject.Find("ButtonPrefab");
        //myCanvas = GameObject.Find("Menu_Canvas");
        //_Handler = GameObject.Find("_Handler").GetComponent<Handler>();
        // in unity, -y upwards, +y downwards
        // scale : 4
        Debug.Log(uiManager.count);

        Vector3 moveButtonDown = new Vector3(0f, 0f, 0f);

        moveButtonDown = new Vector3(225.7f, 25.08f, -247.1f);

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < uiManager.count; i++)
        {
            Debug.Log("Instantiated Button");
            // copy a button and set property
            newButton = Instantiate(ButtonPrefab) as GameObject;
            //Handler handler = newButton.AddComponent<Handler>() as Handler;
            moveButtonDown += new Vector3(0f, -26.6f, 0f);
            newButton.transform.position = moveButtonDown;
            newButton.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            Button b = newButton.GetComponentInChildren<Button>();
            b.GetComponentInChildren<Text>().text = UIManager.moleculeNames[i];
            b.name = UIManager.moleculeNames[i];
            // we can use tag to create click event
            b.tag = "b";
            // set buttons parent and set relative position
            newButton.transform.SetParent(myCanvas.transform, false);
        }
        //Destroy(ButtonPrefab);
        //DestroyImmediate(ButtonPrefab, true);
        // maybe a better way: DontDestoryOnLoad(Menu_Canvas)
        DontDestroyOnLoad(uiManager);
    }




    /*
	// Update is called once per frame
	void Update(){
        if(uiManager.count > 0 & !built){
            //_Handler = GameObject.Find("_Handler").GetComponent<Handler>();
            // in unity, -y upwards, +y downwards
            // scale : 4
            Vector3 moveButtonDown = new Vector3(-153.3f, 60.08f, -247.1f);
            for (int i = 0; i < uiManager.count; i++){
                // copy a button and set property
                newButton = Instantiate(ButtonPrefab) as GameObject;
                //Handler handler = newButton.AddComponent<Handler>() as Handler;
                moveButtonDown += new Vector3(0f, -30.6f, 0f);
                newButton.transform.position = moveButtonDown;
                newButton.transform.localScale = new Vector3(1f, 1f, 1f);
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

    */
}
