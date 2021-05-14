using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public delegate void SpellCallback(CharacterController character, Tile targetTile, Spell spell);

public class CombatSystem : MonoBehaviour {
    public TileGrid tileGrid;
    public CharacterController player;
    private List<CharacterController> characters;
    

    // Use this for initialization
    void Start() {
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
                return GetBoxTiles(targetTile, spell.radius);
            case SpellRadiusType.Circle:
                return GetCircleTiles(targetTile, spell.radius);
            case SpellRadiusType.Line:
                return GetLineTiles(targetTile, spell.radius);
        }
        return null;
    }

    private void RunBoxSpell(Tile targetTile, Spell spell, int layer, CharacterController character) {
        for (int i = targetTile.X - layer; i <= targetTile.X + layer; ++i) {
            if (i < 0 || i >= tileGrid.GetXLength()) {
                continue;
            }
            if (i == targetTile.X - layer || i == targetTile.X + layer) {
                for (int j = targetTile.Y - layer; j <= targetTile.Y + layer; ++j) {
                    if (j < 0 || j >= tileGrid.GetZLength()) {
                        continue;
                    }
                    tileGrid.SwitchHighlight(tileGrid.GetTileAt(i, j), true);
                    if(tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, j)) != character && tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, j)) != null)
                    {
                        Debug.Log("Hit");

                        tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, j)).DecreaseHealth();
                    }
                }
            }
            else {
                if (!(targetTile.Y - layer < 0)) {
                    tileGrid.SwitchHighlight(tileGrid.GetTileAt(i, targetTile.Y - layer), true);
                    if(tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, targetTile.Y - layer)) != character && tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, targetTile.Y - layer)) != null)
                    {
                        Debug.Log("Hit");

                        tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, targetTile.Y - layer)).DecreaseHealth();
                    }
                }

                if (!(targetTile.Y + layer >= tileGrid.GetZLength())) {
                    tileGrid.SwitchHighlight(tileGrid.GetTileAt(i, targetTile.Y + layer), true);
                    if(tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, targetTile.Y + layer)) != character && tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, targetTile.Y + layer)) != null)
                    {
                        Debug.Log("Hit");

                        tileGrid.doesTileHaveCharacter(tileGrid.GetTileAt(i, targetTile.Y + layer)).DecreaseHealth();
                    }
                }
            }
        }
    }

    private void UnHighlightBoxSpell(Tile targetTile, Spell spell, int layer) {
        for (int i = targetTile.X - layer; i <= targetTile.X + layer; ++i) {
            if (i < 0 || i >= tileGrid.GetXLength()) {
                continue;
            }
            if (i == targetTile.X - layer || i == targetTile.X + layer) {
                for (int j = targetTile.Y - layer; j <= targetTile.Y + layer; ++j) {
                    if (j < 0 || j >= tileGrid.GetZLength()) {
                        continue;
                    }
                    tileGrid.SwitchHighlight(tileGrid.GetTileAt(i, j), false);
                }
            }
            else {
                if (!(targetTile.Y - layer < 0)) {
                    tileGrid.SwitchHighlight(tileGrid.GetTileAt(i, targetTile.Y - layer), false);
                }

                if (!(targetTile.Y + layer >= tileGrid.GetZLength())) {
                    tileGrid.SwitchHighlight(tileGrid.GetTileAt(i, targetTile.Y + layer), false);
                }
            }
        }
    }

    private Tile[,] GetBoxTiles(Tile targetTile, int radius) {
        Tile[,] result = new Tile[2 * radius + 1, 2 * radius + 1];
        int x = 0;
        for (int i = targetTile.X - radius; i <= targetTile.X + radius; ++i) {
            int y = 0;
            for (int j = targetTile.Y - radius; j <= targetTile.Y + radius; ++j) {
                if (i < 0 || j < 0 || i >= tileGrid.GetXLength() || j >= tileGrid.GetZLength()) {
                    result[x, y] = null;
                }
                else {
                    result[x, y] = tileGrid.GetTileAt(i, j);
                }
                y++;
            }
            x++;
        }

        return result;
    }

    private Tile[,] GetCircleTiles(Tile currentTile, int radius) {
        throw new NotImplementedException();
    }

    private Tile[,] GetLineTiles(Tile currentTile, int radius) {
        throw new NotImplementedException();
    }

    private IEnumerator RunSpellSequence(CharacterController character, Tile targetTile, Spell spell) {
        for (int layer = 1; layer <= spell.radius; ++layer) {
            if (spell.radiusType == SpellRadiusType.Box) {
                RunBoxSpell(targetTile, spell, layer, character);
            }
            yield return new WaitForSeconds(0.5f);
        }

        for (int layer = 1; layer <= spell.radius; ++ layer) {
            if (spell.radiusType == SpellRadiusType.Box) {
                UnHighlightBoxSpell(targetTile, spell, layer);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
