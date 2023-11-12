using UnityEngine;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
    public MonsterData data;

    public Transform target;

    private List<MonsterActionState> allAction;
    public MonsterActionState currentState;
    public bool lockInState;

    private void Awake()
    {
        if (data != null)
            SetData(data);
    }

    public void SetData(MonsterData _data)
    {
        data = _data;
        allAction = data.GetActions();
    }

    void FixedUpdate()
    {
        currentState?.UpdateState(this);

        if (lockInState)
            return;

        MonsterActionState action = SelectAction();

        if (action == null || action == currentState)
            return;

        ChangeState(action);

        //Debug.Log("DoAction : " + action.GetType().Name + " || " + action.Evaluate(this));
    }

    private MonsterActionState SelectAction()
    {
        MonsterActionState selectedAction = null;
        float evaluation = 0;

        foreach (MonsterActionState MA in allAction)
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

    private void ChangeState(MonsterActionState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState?.EnterState(this);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
