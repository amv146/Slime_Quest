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
    public Material TileGrassAttack;
    public Material TileGrass;

    public CharacterController SelectedObject;
    public List<CharacterController> characters;

    private List<Tile> path = new List<Tile>();
    public bool isHighlightEnabled = true;

    public bool inMoveMode = true;
    public bool inAttackMode
    {
        set
        {
            inMoveMode = !value;
        }
        get
        {
            return !inMoveMode;
        }
    }

    private void Start() {
        tiles = new Tile[GetXLength(), GetZLength()];
        Tile.clickCallback = RunClickCallback;
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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            inMoveMode = !inMoveMode;
        }
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
        if (IsTileOccupied(tile)) {
            return new List<Tile>(0);
        }
        AStarAlgorithm.ResetTiles(this);
        AStarAlgorithm.SetWeights(characters);
        path.Clear();
        int counter = 0;
        foreach (Node node in AStarAlgorithm.GetShortestPossiblePath(SelectedObject.currentTile, tile, SelectedObject.movementAmount)) {
            if (inMoveMode) {
                if (counter == SelectedObject.movementAmount + 1) {
                    break;
                }
                path.Add(tiles[node.X, node.Y]);
            }
            else if (inAttackMode) {
                if (node == tile || GetDiagonalDistance(SelectedObject.currentTile, node) == 3) {
                    path.Add(tiles[node.X, node.Y]);
                    break;
                }
            }
            counter++;
        }

        return path;
    }

    public bool IsTileOccupied(Tile tile) {
        foreach (CharacterController character in characters) {
            if (character.currentTile == tile) {
                return true;
            }
        }
        return false;
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
        if (inMoveMode) {
            if (highlight) {
                tile.SetMaterialTo(TileGrassHighlighted);
            }
            else {
                tile.SetMaterialTo(TileGrass);
            }
        }
        else if (inAttackMode) {
            if (highlight) {
                tile.SetMaterialTo(TileGrassAttack);
            }
            else {
                tile.SetMaterialTo(TileGrass);
            }
        }

        tile.isHighlighted = highlight;
    }

    public Tile GetTileAt(int x, int z) {
        return tiles[x, z];
    }

    public void RunClickCallback(Tile tile) {
        if (inMoveMode) {
            MoveSelectedObjectTo(tile);
        }
        else if (inAttackMode) {
            CastPlayerSpellAt(path[path.Count - 1]);
        }
    }

    public void CastPlayerSpellAt(Tile tile) {
        SelectedObject.CastSpell(tile);
    }

    public void MoveSelectedObjectTo(Tile tile) {
        isHighlightEnabled = false;
        if (!SelectedObject.readyToMove) {
            return;
        }
        StartCoroutine(RunMoveObjectTo(tile));
    }

    IEnumerator RunMoveObjectTo(Tile tile) {
        Tile firstTile = SelectedObject.currentTile;
        foreach (Tile pathTile in path) {
            if (firstTile == pathTile) {
                continue;
            }
            SelectedObject.MoveTo(TileCoordToWorldCoord(pathTile));
            SelectedObject.currentTile = pathTile;
            yield return new WaitUntil(() => SelectedObject.readyToMove);
        }
        isHighlightEnabled = true;

    }

    public static int GetEuclidianDistance(Node startNode, Node targetNode) {
        return Mathf.Abs(startNode.X - targetNode.X) + Mathf.Abs(startNode.Y - targetNode.Y);
    }

    public static int GetDiagonalDistance(Node startNode, Node targetNode) {
        int XDistance = Mathf.Abs(startNode.X - targetNode.X);
        int YDistance = Mathf.Abs(startNode.Y - targetNode.Y);

        if (XDistance <= YDistance) {
            return YDistance;
        }
        else {
            return XDistance;
        }
    }
}
