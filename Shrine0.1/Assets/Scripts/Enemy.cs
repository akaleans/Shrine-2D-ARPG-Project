using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    private IEnemyState currentState;

    [SerializeField]
    private float myAttackRange;
    [SerializeField]
    private float myCooldown;
    [SerializeField]
    private float myAttackSpeed;
    [SerializeField]
    private float initAggroRange;

    public float MyAttackRange { get => myAttackRange; set => myAttackRange = value; }
    public float MyCooldown { get => myCooldown; set => myCooldown = value; }
    public float MyAttackSpeed { get => myAttackSpeed; set => myAttackSpeed = value; }
    public float MyAggroRange { get; set; }
    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyCooldown += Time.deltaTime;
            }
            currentState.Update();
        }
        base.Update();
    }

    protected void Awake()
    {
        MyAggroRange = initAggroRange;
        ChangeState(new IdleState());
    }

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public override void TakeDamage(float damage, Transform source)
    {
        SetTarget(source);

        base.TakeDamage(damage, source);
    }

    public void SetTarget(Transform target)
    {
        if(MyTarget == null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {
        MyTarget = null;
        MyAggroRange = initAggroRange;
    }
}
