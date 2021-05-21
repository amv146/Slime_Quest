using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CombatController : MonoBehaviour
{
    public GameObject DataController;
    public List<Sprite> Enemies;
    public List<int> Healths;
    public GameObject ScenePosManager;
    public GameObject SceneDataMan;
    public float timer = 10;
    public bool isTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        SceneDataMan = GameObject.FindGameObjectWithTag("SceneManager");
        ScenePosManager = SceneDataMan;
        StartCoroutine(EncounterEnemy());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator EncounterEnemy()
    {
        //Wait random Time to encounter
        if(timer>0)
        {
            yield return new WaitForSeconds(timer);
            startBattle();
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    public void startBattle()
    {
        int enemyIndex = Random.Range(0, Enemies.Count);
        Sprite sprite = Enemies[enemyIndex];
        int health = Healths[enemyIndex];
        //Start Encounter
        SceneDataMan.GetComponent<SceneDataManager>().EnemySprite = sprite;
        SceneDataMan.GetComponent<SceneDataManager>().EnemieHealth = health;

        ScenePosManager.GetComponent<SceneTransition>().TransitionScene("Tutorial");
    }
}
