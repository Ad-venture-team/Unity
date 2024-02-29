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

    public Animator animator;
    public SpriteRenderer visual;

    [HideInInspector] public bool lockInState;

    private void Awake()
    {
        if (data != null)
            SetData(data);
    }

    public void SetData(MonsterData _data)
    {
        data = _data;
        allAction = data.GetActions();
        float fMaxHealth = data.maxHealth;
        List<float> healthMod = MasterUpgradeManager.Instance.GetUpgradesValue(data.type, UpgradeType.MAX_HEALTH);
        for (int i = 0; i < healthMod.Count; i++)
            fMaxHealth += healthMod[i] * data.maxHealth;

        maxHealth = (int)fMaxHealth;
        health = maxHealth;
        animator.runtimeAnimatorController = _data.animationController;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void FixedUpdate()
    {
        currentState?.UpdateState(this);

        MonsterActionState action = SelectAction();

        if (action == null || action == currentState || lockInState)
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

    public int GetAttackValue()
    {
        List<float> mods = MasterUpgradeManager.Instance.GetUpgradesValue(data.type, UpgradeType.DAMAGE);
        float result = data.baseAttack;
        for (int i = 0; i < mods.Count; i++)
            result += data.baseAttack * mods[i];
        return (int)result;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
