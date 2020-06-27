using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string Menu;
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("MixedRealityToolkit"));
        DontDestroyOnLoad(GameObject.Find("MixedRealityPlayspace"));
        DontDestroyOnLoad(GameObject.Find("UIRaycastCamera"));
        DontDestroyOnLoad(GameObject.Find("UIManager"));
        SceneManager.LoadScene(Menu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
