using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterManager : MonoBehaviour
{
    public Transform playerTransform;

    public Monster monsterPrefab;
    List<Monster> monsters = new List<Monster>();

    [SerializeField] private Tilemap mapTile;

    private void OnEnable()
    {
        EventWatcher.getMonsterList += RequestMonsters;
        EventWatcher.onNewRoom += LoadRoom;
    }

    private void OnDisable()
    {
        EventWatcher.getMonsterList -= RequestMonsters;
        EventWatcher.onNewRoom -= LoadRoom;
    }

    private void LoadRoom(Room _room)
    {
        foreach (Transform T in transform)
            Destroy(T.gameObject);

        monsters.Clear();

        foreach(RoomElement monsterElem in _room.monsters)
            AddMonster(DataBase.Instance.monsterData[monsterElem.id], new Vector2(monsterElem.posX, monsterElem.posY));
    }

    public void AddMonster(MonsterData _data, Vector2 _position)
    {
        Monster newMonster = Instantiate(monsterPrefab, _position, Quaternion.identity, transform);
        newMonster.SetData(_data);
        newMonster.SetTarget(playerTransform);
        monsters.Add(newMonster);
        EventWatcher.DoOnAddMonster(newMonster);
    }

    public void RemoveMonster(Monster _monster)
    {
        monsters.Remove(_monster);
        EventWatcher.DoOnRemoveMonster(_monster);
    }

    private void RequestMonsters(ref List<Monster> _monsters)
    {
        foreach (Monster M in monsters)
            _monsters.Add(M);
    }

}
