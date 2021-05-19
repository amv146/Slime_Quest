using UnityEngine;
using System.Collections;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the methods for the spells
*/
public static class SpellMethods {
    public static void DamageEnemy(CharacterController enemy) {
        enemy.DecreaseHealth();
    }

    public static void BlockTiles(Tile tile) {
        tile.defaultWeight = 15;
    }
}
