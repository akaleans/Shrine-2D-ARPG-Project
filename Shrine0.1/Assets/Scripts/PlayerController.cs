using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;
    private bool isMoving;
    private Vector2 lastMove;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;

        float moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        if(moveVertical != 0f && moveHorizontal != 0f)
        {
            moveHorizontal *= 0.7071f;
            moveVertical *= 0.7071f;
            isMoving = true;
            transform.Translate(new Vector3(moveHorizontal, moveVertical, 0f));
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        if(moveHorizontal != 0f && moveVertical == 0f)
        {
            isMoving = true;
            transform.Translate(new Vector3(moveHorizontal, 0f, 0f));
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        if(moveVertical != 0f && moveHorizontal == 0f)
        {
            isMoving = true;
            transform.Translate(new Vector3(0f, moveVertical, 0f));
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
