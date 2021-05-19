using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using static SpellSystem;
using System.Linq;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls the Combat system
*/
public delegate void SpellCallback(CharacterController character, Tile targetTile, Spell spell);

public class CombatSystem : MonoBehaviour {
    public TurnSystem turnSystem;
    public TileGridManager tileGrid;
    public CharacterController player;
    private static List<CharacterController> characters;
    

    // Use this for initialization
    void Awake() {
        characters = new List<CharacterController>();

        foreach (CharacterController player in from gameObject in GameObject.FindGameObjectsWithTag("Player") select gameObject.GetComponent<CharacterController>()) {
            characters.Add(player);
        }

        foreach (CharacterController character in from gameObject in GameObject.FindGameObjectsWithTag("Character") select gameObject.GetComponent<CharacterController>()) {
            characters.Add(character);
        }
        tileGrid.characters = characters;
        SpellSystem.tileGrid = tileGrid;
        turnSystem.tileGrid = tileGrid;
        Tile.clickCallback = RunClickCallback;
        AI.aiCallback = RunAICallback;

        turnSystem.ChangeTurn();
        foreach (CharacterController character in characters) {
            character.castCallback = CastSpell;
            character.moveCallback = MoveAlongPath;
        }
    }

    public IEnumerator RunClickCallback(Tile tile) {
        turnSystem.SetMoveOver(false);
        if (turnSystem.IsPlayerTurn()) {
            if (tileGrid.mode == GridMode.Move) {
                yield return StartCoroutine(CombatMove(tile));
            }
            else if (tileGrid.mode == GridMode.Attack && turnSystem.CanCurrentPlayerAttack()) {
                yield return StartCoroutine(CombatAttack());
            }
            else if (tileGrid.mode == GridMode.Knockback) {
                CombatKnockback(tile);
            }
        }
    }

    public IEnumerator RunAICallback(Tile tile) {
        Debug.Log("AI2");
        turnSystem.SetMoveOver(false);
        if (!turnSystem.IsPlayerTurn()) {
            if (tileGrid.mode == GridMode.Move) {
                yield return StartCoroutine(CombatMove(tile));
            }
            else if (tileGrid.mode == GridMode.Attack && turnSystem.CanCurrentPlayerAttack()) {
                yield return StartCoroutine(CombatAttack());
            }
        }
    }

    private void CombatKnockback(Tile tile) {
        tileGrid.RunKnockbackEvent(tile);
    }

    private IEnumerator CombatAttack() {
        tileGrid.RunSpellEvent();
        Debug.Log("Here");
        yield return new WaitUntil(turnSystem.IsMoveOver);
        Debug.Log("Attack done");
        turnSystem.UseMove(true);
        tileGrid.ResetKnockbackTile();
    }

    private IEnumerator CombatMove(Tile tile) {
        tileGrid.RunMoveEvent(tile);
        yield return new WaitUntil(turnSystem.IsMoveOver);
        turnSystem.UseMove(false);
    }

    // Update is called once per frame
    void Update() {

    }

    void CastSpell(CharacterController character, Tile targetTile, Spell spell) {
        if (character.tag == "Player") {
            tileGrid.mode = GridMode.Knockback;
        }
        StartCoroutine(KnockbackPlayer(character, targetTile, spell));
    }

    Tile[,] GetSpellTiles(Tile targetTile, Spell spell) {
        switch (spell.radiusType) {
            case SpellRadiusType.Box:
                break;
            case SpellRadiusType.Circle:
                break;
            case SpellRadiusType.Line:
                break;
        }
        return null;
    }

    IEnumerator KnockbackPlayer(CharacterController character, Tile targetTile, Spell spell) {
        CharacterController enemyController = null;

        if (spell.knockbackRadius == 0) {
            yield break;
        }
        else {
            for (int layer = 0; layer <= spell.radius; ++layer) {
                if (spell.radiusType == SpellRadiusType.Box) {
                    enemyController = RunBoxSpell(targetTile, spell, layer, character);
                }
                else if (spell.radiusType == SpellRadiusType.Circle) {
                    enemyController = RunCircleSpell(targetTile, spell, layer, character);
                }
            }

            if (spell.radiusType == SpellRadiusType.Line) {
                List<Node> path = AStarAlgorithm.GetShortestPossiblePath(character.currentTile, targetTile);
                for (int i = 0; i < path.Count; ++i) {
                    if (tileGrid.IsTileOccupied(path[i].X, path[i].Y, out CharacterController tileEnemy) && tileEnemy != character) {
                        enemyController = tileEnemy;
                    }
                }
            }

            if (enemyController != null) {
                tileGrid.SelectedObject = enemyController;
                yield return new WaitUntil(tileGrid.KnockbackTileExists);
                tileGrid.isHighlightEnabled = false;
                tileGrid.GetKnockbackTile().SetCursorLayerState(false);
                targetTile.SetCursorLayerState(false);
                tileGrid.Arrow.Disable();
            }
            StartCoroutine(RunSpellSequence(character, targetTile, spell));
        }

    }

    private IEnumerator RunSpellSequence(CharacterController character, Tile targetTile, Spell spell) {
        tileGrid.mode = GridMode.Attack;
        AStarAlgorithm.ResetTiles(tileGrid);
        if (spell.radiusType == SpellRadiusType.Box || spell.radiusType == SpellRadiusType.Circle) {
            for (int layer = 0; layer <= spell.radius; ++layer) {
                if (spell.radiusType == SpellRadiusType.Box) {
                    RunBoxSpell(targetTile, spell, layer, character,
                        (tile) =>
                        {
                            tileGrid.SwitchHighlight(tile, true);

                        },
                        (tile, otherCharacter) =>
                        {
                            otherCharacter.currentTile = tileGrid.GetKnockbackTile();
                            otherCharacter.MoveTo(tileGrid.TileCoordToWorldCoord(tileGrid.GetKnockbackTile()));
                            Debug.Log("Hit!");
                            spell.action(character, otherCharacter);
                        });
                }
                else if (spell.radiusType == SpellRadiusType.Circle) {
                    RunCircleSpell(targetTile, spell, layer, character,
                        (tile) =>
                        {
                            tileGrid.SwitchHighlight(tile, true);
                        },
                        (tile, otherCharacter) =>
                        {
                            otherCharacter.currentTile = tileGrid.GetKnockbackTile();
                            otherCharacter.MoveTo(tileGrid.TileCoordToWorldCoord(tileGrid.GetKnockbackTile()));
                            Debug.Log("Hit!");
                            spell.action(character, otherCharacter);
                        });
                }
                yield return new WaitForSeconds(0.5f);
            }

            for (int layer = 1; layer <= spell.radius; ++layer) {
                if (spell.radiusType == SpellRadiusType.Box) {
                    RunBoxSpell(targetTile, spell, layer, character, (tile) => {
                        tileGrid.SwitchHighlight(tile, false);
                    });
                }
                else if (spell.radiusType == SpellRadiusType.Circle) {
                    RunCircleSpell(targetTile, spell, layer, character,
                        (tile) =>
                        {
                            tileGrid.SwitchHighlight(tile, false);
                        });
                }
                yield return new WaitForSeconds(0.5f);
            }
            tileGrid.SelectedObject = character;
            turnSystem.SetMoveOver(true);
        }
        else if (spell.radiusType == SpellRadiusType.Line) {
            List<Node> path = AStarAlgorithm.GetShortestPossiblePath(character.currentTile, targetTile);
            for (int i = 0; i < path.Count; ++i) {
                if (spell.radiusType == SpellRadiusType.Line) {
                    RunLineSpell(tileGrid.GetTileAt(path[i].X, path[i].Y), spell, character,
                    (tile) =>
                    {
                        tileGrid.SwitchHighlight(tile, true);
                    },
                    (tile, otherCharacter) =>
                    {
                        otherCharacter.currentTile = tileGrid.GetKnockbackTile();
                        otherCharacter.MoveTo(tileGrid.TileCoordToWorldCoord(tileGrid.GetKnockbackTile()));
                        Debug.Log("Hit!");
                        spell.action(character, otherCharacter);
                    });
                }
                yield return new WaitForSeconds(0.10f);
            }

            for (int i = 0; i < path.Count; ++i) {
                if (spell.radiusType == SpellRadiusType.Line) {
                    RunLineSpell(tileGrid.GetTileAt(path[i].X, path[i].Y), spell, character,
                    (tile) =>
                    {
                        tileGrid.SwitchHighlight(tile, false);
                    });
                }
                yield return new WaitForSeconds(0.10f);
            }


            tileGrid.SelectedObject = character;
            turnSystem.SetMoveOver(true);
        }
    }

    public void MoveAlongPath(CharacterController character) {
        if (!character.readyToMove) {
            return;
        }
        StartCoroutine(RunMoveObjectTo(character));
    }

    IEnumerator RunMoveObjectTo(CharacterController character) {
        Tile firstTile = character.currentTile;
        foreach (Tile pathTile in tileGrid.GetCurrentPath()) {
            if (firstTile == pathTile) {
                continue;
            }
            character.MoveTo(tileGrid.TileCoordToWorldCoord(pathTile));
            character.currentTile = pathTile;
            yield return new WaitUntil(() => character.readyToMove);
        }

        Debug.Log(turnSystem.currentPlayer.name + ": Move over"); 
        turnSystem.SetMoveOver(true);

    }
}
