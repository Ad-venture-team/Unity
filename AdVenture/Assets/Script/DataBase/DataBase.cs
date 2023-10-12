using System.Collections.Generic;
using UnityEngine;

public class DataBase : SingletonBehaviour<DataBase>
{
    void Awake()
    {
        LoadAll();
    }

    private void LoadAll()
    {
    }

    private Dictionary<int, T> Load<T>(string path) where T : Object, IData
    {
        var dict = Resources.LoadAll<T>(path);

        var result = new Dictionary<int, T>(dict.Length);

        foreach (var pair in dict)
            result.Add(pair.GetId(), pair);

        return result;
    }
}