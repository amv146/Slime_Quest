using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using static SpellSystem;

public delegate void SpellCallback(CharacterController character, Tile targetTile, Spell spell);

public class CombatSystem : MonoBehaviour {
    public TurnSystem turnSystem;
    public TileGridManager tileGrid;
    public CharacterController player;
    private List<CharacterController> characters;
    

    // Use this for initialization
    void Start() {
        SpellSystem.tileGrid = tileGrid;
        turnSystem.tileGrid = tileGrid;
        characters = tileGrid.characters;
        Tile.clickCallback = RunClickCallback;
        foreach (CharacterController character in characters) {
            character.castCallback = CastSpell;
        }
    }

    public void RunClickCallback(Tile tile) {
        if (turnSystem.IsPlayerTurn()) {
            tileGrid.RunClickEvents(tile);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void CastSpell(CharacterController character, Tile targetTile, Spell spell) {
        tileGrid.mode = GridMode.Knockback;
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
                            spell.action(otherCharacter);
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
                            spell.action(otherCharacter);
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

            tileGrid.mode = GridMode.Attack;
            tileGrid.SelectedObject = character;
            tileGrid.ResetKnockbackTile();
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
                        spell.action(otherCharacter);
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
            tileGrid.ResetKnockbackTile();
            tileGrid.isHighlightEnabled = true;
        }
    }
}
