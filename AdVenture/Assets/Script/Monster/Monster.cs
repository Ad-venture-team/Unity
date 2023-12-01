using UnityEngine;
using System.Collections.Generic;

public class Monster : MonoBehaviour, IDamageable
{
    public MonsterData data;

    public Transform target;

    private List<MonsterActionState> allAction;
    public MonsterActionState currentState;

    public int maxHealth;
    public int health;

    private void Awake()
    {
        if (data != null)
            SetData(data);
    }

    public void SetData(MonsterData _data)
    {
        data = _data;
        allAction = data.GetActions();
        maxHealth = _data.maxHealth;
        health = maxHealth;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void FixedUpdate()
    {
        currentState?.UpdateState(this);

        MonsterActionState action = SelectAction();

        if (action == null || action == currentState)
            return;

        ChangeState(action);
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

    public void TakeDamage(int _value)
    {
        health -= _value;
        if(IsDead())
        {
            gameObject.SetActive(false);
            EventWatcher.DoOnMonsterDie(this);
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
