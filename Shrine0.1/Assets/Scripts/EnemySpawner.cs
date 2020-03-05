using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;

    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    private float randX;
    private float randY;

    private bool spawn;
    public int spawnMax;
    private int spawnCount;
    private int spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;

        minX = transform.position.x - 3.5f;
        maxX = transform.position.x + 3.5f;
        minY = transform.position.y - 3.5f;
        maxY = transform.position.y + 3.5f;

        spawn = true;
        spawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ++spawnTimer;
        if(spawn == true && spawnTimer == 10)
        {
            print("spawned at" + transform.position);
            randX = Random.Range(minX, maxX);
            randY = Random.Range(minY, maxY);

            Vector2 newPos = new Vector2(randX, randY);
            transform.position = newPos;

            Instantiate(enemyTypes[0], transform.position, Quaternion.identity);
            ++spawnCount;

            spawnTimer = 0;

            if (spawnCount == spawnMax)
            {
                Destroy(this);
            }
        }
    }
}
