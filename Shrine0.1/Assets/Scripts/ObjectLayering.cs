using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLayering : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > this.transform.position.y)
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Front";
        }
        else if(player.transform.position.y <= this.transform.position.y)
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
            this.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }
    }
}
