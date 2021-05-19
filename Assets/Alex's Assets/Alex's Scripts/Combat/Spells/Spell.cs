using UnityEngine;
using System.Collections;
using System;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the spells
*/
public delegate void SpellAction(CharacterController caster = null, CharacterController enemy = null, Tile tile = null);

public class Spell {
    public SpellRadiusType radiusType;
    public SpellType type;
    public int radius;
    public SpellAction action;
    public int knockbackRadius;
    public int power = 0;
}
