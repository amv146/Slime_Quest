using System;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This code has all of the enums for the grids/combat
*/
public enum SpellRadiusType {
    Line,
    Box,
    Circle,
    Front
}

public enum SpellType {
    Damage,
    Debuff,
    Other
}

public enum GridMode {
    Move = 0,
    Attack = 1,
    Knockback = 2
}