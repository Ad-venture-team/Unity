using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapsManager : MonoBehaviour
{

    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap wall;

    private void Awake()
    {
        EventWatcher.onNewRoom += MapCreator;
    }

    private void MapCreator(Room room)
    {
        Room createRoom = room;
        ground.ClearAllTiles();

        for(int x = 0; x < createRoom.width; x++)
        {
            for(int y = 0; y < createRoom.height; y++)
            {

                ground.SetTile(new Vector3Int(x, y, 0), DataBase.Instance.tileData[createRoom.map[x, y]].tileBase);
            }
        }

        for (int x = -1; x < createRoom.width + 1; x++)
        {
            wall.SetTile(new Vector3Int(x, createRoom.height, 0), DataBase.Instance.tileData[1].tileBase);
            wall.SetTile(new Vector3Int(x, -1, 0), DataBase.Instance.tileData[1].tileBase);
        }

        for(int y = -1; y < createRoom.height + 1; y++)
        {
            wall.SetTile(new Vector3Int(createRoom.width, y, 0), DataBase.Instance.tileData[1].tileBase);
            wall.SetTile(new Vector3Int(-1, y, 0), DataBase.Instance.tileData[1].tileBase);
        }


    }
}
