using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Movement
{
    public Tile currentTile;
    private HealthController hc; 
    public int health;
    public List<Spell> spells;
    public SpellCallback castCallback;
    public int castRadius = 3;
    // Start is called before the first frame update
    private void Start() 
    {
        hc = GetComponentInChildren<HealthController>();
        health = hc.currentHealth();
    }

    public void CastSpell(Tile targetTile) {
        Spell newSpell = new Spell();
        newSpell.radius = 1;
        newSpell.radiusType = SpellRadiusType.Box;
        castCallback(this, targetTile, newSpell);
    }
    public void DecreaseHealth()
    {
        hc.DecreaseHealth();
        health--;
    }
    public bool IsAlive()
    {
        return hc.IsAlive;
    }
    public void setTile(Tile T) {
        
    }
}
