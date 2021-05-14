using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    //public Vector3 playerPos = new Vector3(41,2,93);
    public VectorValue playerPrevPos;

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !other.isTrigger && SceneDataManager.Instance.isReadyToLoad) {
            SceneDataManager.AddPosition(SceneManager.GetActiveScene().name, other.gameObject.transform.position);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    public void transitionScene(string scene) {
        SceneManager.LoadScene(scene);
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
