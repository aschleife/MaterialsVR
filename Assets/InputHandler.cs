using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevices(devices);
        var device = devices[0];
        bool menuValue;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuValue) && menuValue)
        {
            Debug.Log("menu pressed");
            ToggleMenu();
        }


    }

    void ToggleMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
