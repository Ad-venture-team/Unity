using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPrefab<T> : MonoBehaviour where T : MonoBehaviour
{
    public virtual void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private static T m_Instance;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(T)) as T;
                if (m_Instance == null)
                {
                    T prefab = Resources.Load("Prefabs/Singletons/" + typeof(T).Name, typeof(T)) as T;
                    m_Instance = Instantiate(prefab);
                }
            }
            return m_Instance;

        }
    }
}
