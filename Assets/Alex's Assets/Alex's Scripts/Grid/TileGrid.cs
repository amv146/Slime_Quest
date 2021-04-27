using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGrid : MonoBehaviour
{
    public float xMin;
    public float zMin;

    public float xMax;
    public float zMax;

    public float xSpacing;
    public float zSpacing;

    public float tileHeight;
    public float yStart;

    public bool ShouldAutoGenerate;

    public GameObject TilePrefab;
    private Tile[,] tiles;

    public CharacterController SelectedObject;

    private void Start() {
        tiles = new Tile[GetXLength(), GetZLength()];
        for (int x = 0; x < xMax / xSpacing; ++x) {
            for (int z = 0; z < zMax / zSpacing; ++z) {
                Vector3 position = new Vector3(xMin + (xSpacing * x), yStart, zMin + (zSpacing * z));

                if (!ShouldAutoGenerate) {
                    GameObject gameObject;
                    Tile tile;
                    if ((gameObject = GameObjectUtils.ObjectAt(position)) != null && (tile = gameObject.GetComponent<Tile>()) != null) {
                        tile.SetTileCoords(x, z);
                        tile.clickCallback = MoveSelectedUnitTo;
                        tiles[x, z] = tile;
                        print(tile);
                    }
                }
                else {
                    GameObject gameObject = (GameObject) Instantiate(TilePrefab, new Vector2(x, z), Quaternion.identity);
                    gameObject.transform.parent = this.gameObject.transform;
                    Tile tile = gameObject.GetComponent<Tile>();
                    tile.tileX = x;
                    tile.clickCallback = MoveSelectedUnitTo;

                }
            }
        }
    }

    public int GetXLength() {
        return Mathf.RoundToInt(Mathf.Abs(xMax - xMin) / xSpacing);
    }

    public int GetZLength() {
        return Mathf.RoundToInt(Mathf.Abs(zMax - zMin) / zSpacing);
    }

    public Vector3 TileCoordToWorldCoord(int x, int z) {
        return new Vector3(xMin + (xSpacing * x), SelectedObject.transform.position.y, zMin + (zSpacing * z));
    }

    public void MoveSelectedUnitTo(int x, int z) {
        SelectedObject.MoveTo(TileCoordToWorldCoord(x, z));
    }
}
