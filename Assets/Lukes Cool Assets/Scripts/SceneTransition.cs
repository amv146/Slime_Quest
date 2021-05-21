using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the scene changing
*/
public class SceneTransition : MonoBehaviour
{
    public string SceneToLoad;
    //public Vector3 playerPos = new Vector3(41,2,93);

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !other.isTrigger && SceneDataManager.Instance.IsReadyToLoad) {
            
            SceneDataManager.AddPosition(SceneManager.GetActiveScene().name, other.gameObject.transform.position);
            SceneManager.LoadScene(SceneToLoad);
            if(SceneToLoad == "Cave Floor 1" || SceneToLoad == "Cave Floor 2" || SceneToLoad == "Cave Floor 3" )
            {
                PlayerPrefs.SetInt("Health",5);
                PlayerPrefs.Save();
                Debug.Log("Reseting Health");
            }
        }
    }
    public void TransitionScene(string scene) {
        
        if (SceneDataManager.Instance.IsReadyToLoad) {
            
            SceneDataManager.AddPosition(SceneManager.GetActiveScene().name, GameObject.FindGameObjectWithTag("Player").gameObject.transform.position);
            SceneManager.LoadScene(scene);
        }
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
