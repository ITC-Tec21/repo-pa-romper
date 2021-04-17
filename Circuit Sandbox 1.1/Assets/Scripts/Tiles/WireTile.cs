using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class WireTile : Tile
{
    public Sprite[] wireSprites;
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        if(tilemap.GetTile(location))
        {
            Circuit.AddComponent(location, new Wire(location, tilemap));
        }
        else
        {
            Circuit.RemoveComponent(location);
        }
        tilemap.RefreshTile(location);

        if(Circuit.circuitComponents.ContainsKey(location))
        {
            CircuitComponent component = Circuit.circuitComponents[location];
            foreach(CircuitComponent inComponent in component.ins)
            {
                tilemap.RefreshTile(inComponent.location);
            }
            foreach(CircuitComponent outComponent in component.outs)
            {
                tilemap.RefreshTile(outComponent.location);
            }
        }
    }
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        if (!Circuit.circuitComponents.ContainsKey(location)) {
            return;
        }
        CircuitComponent component = Circuit.circuitComponents[location];
        HashSet<Vector3Int> borders = new HashSet<Vector3Int>();
        foreach(CircuitComponent inComponent in component.ins)
        {
            borders.Add(inComponent.location);
        }
        foreach(CircuitComponent outComponent in component.outs)
        {
            borders.Add(outComponent.location);
        }
        
        int mask = borders.Contains(location + new Vector3Int(0, 1, 0)) ? 1 : 0;
        mask += borders.Contains(location + new Vector3Int(1, 0, 0)) ? 2 : 0;
        mask += borders.Contains(location + new Vector3Int(0, -1, 0)) ? 4 : 0;
        mask += borders.Contains(location + new Vector3Int(-1, 0, 0)) ? 8 : 0;
        mask += borders.Contains(location + new Vector3Int(-2, 0, 0)) ? 16 : 0;

        int index = GetIndex((byte)mask);
        if (index >= 0 && index < wireSprites.Length)
        {
            tileData.sprite = wireSprites[index];
        }
        else
        {
            Debug.LogWarning("Not enough sprites in WireTile instance, recieved index: " + index);
        }

        if(Circuit.circuitComponents[location].on)
        {
            tileData.sprite = wireSprites[0];
        }
    }
    private int GetIndex(byte mask)
    {
        switch (mask)
        {
            case 0: return 0;
            case 1: return 1;
            case 2: return 2;
            case 3: return 3;
            case 4: return 1;
            case 5: return 1;
            case 6: return 4;
            case 7: return 8;
            case 8: return 2;
            case 9: return 6;
            case 10: return 2;
            case 11: return 7;
            case 12: return 5;
            case 13: return 10;
            case 14: return 9;
            case 15: return 11;
            case 16: return 2;
            case 17: return 6;
            case 18: return 2;
            case 19: return 7;
            case 20: return 5;
            case 21: return 10;
            case 22: return 9;
            case 23: return 11;
            default: return -1;
        }
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/WireTile")]
    public static void CreateWireTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Wire Tile", "New Wire Tile", "Asset", "Save Wire Tile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WireTile>(), path);
    }
    #endif
}
