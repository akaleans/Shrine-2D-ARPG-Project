using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Status health;

    [SerializeField]
    private Status mana;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float maxMana;

    private static bool playerExists;

    // Start is called before the first frame update
    protected override void Start()
    {
        health.Initialize(maxHealth, maxHealth);
        mana.Initialize(maxMana, maxMana);

        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        base.Update();
    }

    protected override void Move()
    {
        isMoving = false;

        float moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float moveVertical = Input.GetAxisRaw("Vertical") * moveSpeed;

        if (moveVertical != 0f && moveHorizontal != 0f)
        {
            moveHorizontal *= 0.7071f;
            moveVertical *= 0.7071f;
            isMoving = true;
            myRigidBody.velocity = new Vector2(moveHorizontal, moveVertical);
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        if (moveHorizontal != 0f && moveVertical == 0f)
        {
            isMoving = true;
            myRigidBody.velocity = new Vector2(moveHorizontal, moveVertical);
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        if (moveVertical != 0f && moveHorizontal == 0f)
        {
            isMoving = true;
            myRigidBody.velocity = new Vector2(moveHorizontal, moveVertical);
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            myRigidBody.velocity = new Vector2(0f, 0f);
        }
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
