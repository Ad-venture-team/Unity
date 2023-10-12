using System;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly Lazy<T> LazyInstance = new Lazy<T>(() =>
    {
        var instance = FindObjectOfType<T>();
        string objectName = $"[singleton]{typeof(T).Name}";

        if (instance == null)
        {
            var obj = new GameObject(objectName);
            instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
            return instance;
        }

        instance.name = objectName;
        DontDestroyOnLoad(instance);
        return instance;
    });
    public static T Instance => LazyInstance.Value;
}
