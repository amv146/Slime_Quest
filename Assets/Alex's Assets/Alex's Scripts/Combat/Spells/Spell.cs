using UnityEngine;
using System.Collections;
using System;

public class Spell {
    public SpellRadiusType radiusType;
    public SpellType type;
    public int radius;
    public Action<CharacterController> action;
}
