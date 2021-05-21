using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using static SpellMethods;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu 
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the character in the grid
*/
public delegate void MoveCallback(CharacterController character);

public class CharacterController : Movement
{
    public Tile currentTile;
    private HealthController hc; 
    public int health;
    public List<Spell> spells;
    public SpellCallback castCallback;
    public MoveCallback moveCallback;
    public int castRadius = 3;
    public Spell currentSpell;
    public KeyCode KeybindingAbilityOne;
    public KeyCode KeybindingAbilityTwo;
    public AudioSource SoundEffect;
    public bool isEnemy = false;
    public bool isDead = false;
    public GameObject ScenePosManager;
    public GameObject SceneDataMan;
    public GameObject sprite;

    // Start is called before the first frame update

    private void Start() 
    {
        SceneDataMan = GameObject.FindGameObjectWithTag("SceneManager");
        ScenePosManager = SceneDataMan;
        spells = new List<Spell>();
        Spell newSpell = new Spell();
        newSpell.radius = 2;
        newSpell.knockbackRadius = 2;
        newSpell.radiusType = SpellRadiusType.Line;
        newSpell.action = (caster, enemy, tile) => DamageEnemy(enemy);
        spells.Add(newSpell);
        hc = GetComponentInChildren<HealthController>();
        if(!isEnemy)
        {  
            Debug.Log("Health of Player: "+PlayerPrefs.GetInt("Health"));
            hc.SetHealth(PlayerPrefs.GetInt("Health"));
        }
        else
        {
            Debug.Log("Health of Enemy: "+health);
            hc.SetHealth(SceneDataMan.GetComponent<SceneDataManager>().EnemieHealth);
            this.sprite.GetComponent<SpriteRenderer>().sprite = SceneDataMan.GetComponent<SceneDataManager>().EnemySprite;

        }
        health = hc.currentHealth();
    }

    public void CastSpell(Tile targetTile) {
        castCallback(this, targetTile, spells[0]);
    }

    public void MoveAlongCurrentPath() {
        moveCallback(this);
    }

    public void DecreaseHealth()
    {
        hc.DecreaseHealth();
        health--;
        if(!isEnemy)
        {
            PlayerPrefs.SetInt("Health",health);
            PlayerPrefs.Save();
        }
        if(this.health == 0)
        {
            if(isEnemy)
            {
                ScenePosManager.GetComponent<SceneTransition>().TransitionScene("Cave Floor 1");
            }
            else
            {
                ScenePosManager.GetComponent<SceneTransition>().TransitionScene("Crossroads");
            }
        }
    }
    public bool IsAlive()
    {
        return hc.IsAlive;
    }
    public void setTile(Tile T) {
        
    }
    public void SetHealth(int h)
    {
        this.health = h;
        hc = GetComponentInChildren<HealthController>();
        hc.SetHealth(h);
    }
}
