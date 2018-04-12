using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class DW_DemoHelper : MonoBehaviour {
    private const string kDemoMenuScene = "DW_Menu";

    private static readonly string[] kDemoScenes = {
        kDemoMenuScene,
        "DW_Waterfall",
        "DW_Buoyancy",
        "DW_BuoyancyMobile",
        "DW_Pool",
        "DW_PoolMobile",
        "DW_Boat",
        "DW_BoatMobile",
        "DW_Character",
        "DW_Obstruction"
    };

    static DW_DemoHelper() {
#if UNITY_2017_2_OR_NEWER
        EditorApplication.playModeStateChanged += GenerateManifestIfAbsent;
        GenerateManifestIfAbsent(PlayModeStateChange.EnteredEditMode);
#else
        EditorApplication.playmodeStateChanged += GenerateManifestIfAbsent;
        GenerateManifestIfAbsent();
#endif
    }
    
#if UNITY_2017_2_OR_NEWER
    private static void GenerateManifestIfAbsent(PlayModeStateChange playModeStateChange) {
#else
    private static void GenerateManifestIfAbsent() {
#endif
        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying) {
            CheckLayers();
            CheckIfDemoScenesAdded();
        }
    }

    private static void CheckIfDemoScenesAdded() {
        if (DW_SceneManagerWrapper.LoadedLevelName != kDemoMenuScene)
            return;

        // Detect which demo scenes are missing from the build settings
        EditorBuildSettingsScene[][] currentBuildScenes = { EditorBuildSettings.scenes };
        IEnumerable<string> alreadyAddedScenes = 
            kDemoScenes
            .Where(demoSceneName =>
                currentBuildScenes[0]
                    .Select(buildScene => Path.GetFileNameWithoutExtension(buildScene.path))
                    .Contains(demoSceneName)
            );
        string[] scenesToAdd = kDemoScenes.Except(alreadyAddedScenes).ToArray();

        // Show the dialog
        if (scenesToAdd.Length > 0) {
            const string dialogMessage =
                "Having Dynamic Water System demo scenes added to Build Settings " +
                "is required for Demo Menu to function, " +
                "but not all scenes are currently added.\n\n" +
                "Do you want to add them automatically?";

            if (!EditorUtility.DisplayDialog("Add demo scenes", dialogMessage, "Add", "Cancel"))
                return;
        }

        // Match the scene names with actual scene assets
        List<EditorBuildSettingsScene> addedDemoScenes = new List<EditorBuildSettingsScene>();
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string sceneName in scenesToAdd) {
            string sceneNameWithExtension = sceneName + ".unity";
            bool isFound = false;
            for (int i = 0; i < allAssetPaths.Length; i++) {
                if (allAssetPaths[i].EndsWith(sceneNameWithExtension)) {
                    EditorBuildSettingsScene addedScene = new EditorBuildSettingsScene(allAssetPaths[i], true);
                    addedDemoScenes.Add(addedScene);
                    isFound = true;
                    break;
                }
            }

            if (!isFound) {
                Debug.LogError("Demo scene " + sceneName + "not found in assets");
            }
        }

        // Finally, add the scenes to Build Settings
        if (addedDemoScenes.Count > 0) {
            currentBuildScenes[0] = currentBuildScenes[0].Concat(addedDemoScenes).ToArray();
            EditorBuildSettings.scenes = currentBuildScenes[0];
        }
    }

    private static void CheckLayers() { 
        // Is the scene present if the list?
        string currentScenePath =
#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            EditorApplication.currentScene;
#else
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
#endif
        string scene = Path.GetFileNameWithoutExtension(currentScenePath);
        foreach (string x in kDemoScenes) {
            if (x != scene) 
                continue;

            DW_LayerTagChecker.ShowMissingTagsAndLayersDialog();
            break;
        }
    }
}