using UnityEngine;

/// <summary>
/// Base GUI used for demos. 
/// </summary>
public abstract class DW_DemoGUI : MonoBehaviour {
    public Texture2D Logo;
    protected bool visible = true;

#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3
    protected void OnLevelWasLoaded(int level) {
        SceneLoadedHandler(level);
    }
#else
    protected virtual void OnEnable() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    protected virtual void OnDisable() {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }

    private void SceneManagerOnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode) {
        SceneLoadedHandler(scene.buildIndex);
    }
#endif

    protected virtual void SceneLoadedHandler(int buildIndex) {
        DW_CameraFade.StartAlphaFade(Color.black, true, 0.5f, 0f);
    }
    
    protected virtual void Start() {
        useGUILayout = false;
    }

    protected virtual void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            DW_CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0f, () => DW_SceneManagerWrapper.LoadLevel("DW_Menu"));
        }

        if (Input.GetKeyDown(KeyCode.Menu) || Input.GetKeyDown(KeyCode.Return)) {
            visible = !visible;
        }
    }

    protected virtual void DrawBackToMenuButton(float height) {
        DW_GUILayout.itemHeight = height;
        GUI.backgroundColor = new Color(1f, 0.6f, 0.6f, 1f);
        if (GUI.Button(new Rect(DW_GUILayout.paddingLeft, height - 40f, DW_GUILayout.itemWidth, 30f), "Back to Main Menu")) {
            DW_CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0f, () => DW_SceneManagerWrapper.LoadLevel("DW_Menu"));
        }
        GUI.backgroundColor = Color.white;
    }
}
