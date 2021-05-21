using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the tutorial
*/
public class TutorialController : MonoBehaviour
{
    public GameObject Text;
    public GameObject Combatcontroller;

    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            Text.SetActive(true);
            StartCoroutine(StartBattle());
        }
    }
    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(30f);
        Combatcontroller.GetComponent<CombatController>().startBattle();
    }
    
}
