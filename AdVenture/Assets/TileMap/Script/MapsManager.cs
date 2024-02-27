using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;
using MoreMountains.Tools;
using UnityEngine.TextCore.Text;

public class MapsManager : MonoBehaviour
{

    #region Singleton
    private static MapsManager _instance;
    public static MapsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapsManager>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField] private Tilemap ground;
    [SerializeField] private Tilemap wall;
    [SerializeField] private PolygonCollider2D camColider;
    [SerializeField] private CinemachineConfiner2D cinemachine;


    public HashSet<Vector3> GetValidePlace()
    {
        bool validePlace = true;
        List<Vector3Int> gridPos = new List<Vector3Int>();

        //foreach (Character character in playingTurn)
        //{
        //    if (character.controlle != Character.Controlle.DEAD)
        //    {
        //        gridPos.Add(grid.WorldToCell(character.gameObject.transform.position));
        //    }
        //}

        //TileBase tile = tilemap.GetTile(gridPos);
        TileBase[] tiles = ground.GetTilesBlock(ground.cellBounds);
        HashSet<Vector3> Map = new HashSet<Vector3>();

        for (int n = ground.cellBounds.xMin; n < ground.cellBounds.xMax; n++)
        {
            for (int p = ground.cellBounds.yMin; p < ground.cellBounds.yMax; p++)
            {
                validePlace = true;
                Vector3Int localPlace = (new Vector3Int(n, p, (int)ground.transform.position.y));
                Vector3 place = ground.CellToWorld(localPlace);
                if (ground.HasTile(localPlace))
                {
                    foreach (Vector3Int playerPlace in gridPos)
                    {
                        if (playerPlace == localPlace)
                        {
                            validePlace = false;
                            break;
                        }
                    }

                    if (validePlace == true) Map.Add(place); ///récupère toute les position jouable

                }
            }
        }

        return Map;
    }

    private void Awake()
    {
        EventWatcher.onNewRoom += MapCreator;
    }

    private void MapCreator(Room room)
    {
        Room createRoom = room;
        ground.ClearAllTiles();
        wall.ClearAllTiles();

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
