using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private Vector2 newMapLocation;
    public GameObject newLevel;

    // Start is called before the first frame update
    void Start()
    {
        newMapLocation = new Vector2(LevelGeneration.newMapVectorX, LevelGeneration.newMapVectorY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Instantiate(newLevel, newMapLocation, Quaternion.identity);
        }
    }
}
