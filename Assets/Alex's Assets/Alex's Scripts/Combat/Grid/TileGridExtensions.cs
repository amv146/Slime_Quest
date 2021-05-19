using UnityEngine;
using System.Collections;
/*
*   Name: Luke Driscoll, Mark Griffin, Alex Vallone, Grant Ward
*   ID: 2344496, 2340502
*   Email: ldriscoll@chapman.edu, magriffin@chapman.edu
*   Class: CPSC244-01
*   Final Project
*   This is my own work. I did not cheat on this assignment
*   This class controls more stuff for the tilegrif
*/
public static class TileGridExtensions {
    public static int GetXLength(this TileGrid tileGrid) {
        return Mathf.RoundToInt(Mathf.Abs(tileGrid.xMax + 1 - tileGrid.xMin) / tileGrid.xSpacing);
    }

    public static int GetZLength(this TileGrid tileGrid) {
        return Mathf.RoundToInt(Mathf.Abs(tileGrid.zMax + 1 - tileGrid.zMin) / tileGrid.zSpacing);
    }

    public static Vector3 TileCoordToWorldCoord(this TileGrid tileGrid, int x, int z) {
        return new Vector3(tileGrid.xMin + (tileGrid.xSpacing * x), tileGrid.SelectedObject.transform.position.y, tileGrid.zMin + (tileGrid.zSpacing * z));
    }

    public static Vector3 TileCoordToWorldCoord(this TileGrid tileGrid, Tile tile) {
        return new Vector3(tileGrid.xMin + (tileGrid.xSpacing * tile.X), tileGrid.SelectedObject.transform.position.y, tileGrid.zMin + (tileGrid.zSpacing * tile.Z));
    }

    public static bool IsTileOccupied(this TileGrid tileGrid, Tile tile, out CharacterController character) {
        foreach (CharacterController tempCharacter in tileGrid.characters) {
            if (tempCharacter.currentTile == tile) {
                character = tempCharacter;
                return true;
            }
        }
        character = null;
        return false;
    }

    public static bool IsTileOccupied(this TileGrid tileGrid, int x, int z, out CharacterController character) {
        Tile tile = tileGrid.GetTileAt(x, z);
        foreach (CharacterController tempCharacter in tileGrid.characters) {
            if (tempCharacter.currentTile == tile) {
                character = tempCharacter;
                return true;
            }
        }
        character = null;
        return false;
    }

    public static bool IsTileOccupied(this TileGrid tileGrid, Tile tile) {
        foreach (CharacterController tempCharacter in tileGrid.characters) {
            if (tempCharacter.currentTile == tile) {
                return true;
            }
        }
        return false;
    }

    public static bool IsTileOccupied(this TileGrid tileGrid, int x, int z) {
        Tile tile = tileGrid.GetTileAt(x, z);
        foreach (CharacterController tempCharacter in tileGrid.characters) {
            if (tempCharacter.currentTile == tile) {
                return true;
            }
        }
        return false;
    }

    public static Tile GetTileAt(this TileGrid tileGrid, int x, int z) {
        return tileGrid.tiles[x, z];
    }

    public static Tile GetTileAt(this TileGrid tileGrid, Node node) {
        return tileGrid.GetTileAt(node.X, node.Y);
    }

    public static int GetEuclidianDistance(this TileGrid tileGrid, Node startNode, Node targetNode) {
        return Mathf.Abs(startNode.X - targetNode.X) + Mathf.Abs(startNode.Y - targetNode.Y);
    }

    

}
