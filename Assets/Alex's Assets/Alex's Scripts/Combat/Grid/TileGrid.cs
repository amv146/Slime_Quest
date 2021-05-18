using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Tile[,] tiles;

    public Material TileGrassHighlighted;
    public Material TileGrassAttack;
    public Material TileGrass;

    public CharacterController SelectedObject;
    public List<CharacterController> characters;
    public GameObject character;

    public static Material ArrowStraight;
    public static Material ArrowCorner;
    public static Material ArrowEnd;
    public static Material SelectCursor;
    public static Material AttackCursor;

    public Material ArrowStraightPublic;
    public Material ArrowCornerPublic;
    public Material ArrowEndPublic;
    public Material SelectCursorPublic;
    public Material AttackCursorPublic;

    protected List<Tile> path = new List<Tile>();
    public bool isHighlightEnabled = true;

    public bool IsPlayerTurn;

    public Text TurnText;

    public DirectionArrow Arrow;

    public static GridMode mode = GridMode.Move;

    protected Tile knockbackTile;

    public Spell currentSpell;

    public virtual void Start() {
        ArrowStraight = ArrowStraightPublic;
        ArrowCorner = ArrowCornerPublic;
        ArrowEnd = ArrowEndPublic;
        SelectCursor = SelectCursorPublic;
        AttackCursor = AttackCursorPublic;

        tiles = new Tile[this.GetXLength(), this.GetZLength()];
        Arrow.gameObject.SetActive(false);
        
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
        if (Input.GetKeyDown(character.GetComponent<CharacterController>().KeybindingAbilityOne)) {
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
    public List<Tile> FindPathTo(Tile targetTile) {
        AStarAlgorithm.ResetTiles(this);

        if (mode == GridMode.Move) {
            AStarAlgorithm.SetWeights(characters);
        }

        path.Clear();

        if (mode != GridMode.Knockback && !isHighlightEnabled) {
            return null;
        }

        int counter = 0;
        foreach (Tile tile in from node in AStarAlgorithm.GetShortestPossiblePath(SelectedObject.currentTile, targetTile) select this.GetTileAt(node)) {
            int currentDistance = GetCurrentDistance(SelectedObject.currentTile, tile);
            int maxDistance = GetMaxDistance();

            if (currentDistance > maxDistance) {
                break;
            }
            tile.SetTileMaterialAndRotation();


            if (currentDistance == 0) {
                tile.SetCursorLayerState(true);
                tile.SetHighlightLayerState(false);
            }
            else if (tile == targetTile || currentDistance == maxDistance) {
                tile.SetHighlightMaterialTo(ArrowEnd);
            }

            path.Add(tile);
            counter++;
        }
        return path;
    }

    private int GetCurrentDistance(Tile startTile, Tile targetTile) {
        switch (mode) {
            case GridMode.Move:
                return this.path.Count;
            case GridMode.Attack:
                return this.GetDiagonalDistance(startTile, targetTile);
            case GridMode.Knockback:
                return this.GetDiagonalDistance(startTile, targetTile);
            default:
                return 0;
        }
    }

    private int GetMaxDistance() {
        switch (mode) {
            case GridMode.Move:
                return SelectedObject.movementAmount;
            case GridMode.Attack:
                return SelectedObject.castRadius;
            case GridMode.Knockback:
                return currentSpell.knockbackRadius;
            default:
                return 0;
        }
    }
    
}
