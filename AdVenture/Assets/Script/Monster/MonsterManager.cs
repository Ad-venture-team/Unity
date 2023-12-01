using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Transform monsterContent;
    public Transform playerTransform;

    public Monster monsterPrefab;
    List<Monster> monsters = new List<Monster>();

    private void OnEnable()
    {
        EventWatcher.getMonsterList += RequestMonsters;
    }

    private void OnDisable()
    {
        EventWatcher.getMonsterList -= RequestMonsters;
    }

    public void AddMonster(MonsterData _data, Vector2 _position)
    {
        Monster newMonster = Instantiate(monsterPrefab, _position, Quaternion.identity, monsterContent);
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
