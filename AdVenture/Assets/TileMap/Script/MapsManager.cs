using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;

public class MapsManager : MonoBehaviour
{

    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap wall;
    [SerializeField] private PolygonCollider2D camColider;
    [SerializeField] private CinemachineConfiner2D cinemachine;

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

        camColider.SetPath(0, new[] { new Vector2(-1, -1), new Vector2(createRoom.width +1, -1), new Vector2(createRoom.width +1, createRoom.height +5), new Vector2(-1, createRoom.height +5) });

        cinemachine.InvalidateCache();

    }
}
