using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubData : ScriptableObject,IData
{
    public int id;
    public string name;

    public Vector2 size;

    public PubContent content;

    public int GetId()
    {
        return id;
    }
}
