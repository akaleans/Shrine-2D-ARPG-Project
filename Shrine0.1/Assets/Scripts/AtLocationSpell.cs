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

    private Transform source;

    public void Initialize(Vector2 spellDirection, float damage, Transform source)
    {
        this.source = source;
        direction = spellDirection;
        spellDamage = damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, spellDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Character c = collision.GetComponentInParent<Enemy>();
            c.TakeDamage(spellDamage, source);
            GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
