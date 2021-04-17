using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitLoader : MonoBehaviour
{
    public Tilemap tilemap;
    public Sprite[] wireSprites;
    public Sprite[] twoWiresSprites;
    public Sprite andSprite;
    public Sprite orSprite;
    public Sprite notSprite;
    public Sprite inputOnSprite;
    public Sprite inputOffSprite;
    public Sprite outputSprite;
    public void Awake()
    {
        Debug.Log(Circuit.circuitComponents.Count);
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase current = tilemap.GetTile(pos);

            if(current is WireTile)
            {
                WireTile wire = ScriptableObject.CreateInstance<WireTile>();
                wire.wireSprites = wireSprites;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), wire);
            }
            else if(current is AndTile)
            {
                AndTile and = ScriptableObject.CreateInstance<AndTile>();
                and.trueAndSprites = twoWiresSprites;
                and.sprite = andSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), and);
            }
            else if(current is OrTile)
            {
                OrTile or = ScriptableObject.CreateInstance<OrTile>();
                or.trueOrSprites = twoWiresSprites;
                or.sprite = orSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), or);
            }
            else if(current is NotTile)
            {
                NotTile not = ScriptableObject.CreateInstance<NotTile>();
                not.sprite = notSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), not);
            }
            else if(current is InputOnTile)
            {
                InputOnTile inputOn = ScriptableObject.CreateInstance<InputOnTile>();
                inputOn.sprite = inputOnSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOn);
            }
            else if(current is InputOffTile)
            {
                InputOffTile inputOff = ScriptableObject.CreateInstance<InputOffTile>();
                inputOff.sprite = inputOffSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOff);
            }
            else if(current is OutputTile)
            {
                OutputTile output = ScriptableObject.CreateInstance<OutputTile>();
                output.sprite = outputSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), output);
            }
            else
            {
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
            }
        }
        Debug.Log(Circuit.circuitComponents.Count);
    }
}
