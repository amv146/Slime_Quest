using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public delegate void ClickCallback(Tile tile);
public delegate void HighlightCallback(Tile tile);

public class Tile : Node
{
    public int tileX {
        get
        {
            return X;
        }
        set
        {
            X = value;
        }
    }
    public int tileZ {
        get
        {
            return Y;
        }
        set
        {
            Y = value;
        }
    }
    public static ClickCallback clickCallback;
    public static HighlightCallback pathFindCallback;
    public static Material tileGrassHighlighted;
    public static Material tileGrass;
    public bool isHighlighted;
    public bool isInPath;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp() {
        clickCallback(this);
    }

    private void OnMouseEnter() {
        pathFindCallback(this);
    }

    private void OnMouseExit() {
        pathFindCallback(this);
    }

    public void SetTileCoords(int x, int z) {
        tileX = x;
        tileZ = z;
    }

    public void SetTileMaterialAndRotation() {
        Node parent = parentNode;
        if (parent != null) {
            Vector2 direction = parent.GetDirectionVector(this);

            GetHighlightLayer().transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
            
            if (parent.parentNode != null) {
                Vector2 parentDirection = parent.parentNode.GetDirectionVector(parent);
                if (Mathf.Abs(direction.x) - Mathf.Abs(parentDirection.x) != 0 || Mathf.Abs(direction.y) - Mathf.Abs(parentDirection.y) != 0) {
                    parent.GetComponent<Tile>().SetHighlightMaterialTo(TileGrid.ArrowCorner);
                    parent.GetComponent<Tile>().GetHighlightLayer().transform.rotation = Quaternion.Euler(0, GetRotation(direction, parentDirection), 0);
                }
            }

            SetHighlightMaterialTo(TileGrid.ArrowStraight);
        }

        SetCursorLayerState(false);
    }

    public GameObject GetHighlightLayer() {
        return transform.Find("HighlightLayer").gameObject;
    }

    public GameObject GetCursorLayer() {
        return transform.Find("CursorLayer").gameObject;
    }

    public void SetHighlightMaterialTo(Material material) {
        GetHighlightLayer().GetComponent<MeshRenderer>().material = material;
    }

    public void SetCursorMaterialTo(Material material) {
        GetCursorLayer().GetComponent<MeshRenderer>().material = material;
    }

    public void SetCursorLayerState(bool active) {
        if (TileGrid.mode == GridMode.Attack || TileGrid.mode == GridMode.Knockback) {
            SetCursorMaterialTo(TileGrid.AttackCursor);
        }
        else if (TileGrid.mode == GridMode.Move) {
            SetCursorMaterialTo(TileGrid.SelectCursor);
        }
        GetCursorLayer().SetActive(active);
    }

    public void SetHighlightLayerState(bool active) {
        GetHighlightLayer().SetActive(active);
    }

    public int GetRotation(Vector3 direction, Vector3 parentDirection) {
        //float first = (float) Math.Atan2(direction.y, direction.x);
        //float second = (float ) (2.0f * Math.PI - Math.Atan2(parentDirection.y, parentDirection.x));
        //if (Mathf.Abs(first - second) - Math.PI / 2.0f > 0.01) {
        //    first = (float) (2.0f * Math.PI) - first;
        //}

        //int degrees =(int) (Rad2Deg * Mathf.Min(first, second)) - 90;
        //return degrees;
        int[] array =
        {
            (int) parentDirection.x, (int) parentDirection.y, (int) direction.x, (int) direction.y
        };
        if (Enumerable.SequenceEqual(array, new int[] { 1, 0, 0, 1 })) {
            return 0;
        }
        else if (Enumerable.SequenceEqual(array, new int[] { -1, 0, 0, 1 })) {
            return 90;
        }
        else if (Enumerable.SequenceEqual(array, new int[] { 1, 0, 0, -1 })) {
            return 270;
        }
        else if (Enumerable.SequenceEqual(array, new int[] { -1, 0, 0, -1 })) {
            return 180;
        }
        else if (Enumerable.SequenceEqual(array, new int[] { 0, 1, 1, 0 })) {
            return 180;
        }
        else if (Enumerable.SequenceEqual(array, new int[] { 0, -1, 1, 0 })) {
            return 90;
        }
        else if (Enumerable.SequenceEqual(array, new int[] { 0, 1, -1, 0 })) {
            return 270;
        }
        else if (Enumerable.SequenceEqual(array, new int[] { 0, -1, -1, 0 })) {
            return 0;
        }

        return 0;
    }

    public void AddMaterial(Material material) {
        Material[] newMaterials = new Material[2];
        newMaterials[0] = GetHighlightLayer().GetComponent<MeshRenderer>().material;
        newMaterials[1] = material;
        GetHighlightLayer().GetComponent<MeshRenderer>().materials = newMaterials;
    }

    public void RemoveLastMaterial() {
        Material[] newMaterials = new Material[1];
        newMaterials[0] = GetHighlightLayer().GetComponent<MeshRenderer>().material;
        GetHighlightLayer().GetComponent<MeshRenderer>().materials = newMaterials;
    }
}
