using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Assertions;
using System.Collections.Generic;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the Spell System
*/
public class SpellSystem {
    public static TileGridManager tileGrid;

    public static CharacterController RunBoxSpell(Tile targetTile, Spell spell, int layer, CharacterController character, Action<Tile> alwaysActions = null, Action<Tile, CharacterController> offensiveActions = null) {
        CharacterController enemy = null;
        for (int i = targetTile.X - layer; i <= targetTile.X + layer; ++i) {
            if (i < 0 || i >= tileGrid.GetXLength()) {
                continue;
            }
            if (i == targetTile.X - layer || i == targetTile.X + layer) {
                for (int j = targetTile.Y - layer; j <= targetTile.Y + layer; ++j) {
                    if (j < 0 || j >= tileGrid.GetZLength()) {
                        continue;
                    }
                    Tile tile = tileGrid.GetTileAt(i, j);
                    alwaysActions?.Invoke(tile);
                    if(tileGrid.IsTileOccupied(tile, out CharacterController tileCharacter) && tileCharacter != character)
                    {
                        enemy = tileCharacter;
                        offensiveActions?.Invoke(tile, tileCharacter);
                    }
                }
            }
            else {
                if (!(targetTile.Y - layer < 0)) {
                    Tile tile = tileGrid.GetTileAt(i, targetTile.Y - layer);
                    alwaysActions?.Invoke(tile);
                    if(tileGrid.IsTileOccupied(tile, out CharacterController tileCharacter) && tileCharacter != character)
                    {
                        enemy = tileCharacter;
                        offensiveActions?.Invoke(tile, tileCharacter);
                    }
                }

                if (!(targetTile.Y + layer >= tileGrid.GetZLength())) {
                    Tile tile = tileGrid.GetTileAt(i, targetTile.Y + layer);
                    alwaysActions?.Invoke(tile);
                    if(tileGrid.IsTileOccupied(tile, out CharacterController tileCharacter) && tileCharacter != character)
                    {
                        enemy = tileCharacter;
                        offensiveActions?.Invoke(tile, tileCharacter);
                    }
                }
            }
        }

        return enemy;
    }

    public static CharacterController RunCircleSpell(Tile targetTile, Spell spell, int layer, CharacterController character, Action<Tile> alwaysActions = null, Action<Tile, CharacterController> offensiveActions = null) {
        CharacterController enemy = null;
        for (int i = targetTile.X - layer; i <= targetTile.X + layer; ++i) {
            if (i < 0 || i >= tileGrid.GetXLength()) {
                continue;
            }
            for (int j = targetTile.Y - layer; j <= targetTile.Y + layer; ++j) {
                if (j < 0 || j >= tileGrid.GetZLength()) {
                    continue;
                }
                if (Mathf.Abs(i - targetTile.X) + Mathf.Abs(j - targetTile.Y) != layer) {
                    continue;
                }

                Tile tile = tileGrid.GetTileAt(i, j);
                alwaysActions?.Invoke(tile);

                if (tileGrid.IsTileOccupied(tile, out CharacterController tileCharacter) && tileCharacter != character) {
                    enemy = tileCharacter;
                    offensiveActions?.Invoke(tile, tileCharacter);
                }
            }
        }

        return enemy;
    }

    public static CharacterController RunLineSpell(Tile tile, Spell spell, CharacterController character, Action<Tile> alwaysActions, Action<Tile, CharacterController> offensiveActions = null) {
        alwaysActions(tile);
        CharacterController enemy = null;
        if (tileGrid.IsTileOccupied(tile, out CharacterController tileCharacter) && tileCharacter != character) {
            enemy = tileCharacter;
            offensiveActions?.Invoke(tile, tileCharacter);
        }

        return enemy;
    }
}
