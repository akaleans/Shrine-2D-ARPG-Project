using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    protected Animator myAnimator;
    private Rigidbody2D myRigidBody;

    protected Vector2 direction;
    protected bool isAttacking = false;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
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
        if (!isAttacking)
        {
            myRigidBody.velocity = direction.normalized * moveSpeed;
        }
        else if (isAttacking)
        {
            myRigidBody.velocity = direction.normalized * 0f;
        }
    }

    public void HandleLayers()
    {
        if (IsMoving && !isAttacking)
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);
        }
        else if (isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
            ActivateLayer("IdleLayer");
    }


    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < myAnimator.layerCount; ++i)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);
    }
}
