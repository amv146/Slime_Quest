using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Movement
{
    public Tile currentTile;
    public List<Spell> spells;
    public SpellCallback castCallback;
    // Start is called before the first frame update


    public void CastSpell(Tile targetTile) {
        Spell newSpell = new Spell();
        newSpell.radius = 2;
        newSpell.radiusType = SpellRadiusType.Box;
        castCallback(this, targetTile, newSpell);
    }
}
