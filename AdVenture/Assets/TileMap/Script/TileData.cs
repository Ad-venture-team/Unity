using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newTileData", menuName = "Data/TileData")]
public class TileData : ScriptableObject, IData
{
    public int id;

    public string name;

    public TileBase tileBase;

    public int GetId()
    {
        return id;
    }

}
