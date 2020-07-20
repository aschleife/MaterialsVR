using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] MoleculeLoader molecule;
    [SerializeField] Isosurface isosurface;
    [SerializeField] public float target_size = 100f;
    private Vector3 def_position;
    private BoundingBox bbox;

    // BoundingBox materials
    [SerializeField] Material BoxMaterial;
    [SerializeField] Material BoxGrabbedMaterial;
    [SerializeField] Material HandleMaterial;
    [SerializeField] Material HandleGrabbedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        def_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objMessage.loadBoolean() == true)
        {   
            transform.Rotate(Vector3.down, Time.deltaTime * 10.0f);
        }
        else
        {
            transform.Rotate(Vector3.zero);
        }
    }

    public IEnumerator LoadObject()
    {
        StopAllCoroutines();
        isosurface.Unload();
        molecule.Unload();
        isosurface.gameObject.SetActive(false);
        molecule.gameObject.SetActive(false);
        string objectName = objMessage.unLoadMessage();
        nameText.text = objectName;
        transform.localScale = Vector3.one;
        transform.position = def_position;
        if (objMessage.unLoadIsosurface())
        {
            Debug.Log("Loading isosurface: " + objectName);
            isosurface.gameObject.SetActive(true);
            yield return isosurface.Load(objectName);
        }
        else
        {
            Debug.Log("Loading molecule: " + objectName);
            molecule.gameObject.SetActive(true);
            yield return molecule.Load(objectName);
        }
        
        DrawBBox();
    }
    private void DrawBBox()
    {
        if (bbox != null)
            Destroy(bbox);
        bbox = gameObject.AddComponent<BoundingBox>();
        // Make the scale handles large
        bbox.ScaleHandleSize = 0.02f;
        bbox.BoxMaterial = BoxMaterial;
        bbox.BoxGrabbedMaterial = BoxGrabbedMaterial;
        bbox.HandleMaterial = HandleMaterial;
        bbox.HandleGrabbedMaterial = HandleGrabbedMaterial;
        // Hide rotation handles
        bbox.ShowRotationHandleForX = false;
        bbox.ShowRotationHandleForY = false;
        bbox.ShowRotationHandleForZ = false;

    }
}
