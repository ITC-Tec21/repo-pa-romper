using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitLoader : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite[] testSprite;
    public void Awake()
    {
        Debug.Log(Circuit.circuitComponents.Count);
        for(int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for(int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                WireTile wire = ScriptableObject.CreateInstance<WireTile>();
                wire.wireSprites = testSprite;
                tilemap.SetTile(new Vector3Int(x, y, 0), wire);
            }
        }
        Debug.Log(Circuit.circuitComponents.Count);
    }
}
