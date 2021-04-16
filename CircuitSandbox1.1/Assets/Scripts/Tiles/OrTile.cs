using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OrTile : Tile
{
    public Tilemap realTilemap;
    public Sprite[] trueAndSprites;
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        realTilemap = tilemap.GetComponent<Tilemap>();

        Circuit.RemoveComponent(location);
        tilemap.RefreshTile(location);
        if(tilemap.GetTile(location))
        {
            TrueOrTile orGate = ScriptableObject.CreateInstance<TrueOrTile>();
            orGate.gateWireSprites = trueAndSprites;
            realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), orGate);
        }
        else
        {
            realTilemap.SetTile(location + new Vector3Int(-1, 0, 0), null);
        }
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/OrTile")]
    public static void CreateAndTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save OrTile", "New OrTile", "Asset", "Save OrTile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<OrTile>(), path);
    }
    #endif
}
