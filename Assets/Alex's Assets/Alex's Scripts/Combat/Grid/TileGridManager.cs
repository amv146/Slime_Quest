using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class manages the tile grid
*/
public class TileGridManager : TileGrid {

    public override void Start() {
        base.Start();
        Tile.pathFindCallback = HighlightNewPath;
    }

  

    public void RunMoveEvent(Tile tile) {
        UnhighlightCurrentPath();
        isHighlightEnabled = false;
        SelectedObject.MoveAlongCurrentPath();
    }

    public void RunSpellEvent() {
        CastPlayerSpellAt(path[path.Count - 1]);
    }

    public void RunKnockbackEvent(Tile tile) {
        knockbackTile = tile;
    }

    public void HighlightNewPath(Tile targetTile) {
        if (!isHighlightEnabled) {
            return;
        }
        List<Tile> oldPath = this.path;

        for (int i = 0; i < oldPath.Count; ++i) {
            Tile tile = oldPath[i];
            tile.SetHighlightLayerState(false);
            tile.SetCursorLayerState(false);
        }

        FindPathTo(targetTile);

        for (int i = 0; i < path.Count; ++i) {
            Tile tile = path[i];
            SwitchHighlight(tile, true);
            if (i == 0) {
                tile.SetCursorMaterialTo(AttackCursor);
                tile.SetHighlightLayerState(false);
            }
            if (i == path.Count - 1) {
                tile.SetCursorMaterialTo(AttackCursor);
                tile.SetCursorLayerState(true);
            }
        }

        if (mode == GridMode.Knockback) {
            Arrow.SetDirection(SelectedObject.currentTile, path[path.Count - 1]);
        }
    }

    public void UnhighlightCurrentPath() {
        for (int i = 0; i < path.Count; ++i) {
            Tile tile = path[i];
            tile.SetHighlightLayerState(false);
            tile.SetCursorLayerState(false);
        }
    }

    public void SwitchHighlight(Tile tile, bool highlight) {
        if (mode == GridMode.Move) {
            if (highlight) {
                tile.SetCursorMaterialTo(SelectCursor);
                tile.SetHighlightLayerState(true);
            }
            else {
                tile.SetHighlightLayerState(false);
            }
        }
        else if (mode == GridMode.Attack) {
            if (!KnockbackTileExists() && tile != path[path.Count - 1]) {
                return;
            }
            if (highlight) {
                tile.SetCursorMaterialTo(AttackCursor);
                tile.SetHighlightMaterialTo(TileGrassAttack);
                tile.SetHighlightLayerState(true);
            }
            else {
                tile.SetHighlightLayerState(false);
            }
        }
    }


    public void CastPlayerSpellAt(Tile tile) {
        currentSpell = SelectedObject.spells[0];
        SelectedObject.CastSpell(tile);
        IsPlayerTurn = !IsPlayerTurn;
        TurnText.GetComponent<TextController>().UpdateUI(IsPlayerTurn);
    }

    

    public void changeTurns() {
        IsPlayerTurn = !IsPlayerTurn;
        TurnText.GetComponent<TextController>().UpdateUI(IsPlayerTurn);
    }

    public bool KnockbackTileExists() {
        return (knockbackTile != null);
    }

    public Tile GetKnockbackTile() {
        return knockbackTile;
    }

    public void ResetKnockbackTile() {
        mode = GridMode.Move;
        knockbackTile = null;
    }
}
