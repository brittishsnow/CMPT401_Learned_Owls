using System;
using System.IO;
using LostPolygon.DynamicWaterSystem;
using LostPolygon.DynamicWaterSystem.EditorExtensions;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class DW_MenuItems : MonoBehaviour {

    private static GameObject CreateGameObject<T>() where T : Component {
        GameObject go = new GameObject(typeof(T).Name);
        go.AddComponent<T>();
        Undo.RegisterCreatedObjectUndo(go, "Create_" + typeof(T).Name);

        Selection.activeGameObject = go;
        EditorGUIUtility.PingObject(go);

        return go;
    }

    [MenuItem("GameObject/Create Other/Dynamic Water")]
    private static void CreateDynamicWater() {
        GameObject water = CreateGameObject<DynamicWater>();
        water.GetComponent<Renderer>().material = new Material(Shader.Find("Diffuse"));

        DW_LayerTagChecker.ShowMissingTagsAndLayersDialog();
    }

    [MenuItem("GameObject/Create Other/Fluid Volume")]
    private static void CreateFluidVolume() {
        CreateGameObject<FluidVolume>();

        DW_LayerTagChecker.ShowMissingTagsAndLayersDialog();
    }

    [MenuItem("GameObject/Create Other/Splash Zone")]
    private static void CreateSplashZone() {
        CreateGameObject<SplashZone>();

        DW_LayerTagChecker.ShowMissingTagsAndLayersDialog();
    }

    [MenuItem("Tools/Lost Polygon/Dynamic Water System/Open Online documentation")]
    private static void OpenOnlineDocumentation() {
        Application.OpenURL("http://static.lostpolygon.com/unity-assets/dynamic-water-system/api-reference/");
    }
}