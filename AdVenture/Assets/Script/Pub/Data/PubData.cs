using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu (fileName = "P_",menuName ="Data/Pub")]
public class PubData : ScriptableObject,IData
{
    public int id;
    public string name;

    public Vector2 size;

    [SerializeReference] public PubContent content;

    public bool hasDelay;
    [ShowIf("hasDelay"), Indent] public bool showDelay;
    [ShowIf("hasDelay"), Indent] public float delay;
    [ShowIf("hasDelay"), Indent] public bool closeOnEnd;

    public int GetId()
    {
        return id;
    }
}
