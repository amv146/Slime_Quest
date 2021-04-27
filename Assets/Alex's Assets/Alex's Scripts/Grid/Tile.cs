using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ClickCallback(int x, int y);

public class Tile : MonoBehaviour
{
    [System.NonSerialized]
    public int tileX;
    public int tileZ;
    public ClickCallback clickCallback;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp() {
        clickCallback(tileX, tileZ);
    }

    public void SetTileCoords(int x, int z) {
        tileX = x;
        tileZ = z;
    }
}
