using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Status mana;

    [SerializeField]
    private float maxMana;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private GameObject[] spellPrefabs;
    private int spellIndex;
    private SpellBook spellBook;

    [SerializeField]
    private Transform[] exitPoints;
    private int exitIndex;

    private bool interactableInRange;
    private string interactable;
    private int currentElementInRange;
    private int[] currentElements;
    private Shrine currentShrine;

    private GameObject projectile;

    private Vector3 mousePos;
    private Ray spellDirection;

    private static bool playerExists;

    // Start is called before the first frame update
    protected override void Start()
    {
        mana.Initialize(maxMana, maxMana);

        interactableInRange = false;
        currentElements = new int[2];
        currentElements[0] = -1;
        currentElements[1] = -1;

        spellBook = GetComponent<SpellBook>();
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
        Direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            Direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Direction += Vector2.right;
        }
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(LeftMouseAttack()); // simultaneously
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetSpellIndex1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetSpellIndex2();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (interactableInRange)
            {
                if (interactable == "Shrine")
                {
                    GetElement();
                }
            }
        }
    }

    private void GetSpellIndex1() //FIRST SPELL
    {
        if (currentElements[0] == -1 && currentElements[1] == -1) // NONE
        {
            spellIndex = 0;
        }
        else if (currentElements[0] == 0 || currentElements[1] == 0) //FIRE
        {
            if(currentElements[1] == -1) //FIRE ONLY
            {
                spellIndex = 2; // fireball
            }
        }
        else if (currentElements[0] == 1 || currentElements[1] == 1) //WIND
        {
            if (currentElements[0] == -1 || currentElements[1] == -1) //WIND ONLY
            {
                spellIndex = 4; //twister
            }
        }
    }

    private void GetSpellIndex2() //SECOND SPELL
    {
        if (currentElements[0] == -1 && currentElements[1] == -1) // NONE
        {
            spellIndex = 0;
        }
        else if (currentElements[0] == 0 || currentElements[1] == 0) //FIRE
        {
            if (currentElements[0] == -1 || currentElements[1] == -1) //FIRE ONLY
            {
                spellIndex = 3; //firewall
            }
        }
        else if (currentElements[0] == 1 || currentElements[1] == 1) //WIND
        {
            if (currentElements[0] == -1 || currentElements[1] == -1) //WIND ONLY
            {
                spellIndex = 3; //windstorm
            }
        }
    }

    private void GetElement()
    {
        if (currentElements[0] == -1)
        {
            currentElements[0] = currentElementInRange;
        }
        else if (currentElements[0] != -1 && currentElements[1] == -1)
        {
            currentElements[1] = currentElementInRange;
        }
        else if (currentElements[0] != -1 && currentElements[1] != -1)
        {
            currentElements[0] = currentElements[1];
            currentElements[1] = currentElementInRange;
        }
        currentShrine.ElementTaken();
    }

    private void GetDirections()
    {
        //get direction for spell to travel
        spellDirection = Camera.main.ScreenPointToRay(Input.mousePosition);
        spellDirection.direction = new Vector3(spellDirection.direction.x, spellDirection.direction.y, 0f);

        //get location for atlocationspell
        mousePos = Input.mousePosition;
        mousePos.z = 15;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //set direction for animator
        MyAnimator.SetFloat("x", spellDirection.direction.x);
        MyAnimator.SetFloat("y", spellDirection.direction.y);
    }

    private IEnumerator LeftMouseAttack()
    {
        if (!IsAttacking)
        {
            IsAttacking = true;
            MyAnimator.SetBool("attack", IsAttacking);

            //get spell direction and location
            GetDirections();

            //get exitpoint
            GetExitPoint(spellDirection);

            yield return new WaitForSeconds(attackSpeed); // cast time

            StartCoroutine(CastSpell());

            StopAttack();
        }
    }

    private IEnumerator CastSpell()
    {
        Spell newSpell = spellBook.CastSpell(spellIndex);

        if (spellIndex == 0 || spellIndex == 2) //projectile
        {
            yield return new WaitForSeconds(0f);
            ProjectileSpell s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<ProjectileSpell>();
            s.Initialize(spellDirection.direction, newSpell.MyDamage, transform);
           
        }
        else if (spellIndex == 3) //firewall
        {
            yield return new WaitForSeconds(0f);
            AtLocationSpell s = Instantiate(newSpell.MySpellPrefab, mousePos, Quaternion.identity).GetComponent<AtLocationSpell>();
            s.Initialize(spellDirection.direction, newSpell.MyDamage, transform); 

            yield return new WaitForSeconds(0.2f);
            s = Instantiate(newSpell.MySpellPrefab, new Vector3((spellDirection.direction.y) + mousePos.x,
                                                                           (-spellDirection.direction.x) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity).GetComponent<AtLocationSpell>();
            s.Initialize(spellDirection.direction, newSpell.MyDamage, transform);
            s = Instantiate(newSpell.MySpellPrefab, new Vector3((-spellDirection.direction.y) + mousePos.x,
                                                                           (spellDirection.direction.x) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity).GetComponent<AtLocationSpell>();
            s.Initialize(spellDirection.direction, newSpell.MyDamage, transform);

            yield return new WaitForSeconds(0.2f);
            s = Instantiate(newSpell.MySpellPrefab, new Vector3((spellDirection.direction.y * 2) + mousePos.x,
                                                                           (-spellDirection.direction.x * 2) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity).GetComponent<AtLocationSpell>();
            s.Initialize(spellDirection.direction, newSpell.MyDamage, transform);
            s = Instantiate(newSpell.MySpellPrefab, new Vector3((-spellDirection.direction.y * 2) + mousePos.x,
                                                                           (spellDirection.direction.x * 2) + mousePos.y,
                                                                            mousePos.z), Quaternion.identity).GetComponent<AtLocationSpell>();
            s.Initialize(spellDirection.direction, newSpell.MyDamage, transform);
        }
        else if(spellIndex == 4) //twisters
        {
            yield return new WaitForSeconds(0f);
            AtLocationSpellTwister s = Instantiate(newSpell.MySpellPrefab, mousePos, Quaternion.identity).GetComponent<AtLocationSpellTwister>();
            s.Initialize(spellDirection.direction, newSpell.MyDamage, transform);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shrine")
        {
            currentShrine = collision.transform.GetComponent<Shrine>();
            interactableInRange = true;
            interactable = "Shrine";
            currentElementInRange = collision.GetComponentInParent<Shrine>().GetElement();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shrine")
        {
            currentShrine = null;
            interactableInRange = false;
            interactable = null;
            currentElementInRange = -1;
        }
    }
}
