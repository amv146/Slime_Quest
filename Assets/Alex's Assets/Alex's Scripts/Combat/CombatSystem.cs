using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using static SpellSystem;

public delegate void SpellCallback(CharacterController character, Tile targetTile, Spell spell);

public class CombatSystem : MonoBehaviour {
    public TileGrid tileGrid;
    public CharacterController player;
    private List<CharacterController> characters;
    

    // Use this for initialization
    void Start() {
        SpellSystem.tileGrid = tileGrid;
        characters = tileGrid.characters;    
        foreach (CharacterController character in characters) {
            character.castCallback = CastSpell;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void CastSpell(CharacterController character, Tile targetTile, Spell spell) {
        tileGrid.inAttackMode = true;
        StartCoroutine(RunSpellSequence(character, targetTile, spell));
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

    

    private IEnumerator RunSpellSequence(CharacterController character, Tile targetTile, Spell spell) {
        if (spell.radiusType == SpellRadiusType.Box || spell.radiusType == SpellRadiusType.Circle) {
            for (int layer = 1; layer <= spell.radius; ++layer) {
                if (spell.radiusType == SpellRadiusType.Box) {
                    RunBoxSpell(targetTile, spell, layer, character,
                        (tile) =>
                        {
                            tileGrid.SwitchHighlight(tile, true);
                        },
                        (tile, otherCharacter) =>
                        {
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
                        Debug.Log("Hit!");
                        spell.action(otherCharacter);
                    });
                }
                yield return new WaitForSeconds(0.20f);
            }

            for (int i = 0; i < path.Count; ++i) {
                if (spell.radiusType == SpellRadiusType.Line) {
                    RunLineSpell(tileGrid.GetTileAt(path[i].X, path[i].Y), spell, character,
                    (tile) =>
                    {
                        tileGrid.SwitchHighlight(tile, false);
                    });
                }
                yield return new WaitForSeconds(0.20f);
            }

        }
    }
}
