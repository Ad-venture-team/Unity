using UnityEngine;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
    public MonsterData data;

    public Transform target;

    private List<MonsterAction> allAction;

    private void Awake()
    {
        if (data != null)
            SetData(data);
    }

    public void SetData(MonsterData _data)
    {
        data = _data;
        allAction = data.actions;
    }

    void Update()
    {
        MonsterAction action = SelectAction();
        if (action == null)
            return;

        action.DoAction(this);
        action.OnDoAction();

        Debug.Log("DoAction : " + action.GetType().Name + " || " + action.Evaluate(this));
    }

    private MonsterAction SelectAction()
    {
        MonsterAction selectedAction = null;
        float evaluation = 0;

        foreach (MonsterAction MA in allAction)
        {
            float currentEval = MA.Evaluate(this);
            if (currentEval > evaluation)
            {
                selectedAction = MA;
                evaluation = currentEval;
            }
        }

        return selectedAction;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
