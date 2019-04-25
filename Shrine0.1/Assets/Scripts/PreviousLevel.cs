using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousLevel : MonoBehaviour
{
    private PlayerController player;
    private CameraController camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            player = FindObjectOfType<PlayerController>();
            player.transform.position = transform.position;

            camera = FindObjectOfType<CameraController>();
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        }
    }*/
}
