using UnityEngine;
using System.Collections;
using System;

public delegate void SpellAction(CharacterController caster = null, CharacterController enemy = null, Tile tile = null);

public class Spell {
    public SpellRadiusType radiusType;
    public SpellType type;
    public int radius;
    public SpellAction action;
    public int knockbackRadius;
}
