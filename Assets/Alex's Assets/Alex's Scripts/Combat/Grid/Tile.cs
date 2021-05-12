using System.Collections;
using System.Collections.Generic;
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
        if (!isHighlighted && gameObject.GetComponent<MeshRenderer>().material == tileGrassHighlighted) {
            SetMaterialTo(tileGrass);
        }
        else if (isHighlighted && gameObject.GetComponent<MeshRenderer>().material == tileGrass) {
            SetMaterialTo(tileGrassHighlighted);
        }
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
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

    IEnumerator StartCallback(bool highlight) {
        yield return null;
        pathFindCallback(this, highlight);
    }
}
