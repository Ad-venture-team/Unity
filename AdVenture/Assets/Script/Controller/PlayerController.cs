using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : SingletonInstance<PlayerController>
{
    public enum CharacterState
    {
        FREE,
        UI,
    }

    public CharacterState characterState;


    [SerializeField] private PlayerAction playerActionControl;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private RectTransform healthBar;
    private float health;
    [SerializeField] private WeaponData weapon;
    private float attackDelay;
    private Vector2 moveInput = new Vector2(0,0);

    protected override void SingleAwake()
    {
        playerActionControl = new PlayerAction();
        health = maxHealth;
        InitEvent();
    }

    private void Update()
    {
        if (attackDelay > 0)
            attackDelay -= Time.deltaTime;
        Move(moveInput);
        
        if(this.health > 0)
            healthBar.localScale = new Vector3((float)this.health / (float)this.maxHealth, 1, 1);
    }

    private void OnEnable()
    {
        playerActionControl.Enable();
    }

    private void OnDisable()
    {
        playerActionControl.Disable();
    }


    private void InitEvent()
    {
        playerActionControl.Player.Walk.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerActionControl.Player.Walk.canceled += ctx => moveInput = Vector2.zero;
    }

    private void Move(Vector3 parametre)
    {
        float plusX = 0;
        float plusY = 0;

        if(parametre == Vector3.zero)
        {
            Attack();
            return;
        }

        switch(parametre.x)
        {
            case > 0 :
                plusX = 1*speed;
                break;
            case < 0: 
                plusX = -1 * speed;
                break;
        }
        switch (parametre.y)
        {
            case > 0 :
                plusY = 1 * speed;
                break;
            case < 0:
                plusY =-1 * speed;
                break;
        }

        transform.position += parametre.normalized * speed * Time.deltaTime;
    }

    private void Attack()
    {
        if (attackDelay > 0)
            return;

        Monster target = GetClosestMonster();
        if (target == null || ((target.transform.position - transform.position).sqrMagnitude > weapon.range* weapon.range))
        return;

        weapon.SetData(transform, target.transform);
        attackDelay = weapon.attackDelay;
    }

    public void TakeDamage(int _value)
    {
        health -= _value;
        //EventPlayerLoseHealth
        Debug.Log($"Loss {_value} health");
        if (health <= 0)
        {
            healthBar.localScale = new Vector3(0, 1, 1);
        }
            //Application.Quit();
    }

    private Monster GetClosestMonster()
    {
        List<Monster> monsters = new List<Monster>();
        EventWatcher.DoGetMonsterList(ref monsters);
        monsters.RemoveAll(x => x.IsDead());
        if (monsters.Count == 0)
            return null;
        return monsters.OrderBy(t => (t.transform.position-transform.position).sqrMagnitude).First();
    }
}
