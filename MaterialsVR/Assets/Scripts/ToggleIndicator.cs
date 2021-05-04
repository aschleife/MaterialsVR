using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ToggleIndicator : MonoBehaviour
{
    private IProgressIndicator indicator;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GetComponent<IProgressIndicator>();
    }

    public async void Toggle(GameObject obj)
    {
        await indicator.AwaitTransitionAsync();

        switch (indicator.State)
        {
            case ProgressIndicatorState.Closed:
                obj.SetActive(false);
                gameObject.SetActive(true);
                await indicator.OpenAsync();
                break;

            case ProgressIndicatorState.Open:
                await indicator.CloseAsync();
                gameObject.SetActive(false);
                obj.SetActive(true);
                break;
        }
    }
}
