using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502, 2343966
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu, vallone@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the AI for the enemies
*/
public class AI : CharacterController {
    public static ClickCallback aiCallback;

    public IEnumerator RunTurn(TileGridManager tileGrid, CharacterController enemy, bool canAttack) {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("AI");
        tileGrid.mode = GridMode.Move;
        if (!canAttack) {
            yield return StartCoroutine(MoveAwayFrom(tileGrid, enemy));
        }
        else if (SpellWillKillEnemy(enemy, out Spell spell)) {
            if (!TryToMoveInRange(tileGrid, enemy, spell)) {
                tileGrid.mode = GridMode.Attack;
                tileGrid.FindPathTo(enemy.currentTile);
                tileGrid.RunKnockbackEvent(PickKnockbackTile(tileGrid, spell, enemy));
                yield return StartCoroutine(aiCallback(enemy.currentTile));
            }
        }
        else {
            if (!TryToMoveInRange(tileGrid, enemy, spell)) {
                tileGrid.mode = GridMode.Attack;
                tileGrid.FindPathTo(enemy.currentTile);
                tileGrid.RunKnockbackEvent(PickKnockbackTile(tileGrid, spell, enemy));
                yield return StartCoroutine(aiCallback(enemy.currentTile));
            }
        }
    }

    public bool SpellWillKillEnemy(CharacterController enemy, out Spell spell) {
        spell = null;
        foreach (Spell spellTemp in spells) {
            if (spellTemp.type == SpellType.Damage && spellTemp.power >= enemy.health) {
                spell = spellTemp;
                return true;
            }
        }

        return false;
    }

    public bool TryToMoveInRange(TileGrid tileGrid, CharacterController enemy, Spell spell) {
        if (enemy.currentTile.DiagonalDistance(currentTile) > castRadius) {
            MoveTowards(tileGrid, enemy);
            return true;
        }

        return false;
    }

    public IEnumerator MoveAwayFrom(TileGrid tileGrid, CharacterController enemy) {
        Tile enemyTile = enemy.currentTile;

        int xDistance = enemy.currentTile.X - currentTile.X;
        int yDistance = enemy.currentTile.Y - currentTile.Y;

        int tileX = currentTile.X + xDistance;
        int tileY = currentTile.Y + yDistance;

        if (tileX >= tileGrid.GetXLength()) {
            tileX = tileGrid.GetXLength() - 1;
        }
        else if (tileX < 0) {
            tileX = 0;
        }

        if (tileY >= tileGrid.GetZLength()) {
            tileY = tileGrid.GetZLength() - 1;
        }
        else {
            tileY = 0;
        }

        Tile newTile = tileGrid.GetTileAt(tileX, tileY);

        tileGrid.FindPathTo(newTile);
        yield return StartCoroutine(aiCallback(newTile));
    }


    private void MoveTowards(TileGrid tileGrid, CharacterController enemy) {
        tileGrid.FindPathTo(enemy.currentTile);

        int pathLength = enemy.currentTile.DiagonalDistance(currentTile) - castRadius;

        Tile tileToMoveTo = tileGrid.GetCurrentPath()[pathLength];
        StartCoroutine(aiCallback(tileToMoveTo));
    }

    public Tile PickKnockbackTile(TileGrid tileGrid, Spell spell, CharacterController enemy) {
        int xDistance = enemy.currentTile.X - currentTile.X;
        int yDistance = enemy.currentTile.Y - currentTile.Y;

        int xKnockback = 2 * xDistance / (xDistance + yDistance);//(spell.knockbackRadius) * xDistance / (xDistance + yDistance);
        int yKnockback = 2 * yDistance / (xDistance + yDistance);//(spell.knockbackRadius) * yDistance / (xDistance + yDistance);

        int knockbackTileX;
        int knockbackTileY;

        if (enemy.health < health) {
            knockbackTileX = enemy.currentTile.X + xKnockback;
            knockbackTileY = enemy.currentTile.Y + yKnockback;
        }
        else {
            knockbackTileX = enemy.currentTile.X - xKnockback;
            knockbackTileY = enemy.currentTile.Y - yKnockback;
        }

        if (knockbackTileX >= tileGrid.GetXLength()) {
            knockbackTileX = tileGrid.GetXLength() - 1;
        }
        else if (knockbackTileX < 0) {
            knockbackTileX = 0;
        }

        if (knockbackTileY >= tileGrid.GetZLength()) {
            knockbackTileY = tileGrid.GetZLength() - 1;
        }
        else {
            knockbackTileY = 0;
        }
        Debug.Log(knockbackTileX + knockbackTileY);
        return tileGrid.GetTileAt(knockbackTileX, knockbackTileY);
    }
}
