﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OutputTile : Tile
{
    public Sprite onSprite;
    public Sprite offSprite;
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        if(tilemap.GetTile(location))
        {
            Circuit.AddComponent(location, new OutputComponent(location, tilemap));
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
        if(Circuit.circuitComponents[location].on)
        {
            tileData.sprite = onSprite;
        }
        else
        {
            tileData.sprite = offSprite;
            Debug.Log("offSprite");
        }
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/OutputTile")]
    public static void CreateOutputTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save OutputTile", "New OutputTile", "Asset", "Save OutputTile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<OutputTile>(), path);
    }
    #endif
}
