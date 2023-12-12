using System.Collections.Generic;
using UnityEngine;

public class DataBase : SingletonBehaviour<DataBase>
{
    public Dictionary<int, MonsterData> monsterData = new Dictionary<int, MonsterData>();
    void Awake()
    {
        LoadAll();
    }

    private void LoadAll()
    {
        LoadMonsters();
    }

    private Dictionary<int, T> Load<T>(string path) where T : Object, IData
    {
        var dict = Resources.LoadAll<T>(path);

        var result = new Dictionary<int, T>(dict.Length);

        foreach (var pair in dict)
            result.Add(pair.GetId(), pair);

        return result;
    }

    private void LoadMonsters()
    {
        monsterData = Load<MonsterData>("Data/Monster");
    }
}