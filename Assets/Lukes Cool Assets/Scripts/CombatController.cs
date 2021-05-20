using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CombatController : MonoBehaviour
{
    public GameObject DataController;
    public Sprite sprite;
    public int Health;
    public AudioMixer a;
    // Start is called before the first frame update
    void Start()
    {
        Health = DataController.GetComponent<PlayerData>().EnemyHealth;
        this.gameObject.GetComponent<CharacterController>().SetHealth(Health);
        Debug.Log("Setting Sprite");
        this.sprite = DataController.GetComponent<PlayerData>().EnemySprite;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = this.sprite;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
