using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    private Transform target;

    public Transform Target { get => target; set => target = value; }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FollowTargeT()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed);
        }
    }
}
