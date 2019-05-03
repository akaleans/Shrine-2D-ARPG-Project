using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    public Animator MyAnimator { get; set; }

    protected Rigidbody2D myRigidBody;

    [SerializeField]
    protected Status health;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    protected Transform hitBox;

    private Transform target;

    public Transform MyTarget { get => target; set => target = value; }

    private Vector2 direction;
    public bool IsAttacking { get; set; }
    public bool IsAlive
    {
        get
        {
            return health.MyCurrentValue > 0;
        }
    }

    public bool IsMoving
    {
        get
        {
            return Direction.x != 0 || Direction.y != 0;
        }
    }

    public Vector2 Direction { get => direction; set => direction = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health.Initialize(maxHealth, maxHealth);
        MyAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                myRigidBody.velocity = Direction.normalized * MoveSpeed;
            }
            else if (IsAttacking)
            {
                myRigidBody.velocity = Direction.normalized * 0f;
            }
        }
    }

    public void HandleLayers()
    {
        if (IsAlive)
        {
            if (IsMoving && !IsAttacking)
            {
                ActivateLayer("WalkLayer");

                MyAnimator.SetFloat("x", Direction.x);
                MyAnimator.SetFloat("y", Direction.y);
            }
            else if (IsAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
                ActivateLayer("IdleLayer");
        }
        else ActivateLayer("DeathLayer");
    }


    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < MyAnimator.layerCount; ++i)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking);
    }

    public virtual void TakeDamage(float damage, Transform source)
    {
        health.MyCurrentValue -= damage;

        if(health.MyCurrentValue <= 0)
        {
            direction = Vector2.zero;
            myRigidBody.velocity = direction;
            MyAnimator.SetTrigger("die");
        }
    }

    //for enemy for later
    //private bool InLineOfSight()
    //{
    //    Vector3 targetDirection = (targetDirection.transform.position - transform.position);

    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection,
    //                                        Vector2.Distance(transform.position, target.transform.position));

    //    if (hit.collider == null)
    //    {
    //        return true;
    //    }

    //    return false;
    //}
}
