using System;
using UnityEngine;

public class SingletonInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected SingletonInstance() { }

    protected virtual void SingleAwake() { }

    protected void Awake()
    {
        Instance = this.GetComponent<T>();
        SingleAwake();
    }
}
