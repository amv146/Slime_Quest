using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject Text;
    public GameObject BattleButton;

    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);
        BattleButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            Text.SetActive(true);
            BattleButton.SetActive(true);
        }
    }
    
}
