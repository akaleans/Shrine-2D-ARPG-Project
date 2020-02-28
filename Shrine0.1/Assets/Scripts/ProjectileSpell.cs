using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float spellSpeed;

    [SerializeField]
    private float spellDamage;

    private Vector2 direction;

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
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (alive)
        {
            myRigidBody.velocity = direction.normalized * spellSpeed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Character c = collision.GetComponentInParent<Character>();
            c.TakeDamage(spellDamage, source);
            GetComponent<Animator>().SetTrigger("hit");
            spellSpeed = 0.5f;
        }
    }
}
