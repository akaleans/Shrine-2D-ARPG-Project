using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtLocationSpell : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    private Vector2 direction;

    [SerializeField]
    private float spellDuration;

    [SerializeField]
    private float spellDamage;

    private bool alive = true;

    public void Initialize(Vector2 spellDirection, float damage)
    {
        direction = spellDirection;
        spellDamage = damage;
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
            myRigidBody.velocity = direction.normalized * 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponentInParent<Enemy>().TakeDamage(spellDamage);
            GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
