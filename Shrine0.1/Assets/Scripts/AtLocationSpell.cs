using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtLocationSpell : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    private Vector2 direction;

    [SerializeField]
    private float spellDuration;

    private bool alive = true;

    public void SpellDirection(Vector2 spellDirection)
    {
        direction = spellDirection;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, spellDuration);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (alive)
        {
            myRigidBody.velocity = direction.normalized * 0.01f;
        }
    }

}
