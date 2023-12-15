using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2Int nMonster;
    public Vector2Int minRoomSize;
    public Vector2Int maxRoomSize;
    private void Awake()
    {
        EventWatcher.onEndRoom += NewRoom;
    }

    private void OnDestroy()
    {
        EventWatcher.onEndRoom -= NewRoom;
    }

    private void Start()
    {
        CreateNewRoom();
    }

    private void CreateNewRoom()
    {
        int n = RandomUtils.GetRandom(nMonster.x, nMonster.y);
        Vector2Int roomSize = RandomUtils.RandomVector2Int(minRoomSize, maxRoomSize);
        Room firstRoom = new Room(roomSize.x, roomSize.y);
        List<MonsterData> monsters = DataBase.Instance.monsterData.Values.ToList();
        for (int i = 0; i < n; i++)
        {
            int rand = RandomUtils.GetRandom(0, monsters.Count);
            Vector2 pos = RandomUtils.RandomVector2(Vector2.zero, roomSize);
            MonsterData data = monsters[rand];
            RoomElement newRoomElem = new RoomElement();
            newRoomElem.id = data.id;
            newRoomElem.posX = pos.x;
            newRoomElem.posY = pos.y;
            firstRoom.monsters.Add(newRoomElem);
        }
        EventWatcher.DoOnNewRoom(firstRoom);
    }

    private async void NewRoom()
    {
        await Task.Delay(3000);
        CreateNewRoom();
    }
}
