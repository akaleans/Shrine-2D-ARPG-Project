using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Player myPlayer;

    [SerializeField]
    private float spellSpeed;

    private Vector2 direction;
    private Vector3 worldMousePos;

    public void SpellDirection(Vector2 spellDirection)
    {
        direction = spellDirection;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = direction.normalized * spellSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
