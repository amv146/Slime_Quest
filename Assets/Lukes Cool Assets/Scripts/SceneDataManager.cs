using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneDataManager : MonoBehaviour {
    public static SceneDataManager Instance;
    public Dictionary<string, Vector3> scenePositions;
    public bool isReadyToLoad = true;
    // Use this for initialization

    private void Awake() {
        scenePositions = new Dictionary<string, Vector3>();
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }


    }

    public static void AddPosition(string sceneName, Vector3 position) {
        Instance.StartCoroutine(Instance.ToggleLoadState());
        if (Instance.scenePositions.ContainsKey(sceneName)) {
            Instance.scenePositions[sceneName] = position;
        }
        else {
            Instance.scenePositions.Add(sceneName, position);
        }
    }

    public IEnumerator ToggleLoadState() {
        isReadyToLoad = false;
        yield return new WaitForSeconds(0.05f);
        isReadyToLoad = true;
    }
}
