using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;
    private Rigidbody2D myRigidBody;

    private bool isMoving;
    private Vector2 lastMove;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;

        float moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float moveVertical = Input.GetAxisRaw("Vertical") * moveSpeed;

        if(moveVertical != 0f && moveHorizontal != 0f)
        {
            moveHorizontal *= 0.7071f;
            moveVertical *= 0.7071f;
            isMoving = true;
            myRigidBody.velocity = new Vector2(moveHorizontal, moveVertical);
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        if(moveHorizontal != 0f && moveVertical == 0f)
        {
            isMoving = true;
            myRigidBody.velocity = new Vector2(moveHorizontal, moveVertical);
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        if(moveVertical != 0f && moveHorizontal == 0f)
        {
            isMoving = true;
            myRigidBody.velocity = new Vector2(moveHorizontal, moveVertical);
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        if(moveHorizontal == 0 && moveVertical == 0)
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
