
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the scene changing
*/
public class SceneDataManager : MonoBehaviour {
    public static SceneDataManager Instance;
    public Dictionary<string, Vector3> ScenePositions;
    public bool IsReadyToLoad = true;
    // Use this for initialization

    private void Awake() {
        ScenePositions = new Dictionary<string, Vector3>();
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }


    }
    //Adds a position to for the Scenes
    public static void AddPosition(string sceneName, Vector3 position) {
        Instance.StartCoroutine(Instance.ToggleLoadState());
        if (Instance.ScenePositions.ContainsKey(sceneName)) {
            Instance.ScenePositions[sceneName] = position;
        }
        else {
            Instance.ScenePositions.Add(sceneName, position);
        }
    }

    public IEnumerator ToggleLoadState() {
        IsReadyToLoad = false;
        yield return new WaitForSeconds(0.05f);
        IsReadyToLoad = true;
    }
}
