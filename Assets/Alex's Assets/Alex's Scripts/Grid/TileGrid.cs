using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public Material TileGrassHighlighted;
    public Material TileGrass;

    public CharacterController SelectedObject;

    private List<Tile> path = new List<Tile>();
    public bool isHighlightEnabled = true;

    private void Start() {
        tiles = new Tile[GetXLength(), GetZLength()];
        Tile.clickCallback = MoveSelectedObjectTo;
        Tile.pathFindCallback = SwitchPathHighlight;
        Tile.tileGrassHighlighted = TileGrassHighlighted;
        Tile.tileGrass = TileGrass;
        for (int x = 0; x <= xMax / xSpacing; ++x) {
            for (int z = 0; z <= zMax / zSpacing; ++z) {
                Vector3 position = new Vector3(xMin + (xSpacing * x), yStart, zMin + (zSpacing * z));

                if (!ShouldAutoGenerate) {
                    GameObject gameObject;
                    Tile tile;
                    if ((gameObject = GameObjectUtils.ObjectAt(position)) != null && (tile = gameObject.GetComponent<Tile>()) != null) {
                        tile.SetTileCoords(x, z);
                        tiles[x, z] = tile;
                    }
                }
                else {
                    GameObject gameObject = (GameObject) Instantiate(TilePrefab, new Vector3(x, yStart, z), Quaternion.identity);
                    gameObject.transform.parent = this.gameObject.transform;
                    Tile tile = gameObject.GetComponent<Tile>();
                    tile.tileX = x;
                    tile.tileZ = z;
                    tiles[x, z] = tile;

                }
            }
        }
        SelectedObject.currentTile = tiles[0, 0];
        AStarAlgorithm.TileMap = tiles;
    }

    public int GetXLength() {
        return Mathf.RoundToInt(Mathf.Abs(xMax + 1 - xMin) / xSpacing);
    }

    public int GetZLength() {
        return Mathf.RoundToInt(Mathf.Abs(zMax + 1 - zMin) / zSpacing);
    }



    public Vector3 TileCoordToWorldCoord(int x, int z) {
        return new Vector3(xMin + (xSpacing * x), SelectedObject.transform.position.y, zMin + (zSpacing * z));
    }

    public Vector3 TileCoordToWorldCoord(Tile tile) {
        return new Vector3(xMin + (xSpacing * tile.X), SelectedObject.transform.position.y, zMin + (zSpacing * tile.Z));
    }

    /** private bool SetUpTile(int tileX, int tileZ, GameObject gameObject, out Tile tile) {
        if ((tile = gameObject.GetComponent<Tile>()) != null) {
            tile.SetTileCoords(tileX, tileZ);
            tiles[tileX, tileZ] = tile;
        }
    }
    */

    public List<Tile> GetCurrentPath() {
        return this.path;
    }
    public List<Tile> GetNewPath(Tile tile) {
        StartCoroutine(AStarAlgorithm.ResetTiles());

        path.Clear();
        foreach (Node node in AStarAlgorithm.GetShortestPossiblePath(SelectedObject.currentTile, tile, SelectedObject.movementAmount)) {
            path.Add(tiles[node.X, node.Y]);
        }

        return path;
    }

    public void SwitchPathHighlight(Tile tile, bool highlight) {
        if (!isHighlightEnabled) {
            return;
        }
        if (highlight) {
            foreach (Tile pathTile in GetNewPath(tile)) {
                SwitchHighlight(pathTile, highlight);
            }
        }
        else {
            foreach (Tile pathTile in path) {
                SwitchHighlight(pathTile, highlight);
            }
        }
    }

    public void SwitchHighlight(Tile tile, bool highlight) {
        if (highlight) {
            tile.SetMaterialTo(TileGrassHighlighted);
        }
        else {
            tile.SetMaterialTo(TileGrass);
        }

        tile.isHighlighted = highlight;
    }


    public void ToggleHighlight(Tile tile) {
        if (!tile.isHighlighted) {
            tile.SetMaterialTo(TileGrassHighlighted);
        }
        else {
            tile.SetMaterialTo(TileGrass);
        }

        tile.isHighlighted = !tile.isHighlighted;
    }

    public Tile GetTileAt(int x, int z) {
        return tiles[x, z];
    }

    public void MoveSelectedObjectTo(Tile tile) {
        isHighlightEnabled = false;
        if (!SelectedObject.readyToMove) {
            return;
        }
        SelectedObject.MoveTo(TileCoordToWorldCoord(path[0]));
        StartCoroutine(RunMoveObjectTo(tile));
    }

    IEnumerator RunMoveObjectTo(Tile tile) {
        foreach (Tile pathTile in path) {
            SelectedObject.MoveTo(TileCoordToWorldCoord(pathTile));
            SelectedObject.currentTile = pathTile;
            yield return new WaitUntil(() => SelectedObject.readyToMove);
        }
        isHighlightEnabled = true;
    }
}
