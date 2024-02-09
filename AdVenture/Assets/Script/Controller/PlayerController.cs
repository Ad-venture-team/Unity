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
    [SerializeField] private int maxHealth;
    [SerializeField] private RectTransform healthBar;
    private int health;
    [SerializeField] private WeaponData weapon;
    private float attackDelay;
    private Vector2 moveInput = new Vector2(0,0);
    private Dictionary<UpgradeType, List<float>> upgrades = new Dictionary<UpgradeType, List<float>>();

    protected override void SingleAwake()
    {
        playerActionControl = new PlayerAction();
        health = maxHealth;
        InitInputEvent();

        EventWatcher.onNewRoom += MovePlayerInRoomBound;
    }

    private void OnDestroy()
    {
        EventWatcher.onNewRoom -= MovePlayerInRoomBound;
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

    private void MovePlayerInRoomBound(Room _room)
    {
        Vector2 randomPos = RandomUtils.RandomVector2(Vector2.zero, new Vector2(_room.width-1, _room.height-1));
        transform.position = randomPos;
        Heal(100);
    }

    private void InitInputEvent()
    {
        playerActionControl.Player.Walk.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerActionControl.Player.Walk.canceled += ctx => moveInput = Vector2.zero;
    }

    private void Move(Vector3 parametre)
    {
        if(parametre == Vector3.zero)
        {
            Attack();
            return;
        }

        float speedModifier = 0;
        List<float> speedUpgrades = GetUpgradesValue(UpgradeType.SPEED);
        for (int i = 0; i < speedUpgrades.Count; i++)
            speedModifier += speedUpgrades[i];

        transform.position += parametre.normalized * (speed + speedModifier) * Time.deltaTime;
    }

    private void Attack()
    {
        if (attackDelay > 0)
            return;

        Monster target = GetClosestMonster();
        if (target == null || ((target.transform.position - transform.position).sqrMagnitude > weapon.range* weapon.range))
        return;

        float damageModifier = 0;
        List<float> dmgUpgrades = GetUpgradesValue(UpgradeType.DAMAGE);
        for (int i = 0; i < dmgUpgrades.Count; i++)
            damageModifier += dmgUpgrades[i];

        weapon.SetData(transform, target.transform, damageModifier);
        attackDelay = weapon.attackDelay;
        List<float> attSpeedUpgrades = GetUpgradesValue(UpgradeType.ATTACK_SPEED);
        for (int i = 0; i < attSpeedUpgrades.Count; i++)
            attackDelay *= attSpeedUpgrades[i];
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

    public void Heal(int _value, bool isMax = false)
    {
        if (isMax)
            maxHealth += _value;
        health += _value;
        health = Mathf.Clamp(health, 0, maxHealth);
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

    public void SetWeapon(int _id)
    {
        weapon = DataBase.Instance.weaponData[_id];
    }

    public void AddUpgrade(UpgradeType _type, float _value)
    {
        if (upgrades.ContainsKey(_type))
            upgrades[_type].Add(_value);
        else
            upgrades.Add(_type, new List<float> { _value });
    }

    private List<float> GetUpgradesValue(UpgradeType _type)
    {
        List<float> result = new List<float>();
        if (upgrades.ContainsKey(_type))
            for (int i = 0; i < upgrades[_type].Count; i++)
                result.Add(upgrades[_type][i]);
        return result;
    }
}
