using UnityEngine;
using DG.Tweening;

public class Monster : MonoBehaviour
{
    public MonsterData data;

    public Transform target;
    public float roamThreshold;

    private MonsterStateManager stateMachine;

    private Vector3 roamTarget;
    private float roamDelay;
    private float attackDelay;

    public void SetData(MonsterData _data)
    {
        data = _data;
    }
    void Awake()
    {
        stateMachine = new MonsterStateManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine?.UpdateState();
    }


    public void NewRoamPosition()
    {
        roamTarget = transform.position + Random.insideUnitSphere * data.roamRange;
        roamDelay = data.waitingTime;
    }
    public void Roam()
    {
        if (roamDelay > 0)
        {
            roamDelay -= Time.deltaTime;
            return;
        }

        if (roamTarget == null)
            NewRoamPosition();

        float dist = Vector2.Distance(transform.position, roamTarget);
        if (dist <= roamThreshold)
            NewRoamPosition();

        transform.position += (roamTarget - transform.position).normalized * data.roamSpeed * Time.deltaTime;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    public void MoveToTarget()
    {
        if (target == null)
            return;

        //Voir pour mettre un A* ici

        transform.position += (target.position - transform.position).normalized * data.movementSpeed * Time.deltaTime;
    }

    public void DoAttack()
    {
        if (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
            return;
        }
        LaunchAttack();
    }

    public void LaunchAttack()
    {
        transform.DOPunchPosition((target.position - transform.position).normalized,.2f);
        attackDelay = data.attackSpeed;
    }

    public bool IsInVisionRange()
    {
        float dist = Vector2.Distance(transform.position, target.position);
        if (dist <= data.visionRange)
            return true;
            
        return false;
    }
    public bool IsInAttackRange()
    {
        float dist = Vector2.Distance(transform.position, target.position);
        if (dist <= data.attackRange)
            return true;

        return false;
    }


    private void OnDrawGizmos()
    {
        if (data == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.visionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }
}
