using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject loader;
    [SerializeField] private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SelectModel()
    {
        Debug.Log("Click registered");
        //if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        if(gameObject.tag == UIManager.activeTag)
        {
            objMessage.loadMessage(gameObject.name, gameObject.tag);
            objMessage.revolve();
            StartCoroutine(GameObject.Find("Loader").GetComponent<Loader>().LoadObject());
        }
    }

    public void ToggleTag(AddMRTKButtons addMRTKButtons, string switch_to_tag)
    {
        addMRTKButtons.setActiveTag(switch_to_tag);
        Debug.Log("Menu Toggled");
    }

    public void ToggleRotate(Interactable button)
    {
        if (button.IsToggled)
            objMessage.pause();
        else
            objMessage.revolve();
    }
    
    public void TogglePoly(Interactable Button)
    {
        MeshRenderer[] objects = GameObject.Find("MoleculeLoader").GetComponentsInChildren<MeshRenderer>();
        string keyword = "Shape_IndexedFaceSet";
        foreach (MeshRenderer i in objects)
        {
            if (i.gameObject.ToString().Contains(keyword))
                // hide paltes
                i.enabled = false;
        }
    }

    public void ToggleMovement(Interactable button)
    {
        loader.GetComponent<NearInteractionGrabbable>().enabled = button.IsToggled;
        loader.GetComponent<ObjectManipulator>().enabled = button.IsToggled;
        loader.GetComponent<BoundingBox>().enabled = button.IsToggled;
    }

    public void ToggleMenu(Interactable button)
    {
        menu.SetActive(button.IsToggled);
    }
    public void AtomSizePress(float scale)
    {

    }
        
    public void AddPlane(GameObject PlanePrefab)
    {
        if (PlanePrefab == null) return;
        
        GameObject plane;
        plane = Instantiate(PlanePrefab, loader.transform.position, Quaternion.identity);
        plane.tag = "plane";
        
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
