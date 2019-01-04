using UnityEngine;
using System.Collections;
using AssetBundles;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadAssets: MonoBehaviour
{
    private string url = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules";
    public GameObject myCanvas;
    public UIManager uiManager;

    public IEnumerator Start()
    {
    	uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    	// wait ReadManifest finish and update the count
        yield return uiManager;
        myCanvas = GameObject.Find("Menu_Canvas");
        StartCoroutine(DownloadModel());
    }

    public IEnumerator DownloadModel()
    {
        /*
    	// load assetBundle from web server
        WWW www = new WWW(url);
        yield return www;
        AssetBundle assetBundle = www.assetBundle;
        if(string.IsNullOrEmpty(www.error))
        {
            Debug.Log("There was a problem loading asset bundles.");
        }
        */
        
        // load assetBundle from local path
        string url = Application.dataPath + "/../AssetBundles/Android/molecules";
        var assetBundle = AssetBundle.LoadFromFile(url);
        if (assetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
        }
        
        // between 0 ~ (count-1)
        int random_number = Mathf.RoundToInt(Random.value * (uiManager.count - 1));
        // copy and set
        GameObject molecule = Instantiate(assetBundle.LoadAsset(UIManager.moleculeNames[random_number] + ".fbx")) as GameObject;
        Vector3 size = new Vector3(5f, 5f, 5f);
        // scale : 4
        Vector3 position = new Vector3(50f, 0.0f, -250.0f);
        molecule.transform.localScale = size;
        molecule.transform.position = position;
        molecule.tag = "mc";
        molecule.name = UIManager.moleculeNames[random_number];
        // set molecule parent and set relative position
        molecule.transform.SetParent(myCanvas.transform, false);
        // free assetbundle and real object will be intact
        assetBundle.Unload(false);
        yield return assetBundle;
    }
}

// maybe try this out later
/*
public class LoadAssets : MonoBehaviour
{
	public const string AssetBundlesOutputPath = "/AssetBundles/";
	public string assetBundleName;
	public string assetName;

	// Use this for initialization
	IEnumerator Start ()
	{
        assetBundleName = "molecules";
        assetName = "LAO.fbx";
		yield return StartCoroutine(Initialize() );
		
		// Load asset.
		yield return StartCoroutine(InstantiateGameObjectAsync (assetBundleName, assetName) );
	}

	// Initialize the downloading url and AssetBundleManifest object.
	protected IEnumerator Initialize()
	{
		// Don't destroy this gameObject as we depend on it to run the loading script.
		DontDestroyOnLoad(gameObject);

		// With this code, when in-editor or using a development builds: Always use the AssetBundle Server
		// (This is very dependent on the production workflow of the project. 
		// 	Another approach would be to make this configurable in the standalone player.)
		#if DEVELOPMENT_BUILD || UNITY_EDITOR
		AssetBundleManager.SetDevelopmentAssetBundleServer ();
		#else
		// Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:
		AssetBundleManager.SetSourceAssetBundleURL("http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles");
		// Or customize the URL based on your deployment or configuration
		//AssetBundleManager.SetSourceAssetBundleURL("http://www.MyWebsite/MyAssetBundles");
		#endif

		// Initialize AssetBundleManifest which loads the AssetBundleManifest object.
		var request = AssetBundleManager.Initialize();
		if (request != null)
			yield return StartCoroutine(request);
	}

	protected IEnumerator InstantiateGameObjectAsync (string assetBundleName, string assetName)
	{
		// This is simply to get the elapsed time for this phase of AssetLoading.
		float startTime = Time.realtimeSinceStartup;

		// Load asset from assetBundle.
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject) );
		if (request == null)
			yield break;
		yield return StartCoroutine(request);

		// Get the asset.
		GameObject prefab = request.GetAsset<GameObject> ();

        if (prefab != null)
        {
            GameObject mc;
            Vector3 size = new Vector3(0.3f, 0.3f, 0.3f);
            Vector3 slideRight = new Vector3(3.0f, 0.0f, 0.0f);
            Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
            mc = GameObject.Instantiate(prefab);
            mc.transform.localScale = size;
            mc.transform.position += slideRight;
            mc.tag = "mc";
        }
		// Calculate and display the elapsed time.
		float elapsedTime = Time.realtimeSinceStartup - startTime;
		Debug.Log(assetName + (prefab == null ? " was not" : " was")+ " loaded successfully in " + elapsedTime + " seconds" );
	}
}
*/