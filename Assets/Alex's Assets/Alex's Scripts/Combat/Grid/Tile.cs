using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public delegate void ClickCallback(Tile tile);
public delegate void HighlightCallback(Tile tile, bool highlight);

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
        pathFindCallback(this, true);
    }

    private void OnMouseExit() {
        pathFindCallback(this, false);
    }

    private void OnMouseDown() {
        pathFindCallback(this, false);
    }

    public void SetTileCoords(int x, int z) {
        tileX = x;
        tileZ = z;
    }

    public void SetMaterialTo(Material material) {
        gameObject.GetComponentInChildren<MeshRenderer>().material = material;
    }

    IEnumerator StartCallback(bool highlight) {
        yield return null;
        pathFindCallback(this, highlight);
    }

    public void SetTileMaterialAndRotation() {
        Node parent = parentNode;
        if (parent != null) {
            Vector2 direction = parent.GetDirectionVector(this);

            GetTileChild().transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
            
            if (parent.parentNode != null) {
                Vector2 parentDirection = parent.parentNode.GetDirectionVector(parent);
                if (Mathf.Abs(direction.x) - Mathf.Abs(parentDirection.x) != 0 || Mathf.Abs(direction.y) - Mathf.Abs(parentDirection.y) != 0) {
                    parent.GetComponent<Tile>().ChangeTileMaterial(TileGrid.ArrowCorner);
                    parent.GetComponent<Tile>().GetTileChild().transform.rotation = Quaternion.Euler(0, GetRotation(direction, parentDirection), 0);
                }
            }

            ChangeTileMaterial(TileGrid.ArrowStraight);
        }
    }

    public GameObject GetTileChild() {
        return transform.GetChild(0).gameObject;
    }

    public void ChangeTileMaterial(Material material) {
        GetTileChild().GetComponent<MeshRenderer>().material = material;
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
        newMaterials[0] = GetTileChild().GetComponent<MeshRenderer>().material;
        newMaterials[1] = material;
        GetTileChild().GetComponent<MeshRenderer>().materials = newMaterials;
    }

    public void RemoveLastMaterial() {
        Material[] newMaterials = new Material[1];
        newMaterials[0] = GetTileChild().GetComponent<MeshRenderer>().material;
        GetTileChild().GetComponent<MeshRenderer>().materials = newMaterials;
    }
}
