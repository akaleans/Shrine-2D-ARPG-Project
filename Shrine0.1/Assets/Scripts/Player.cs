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

    [SerializeField]
    private Transform[] exitPoints;
    private int exitIndex;
    
    private GameObject projectile;

    private Ray spellDirection;

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

            //get direction for spell to travel
            spellDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(spellDirection);
            spellDirection.direction = new Vector3(spellDirection.direction.x, spellDirection.direction.y, 0f);

            //set direction for sprites
            myAnimator.SetFloat("x", spellDirection.direction.x);
            myAnimator.SetFloat("y", spellDirection.direction.y);

            //get exitpoint
            GetExitPoint(spellDirection);

            yield return new WaitForSeconds(1); // cast time

            projectile = Instantiate(spellPrefabs[0], exitPoints[exitIndex].position, Quaternion.identity);
            projectile.GetComponent<Spell>().SpellDirection(spellDirection.direction);

            StopAttack();
        }
    }

    private void GetExitPoint(Ray direc)
    {
        if (direc.direction.x > 0 && direc.direction.y != 0)
        {
            if (direc.direction.y > 0)
            {
                if (direc.direction.y > direc.direction.x) // up
                {
                    exitIndex = 0;
                }
                else if (direc.direction.y < direc.direction.x) // right
                {
                    exitIndex = 1;
                }
            }
            else if (direc.direction.y < 0)
            {
                if (Mathf.Abs(direc.direction.y) > direc.direction.x) // down
                {
                    exitIndex = 2;
                }
                else if (Mathf.Abs(direc.direction.y) < direc.direction.x) // right
                {
                    exitIndex = 1;
                }
            }
        }
        else if (direc.direction.x < 0 && direc.direction.y != 0)
        {
            if (direc.direction.y > 0)
            {
                if (direc.direction.y > Mathf.Abs(direc.direction.x)) // up
                {
                    exitIndex = 0;
                }
                else if (direc.direction.y < Mathf.Abs(direc.direction.x)) // left
                {
                    exitIndex = 3;
                }
            }
            else if (direc.direction.y < 0)
            {
                if (Mathf.Abs(direc.direction.y) > Mathf.Abs(direc.direction.x)) // down
                {
                    exitIndex = 2;
                }
                else if (Mathf.Abs(direc.direction.y) < Mathf.Abs(direc.direction.x)) // left
                {
                    exitIndex = 3;
                }
            }
        }
        else if (direc.direction.x == 0 && direc.direction.y != 0)
        {
            if (direc.direction.y > 0)
            {
                exitIndex = 0; //up
            }
            else if (direc.direction.y < 0)
            {
                exitIndex = 2; //down
            }
        }
        else if (direc.direction.x != 0 && direc.direction.y == 0)
        {
            if (direc.direction.x > 0)
            {
                exitIndex = 1; //right
            }
            else if (direc.direction.x < 0)
            {
                exitIndex = 3; //left
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        base.Update();
    }
}
