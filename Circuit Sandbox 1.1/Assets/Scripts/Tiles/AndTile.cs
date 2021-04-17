using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AndTile : Tile
{
    public Tilemap realTilemap;
    public Sprite[] trueAndSprites;
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        realTilemap = tilemap.GetComponent<Tilemap>();

        TileBase previousTile;
        Circuit.RemoveComponent(location);
        tilemap.RefreshTile(location);
        if(tilemap.GetTile<AndTile>(location))
        {
            TrueAndTile andGate = ScriptableObject.CreateInstance<TrueAndTile>();
            andGate.gateWireSprites = trueAndSprites;

            previousTile = tilemap.GetTile(location + new Vector3Int(-1, 0, 0));
            andGate.replacedTile = previousTile;
            realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), andGate);
        }
        else
        {
            CircuitComponent component = Circuit.circuitComponents[location + new Vector3Int(-1, 0, 0)];
            if(component is AndGate)
            {
                LogicGate gate = (LogicGate)component;
                realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), gate.previousTile);
            }
        }
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/AndTile")]
    public static void CreateAndTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save AndTile", "New AndTile", "Asset", "Save AndTile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AndTile>(), path);
    }
    #endif
}
