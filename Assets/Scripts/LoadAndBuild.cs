// using UnityEngine;
// using System.IO;	
// using UnityEditor;

// use BuildPipeline.BuildAssetBundle
// maybe AssetPostprocessor help to import files
// maybe an instruction used to find all materials and "build"
// BuildPipeline.BuildAssetBundles ("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);

// [ExecuteInEditMode]
// public class LoadAndBuild : MonoBehaviour{
// 	// Before scene start:          isPlayingOrWillChangePlaymode = false;  isPlaying = false
// 	// Pressed Playback button:     isPlayingOrWillChangePlaymode = true;   isPlaying = false
// 	// Playing:                     isPlayingOrWillChangePlaymode = false;  isPlaying = true
// 	// Pressed stop button:         isPlayingOrWillChangePlaymode = true;   isPlaying = true
// 	void Awake(){
// 		BuildAssetBundles();
// 	}

// 	public static void BuildAssetBundles(){
// 			// Choose the output path according to the build target.
// 			string outputPath = Path.Combine("AssetBundles",  "Android");
// 			if (!Directory.Exists(outputPath) )
// 				Directory.CreateDirectory (outputPath);
	
// 			//@TODO: use append hash... (Make sure pipeline works correctly with it.)
// 			BuildPipeline.BuildAssetBundles (outputPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
// 	}
// }
