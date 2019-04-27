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
    private int spellIndex;

    [SerializeField]
    private Transform[] exitPoints;
    private int exitIndex;
    
    private GameObject projectile;

    private Vector3 mousePos;
    private Ray spellDirection;

    private static bool playerExists;

    // Start is called before the first frame update
    protected override void Start()
    {
        health.Initialize(maxHealth, maxHealth);
        mana.Initialize(maxMana, maxMana);

        spellIndex = 0;

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellIndex = 2; //fireball NEED TO CHANGE CHECK ELEMENTS
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spellIndex = 3; //firewall NEED TO CHANGE CHECK ELEMENTS
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
            spellDirection.direction = new Vector3(spellDirection.direction.x, spellDirection.direction.y, 0f);

            //get location for atlocationspell
            mousePos = Input.mousePosition;
            mousePos.z = 15;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //set direction for animator
            myAnimator.SetFloat("x", spellDirection.direction.x);
            myAnimator.SetFloat("y", spellDirection.direction.y);

            //get exitpoint
            GetExitPoint(spellDirection);

            yield return new WaitForSeconds(1); // cast time

            StartCoroutine(CastSpell());

            StopAttack();
        }
    }

    private IEnumerator CastSpell()
    {
        if (spellIndex == 0 || spellIndex == 2) //projectile
        {
            yield return new WaitForSeconds(0f);
            projectile = Instantiate(spellPrefabs[spellIndex], exitPoints[exitIndex].position, Quaternion.identity);
            projectile.GetComponent<ProjectileSpell>().SpellDirection(spellDirection.direction);
        }
        else if (spellIndex == 3) //firewall
        {
            projectile = Instantiate(spellPrefabs[spellIndex], mousePos, Quaternion.identity);
            projectile.GetComponent<AtLocationSpell>().SpellDirection(spellDirection.direction); 
            Debug.Log("Mouse " + mousePos);
            Debug.Log("Direc " + spellDirection);

            yield return new WaitForSeconds(0.2f);
            //+1
            Debug.Log(("x " + spellDirection.direction.y) + mousePos.x);
            Debug.Log(("y " + spellDirection.direction.x) + mousePos.y);
            projectile = Instantiate(spellPrefabs[spellIndex], new Vector3((spellDirection.direction.y) + mousePos.x,
                                                                           (-spellDirection.direction.x) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity);
            projectile.GetComponent<AtLocationSpell>().SpellDirection(spellDirection.direction);
            //-1
            projectile = Instantiate(spellPrefabs[spellIndex], new Vector3((-spellDirection.direction.y) + mousePos.x,
                                                                           (spellDirection.direction.x) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity);
            projectile.GetComponent<AtLocationSpell>().SpellDirection(spellDirection.direction);
            yield return new WaitForSeconds(0.2f);
            //+2
            Debug.Log(("x " + spellDirection.direction.y) + mousePos.x);
            Debug.Log(("y " + spellDirection.direction.x) + mousePos.y);
            projectile = Instantiate(spellPrefabs[spellIndex], new Vector3((spellDirection.direction.y * 2) + mousePos.x,
                                                                           (-spellDirection.direction.x * 2) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity);
            projectile.GetComponent<AtLocationSpell>().SpellDirection(spellDirection.direction);
            //-2
            projectile = Instantiate(spellPrefabs[spellIndex], new Vector3((-spellDirection.direction.y * 2) + mousePos.x,
                                                                           (spellDirection.direction.x * 2) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity);
            projectile.GetComponent<AtLocationSpell>().SpellDirection(spellDirection.direction);

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
