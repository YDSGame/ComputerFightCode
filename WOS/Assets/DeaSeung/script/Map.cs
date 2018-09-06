using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public Transform tilePrefab;
    public Vector2 MapSize;

    [Range(0,1)]
    public float outlinePercent;
    private void Start()
    {
        GernerateMap();
    }
    public void  GernerateMap()
    {
        string TileChild = "BuildMap";
        if(transform.Find(TileChild))
        {
            DestroyImmediate(transform.Find(TileChild).gameObject);
        }
        Transform mapHolder = new GameObject(TileChild).transform;
        mapHolder.parent = transform;
         for(int x= 0; x <MapSize.x; x++)
        {
            for(int y=0;y<MapSize.y; y++)
            {
                Vector3 tilePosition = new Vector3(-this.transform.position.x / 2 + 0.5f + x, 0, -this.transform.position.y / 2 + 0.5f + y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }
    }
}
