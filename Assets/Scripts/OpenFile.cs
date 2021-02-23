using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using SimpleFileBrowser;
using System.IO;

public class OpenFile : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    public static string file_path;

    private void Start()
    {
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        gameObject.GetComponentInChildren<Button>().Select();
    }

    public void OnFocusExit(FocusEventData eventData) { }

    // maybe use GvrControllerInput.ClickButtonDown later
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        //file_path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        //foreach (string file in file_path)
        //    Debug.Log(file);
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Allow multiple selection: true
        // Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(false, true, null, "Load File", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);
            file_path = FileBrowser.Result[0];
            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

            objMessage.loadMessage(file_path, gameObject.tag);
            objMessage.revolve();
            UIManager.load_from_local = true;
            StartCoroutine(UIManager.loader.GetComponent<Loader>().LoadObject(file_path, "b_iso"));
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }
}
