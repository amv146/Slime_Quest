using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Mathf;
using System;
using static TileGrid;

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
    public Tile[,] tiles;

    public Material TileGrassHighlighted;
    public Material TileGrassAttack;
    public Material TileGrass;

    public CharacterController SelectedObject;
    public List<CharacterController> characters;

    public static Material ArrowStraight;
    public static Material ArrowCorner;
    public static Material ArrowEnd;
    public static Material SelectCursor;

    public Material ArrowStraightPublic;
    public Material ArrowCornerPublic;
    public Material ArrowEndPublic;
    public Material SelectCursorPublic;

    private List<Tile> path = new List<Tile>();
    public bool isHighlightEnabled = true;

    public bool IsPlayerTurn;

    public Text TurnText;

    public DirectionArrow Arrow;

    public GridMode mode = GridMode.Move;

    private Tile knockbackTile;

    public Spell currentSpell;

    private void Start() {
        ArrowStraight = ArrowStraightPublic;
        ArrowCorner = ArrowCornerPublic;
        ArrowEnd = ArrowEndPublic;
        SelectCursor = SelectCursorPublic;

        tiles = new Tile[this.GetXLength(), this.GetZLength()];
        Arrow.gameObject.SetActive(false);
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
        characters[0].currentTile = tiles[0, 0];
        characters[1].currentTile = tiles[3, 3];
        characters[1].transform.position = this.TileCoordToWorldCoord(characters[1].currentTile);
       //Make new object for sensei
       //Cpooadoasopd
        AStarAlgorithm.TileMap = tiles;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            mode = 1 - mode;
            if (mode == GridMode.Move) {
                AStarAlgorithm.SetWeights(characters);
            }
            else if (mode == GridMode.Attack) {
                AStarAlgorithm.ResetTiles(this);
            }
        }
        
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
        AStarAlgorithm.ResetTiles(this);
        if (mode == GridMode.Move) {
            AStarAlgorithm.SetWeights(characters);
        }
        path.Clear();
        int counter = 0;
        foreach (Node node in AStarAlgorithm.GetShortestPossiblePath(SelectedObject.currentTile, tile)) {
            Tile tileTemp = this.GetTileAt(node);

            if (mode == GridMode.Move) {
                if (!isHighlightEnabled) {
                    break;
                }
                if (counter == 0) {
                    tileTemp.ChangeTileMaterial(SelectCursor);
                }
                tileTemp.SetTileMaterialAndRotation();
                path.Add(tiles[node.X, node.Y]);
                if (counter == SelectedObject.movementAmount || counter == this.GetEuclidianDistance(SelectedObject.currentTile, tile)) {
                    tileTemp.ChangeTileMaterial(ArrowEnd);
                    tileTemp.AddMaterial(SelectCursor);
                    break;
                }
                else {
                    tileTemp.RemoveLastMaterial();
                }
            }
            else if (mode == GridMode.Attack) {
                if (!isHighlightEnabled) {
                    break;
                }
                if (this.GetDiagonalDistance(SelectedObject.currentTile, node) > SelectedObject.castRadius) {
                    break;
                }
                if (node == tile || this.GetDiagonalDistance(SelectedObject.currentTile, node) == SelectedObject.castRadius) {
                    path.Add(tiles[node.X, node.Y]);
                    tileTemp.RemoveLastMaterial();
                }
            }
            else if (mode == GridMode.Knockback) {
                if (counter == 0) {
                    tileTemp.ChangeTileMaterial(SelectCursor);
                }
                if (this.GetDiagonalDistance(SelectedObject.currentTile, node) > currentSpell.knockbackRadius) {
                    break;
                }
                if (node == tile || this.GetDiagonalDistance(SelectedObject.currentTile, node) == currentSpell.knockbackRadius) {
                    path.Add(tiles[node.X, node.Y]);
                    tileTemp.RemoveLastMaterial();
                }
            }
            counter++;
        }
        if (mode == GridMode.Attack) {
            Tile temp = path[path.Count - 1];
            path = new List<Tile>();
            path.Add(temp);
            temp.ChangeTileMaterial(SelectCursor);
        }
        if (mode == GridMode.Knockback) {
            Tile temp = path[path.Count - 1];
            path = new List<Tile>();
            path.Add(temp);
            temp.ChangeTileMaterial(SelectCursor);
            Arrow.SetDirection(SelectedObject.currentTile, temp);
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
        if (mode == GridMode.Move || mode == GridMode.Knockback) {
            if (highlight) {
                tile.transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                tile.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (mode == GridMode.Attack) {
            if (highlight) {
                tile.RemoveLastMaterial();
                tile.ChangeTileMaterial(TileGrassAttack);
                tile.GetTileChild().SetActive(true);
            }
            else {
                tile.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (mode == GridMode.Knockback) {
            if (highlight) {
                tile.RemoveLastMaterial();
                tile.ChangeTileMaterial(SelectCursor);
                tile.GetTileChild().SetActive(true);
            }
            else {
                tile.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        tile.isHighlighted = highlight;
    }

    public void RunClickCallback(Tile tile) {
        if (mode == GridMode.Move && IsPlayerTurn) {
            MoveSelectedObjectTo(tile);
        }
        else if (mode == GridMode.Attack) {
            CastPlayerSpellAt(path[path.Count - 1]);
        }
        else if (mode == GridMode.Knockback) {
            knockbackTile = tile;
        }
    }

    public void CastPlayerSpellAt(Tile tile) {
        currentSpell = SelectedObject.spells[0];
        SelectedObject.CastSpell(tile);
        IsPlayerTurn = !IsPlayerTurn;
        TurnText.GetComponent<TextController>().UpdateUI(IsPlayerTurn);
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
            SelectedObject.MoveTo(this.TileCoordToWorldCoord(pathTile));
            SelectedObject.currentTile = pathTile;
            yield return new WaitUntil(() => SelectedObject.readyToMove);
        }
        isHighlightEnabled = true;
        IsPlayerTurn = !IsPlayerTurn;
        TurnText.GetComponent<TextController>().UpdateUI(IsPlayerTurn);

    }

    public void changeTurns()
    {
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
        knockbackTile = null;
    }
    
}
