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

    [SerializeField]
    private GameObject[] spellPrefabs;
    
    private GameObject projectile;

    private Vector3 mousePos;
    private Vector2 spellDirection;

    private static bool playerExists;

    // Start is called before the first frame update
    protected override void Start()
    {
        health.Initialize(maxHealth, maxHealth);
        mana.Initialize(maxMana, maxMana);

        base.Start();

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
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(LeftMouseAttack()); // simultaneously
        }
    }

    private IEnumerator LeftMouseAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            myAnimator.SetBool("attack", isAttacking);

            Ray spellDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
            spellDirection.direction = new Vector3(spellDirection.direction.x, spellDirection.direction.y, 0f);

            yield return new WaitForSeconds(1); // cast time

            projectile = Instantiate(spellPrefabs[0], transform.position, Quaternion.identity);
            projectile.GetComponent<Spell>().SpellDirection(spellDirection.direction);

            StopAttack();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        base.Update();
    }
}
