using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class TestTile : Tile
{

    public Sprite testSprite;
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        
        bool testTileThere = tilemap.GetTile(location);
        Debug.Log("There is a tile here (GetTileData): " + location + testTileThere);
        tileData.sprite = testSprite;
    }
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        bool testTileThere = tilemap.GetTile(position);
        Debug.Log("There is a tile here (Refresh): " + position + testTileThere);
        tilemap.RefreshTile(position);
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/TestTile")]
    public static void CreateTestTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Test Tile", "New Test Tile", "Asset", "Save Test Tile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TestTile>(), path);
    }
    #endif
}
