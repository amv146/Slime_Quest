using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpellMethods;

public class CharacterController : Movement
{
    public Tile currentTile;
    private HealthController hc; 
    public int health;
    public List<Spell> spells;
    public SpellCallback castCallback;
    public int castRadius = 3;
    public Spell currentSpell;
    public KeyCode KeybindingAbilityOne;
    public KeyCode KeybindingAbilityTwo;
    // Start is called before the first frame update
    private void Start() 
    {
        spells = new List<Spell>();
        Spell newSpell = new Spell();
        newSpell.radius = 2;
        newSpell.knockbackRadius = 2;
        newSpell.radiusType = SpellRadiusType.Line;
        newSpell.action = (caster, enemy, tile) => DamageEnemy(enemy);
        spells.Add(newSpell);
        hc = GetComponentInChildren<HealthController>();
        health = hc.currentHealth();
    }

    public void CastSpell(Tile targetTile) {
        castCallback(this, targetTile, spells[0]);
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
