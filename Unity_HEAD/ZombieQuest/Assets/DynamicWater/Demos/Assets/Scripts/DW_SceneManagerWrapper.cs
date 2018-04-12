#if !(UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
#define UNITY_NEW_SCENE_MANAGEMENT
#endif

public static class DW_SceneManagerWrapper {
    public static string LoadedLevelName {
        get {
#if UNITY_NEW_SCENE_MANAGEMENT
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
#else
            return UnityEngine.Application.loadedLevelName;
#endif
        }
    }

    public static void LoadLevel(string name) {
#if UNITY_NEW_SCENE_MANAGEMENT
        UnityEngine.SceneManagement.SceneManager.LoadScene(name, UnityEngine.SceneManagement.LoadSceneMode.Single);
#else
        UnityEngine.Application.LoadLevel(name);
#endif
    }
}

