using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    public Player player;
    public CameraController camera;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.transform.position = transform.position;

        camera = FindObjectOfType<CameraController>();
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
