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

    public float MyAttackRange { get => myAttackRange; set => myAttackRange = value; }
    public float MyCooldown { get => myCooldown; set => myCooldown = value; }
    public float MyAttackSpeed { get => myAttackSpeed; set => myAttackSpeed = value; }

    private Transform target;

    public Transform Target { get => target; set => target = value; }

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
}
