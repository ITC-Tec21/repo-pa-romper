using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TrueGateTile : Tile
{
    public Sprite[] gateWireSprites;
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        HashSet<Vector3Int> borders = new HashSet<Vector3Int>();
        if(!Circuit.circuitComponents.ContainsKey(location))
        {

            return;
        }
        foreach (CircuitComponent component in Circuit.circuitComponents[location].ins)
        {
            borders.Add(component.location);
        }

        int mask = borders.Contains(new Vector3Int(-1, 0, 0) + location) ? 1 : 0;
        mask += borders.Contains(new Vector3Int(0, 1, 0) + location) ? 2 : 0;
        mask += borders.Contains(new Vector3Int(0, -1, 0) + location) ? 4 : 0;
        mask += borders.Contains(new Vector3Int(-2, 0, 0) + location) ? 8 : 0;

        int index = GetIndex((byte)mask);
        if (index >= 0 && index < gateWireSprites.Length)
        {
            tileData.sprite = gateWireSprites[index];
        }
        else
        {
            Debug.LogWarning("Not enough sprites in TrueGateTile instance, index " + index);
        }
    }
    private int GetIndex(byte mask)
    {
        switch(mask)
        {
            case 0: return 0;
            case 1: return 1;
            case 2: return 0;
            case 3: return 1;
            case 4: return 0;
            case 5: return 2;
            case 6: return 0;
            case 7: return 0;
            case 8: return 1;
            case 10: return 1;
            case 12: return 2;
            case 14: return 0;
            default: return -1;
        }
    }
}