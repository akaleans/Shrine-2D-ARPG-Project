using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    [SerializeField]
    private GameObject[] elements;

    private GameObject thisElement;

    private int currentElement;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, elements.Length);
        thisElement = Instantiate(elements[rand], transform.position, Quaternion.identity);
        currentElement = rand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetElement()
    {
        return currentElement;
    }

    public void ElementTaken()
    {
        Destroy(thisElement);
        Destroy(this.GetComponent<CircleCollider2D>());
    }
}
