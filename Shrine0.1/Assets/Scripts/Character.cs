using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float moveSpeed;
    protected Animator anim;
    protected Rigidbody2D myRigidBody;

    protected bool isMoving;
    protected Vector2 lastMove;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        AnimateMovement();
    }

    public void AnimateMovement()
    {
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

}
