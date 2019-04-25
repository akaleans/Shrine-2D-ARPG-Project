using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private Player player;
    private CameraController camera;
    public Vector3[] startingPositions;
    public GameObject[] roomTypes;
    public GameObject[] mapObjects;
    private int[] level = new int[16];
    private Vector2[] roomTransforms = new Vector2[16];
    private int roomCount;
    private int shrineCount;
    
    private int rand;
    private int randomRoom;
    private int position;
    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public static float newMapVectorX;
    public static float newMapVectorY;
    public static float newMapVectorZ;

    public float minX;
    public float maxX;
    public float minY;
    private bool stopGeneration;
    private bool crawled = false;
    private bool left = false;
    private bool right = false;
    private bool bottom = false;
    private bool top = false;

    // Start is called before the first frame update
    void Start()
    {
        newMapVectorX = transform.position.x + 60;
        newMapVectorY = transform.position.y;
        newMapVectorZ = transform.position.z;

        Instantiate(mapObjects[3], transform.position, Quaternion.identity);
        startingPositions = new Vector3[4];
        minX = transform.position.x - 15;
        maxX = transform.position.x + 15;
        minY = transform.position.y - 15;
        startingPositions[0] = new Vector3(transform.position.x - 15, transform.position.y + 15, transform.position.z);
        startingPositions[1] = new Vector3(transform.position.x - 5, transform.position.y + 15, transform.position.z);
        startingPositions[2] = new Vector3(transform.position.x + 5, transform.position.y + 15, transform.position.z);
        startingPositions[3] = new Vector3(transform.position.x + 15, transform.position.y + 15, transform.position.z);

        roomCount = -1;

        for(int i = 0; i < 16; ++i)
        {
            level[i] = -1;
            roomTransforms[i] = new Vector2(0f, 0f);
        }

        int randStartingPos = Random.Range(0, startingPositions.Length);
        if (randStartingPos == 0)
        {
            roomCount++;
            position = randStartingPos;
            transform.position = startingPositions[randStartingPos];
            roomTransforms[roomCount] = transform.position;
            Instantiate(roomTypes[1], transform.position, Quaternion.identity);
            level[position] = 1;
        }
        else if (randStartingPos == 1 || randStartingPos == 2)
        {
            roomCount++;
            position = randStartingPos;
            transform.position = startingPositions[randStartingPos];
            roomTransforms[roomCount] = transform.position;
            Instantiate(roomTypes[5], transform.position, Quaternion.identity);
            level[position] = 5;
        }
        else if (randStartingPos == 3)
        {
            roomCount++;
            position = randStartingPos;
            transform.position = startingPositions[randStartingPos];
            roomTransforms[roomCount] = transform.position;
            Instantiate(roomTypes[4], transform.position, Quaternion.identity);
            level[position] = 4;
        }
        player = FindObjectOfType<Player>();
        player.transform.position = transform.position;

        camera = FindObjectOfType<CameraController>();
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);

        direction = Random.Range(1, 6);
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // MOVE RIGHT
        {
            if(transform.position.x < maxX)
            {
                position += 1;
                roomCount++;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                roomTransforms[roomCount] = transform.position;

                if (level[position] == -1)
                {
                    if (position == 1 || position == 2) //1, 2
                    {
                        int[] roomArray = new int[] { 4, 5, 7 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 4) direction = 5;
                        else if (randomRoom == 5)
                        {
                            direction = Random.Range(1, 6);
                            if (direction == 3)
                            {
                                direction = 2;
                            }
                            else if (direction == 4)
                            {
                                direction = 5;
                            }
                        }
                        else if (randomRoom == 7) direction = 1;
                    }
                    else if (position == 3) // 3
                    {
                        Instantiate(roomTypes[4], transform.position, Quaternion.identity);
                        level[position] = 4;
                        direction = 5;
                    }
                    else if (position == 7 && (level[3] == 2 || level[3] == 4)) // 7
                    {
                        Instantiate(roomTypes[6], transform.position, Quaternion.identity);
                        level[position] = 6;
                        direction = 5;
                    }
                    else if (position == 7 && (level[3] != 2 && level[3] != 4)) // 7
                    {
                        Instantiate(roomTypes[4], transform.position, Quaternion.identity);
                        level[position] = 4;
                        direction = 5;
                    }
                    else if (position == 11 && (level[7] == 6 || level[7] == 4 || level[7] == 11)) // 11
                    {
                        Instantiate(roomTypes[6], transform.position, Quaternion.identity);
                        level[position] = 6;
                        direction = 5;
                    }
                    else if (position == 11 && (level[7] != 6 && level[7] != 4 && level[7] != 11)) // 11
                    {
                        Instantiate(roomTypes[4], transform.position, Quaternion.identity);
                        level[position] = 4;
                        direction = 5;
                    }
                    else if (position == 5 && (level[1] == 1 || level[1] == 4 || level[1] == 5 || level[1] == 2 || level[1] == 13)) // 5
                    {
                        int[] roomArray = new int[] { 9, 6, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 9)
                        {
                            direction = Random.Range(1, 6);
                            if (direction == 3)
                            {
                                direction = 2;
                            }
                            else if (direction == 4)
                            {
                                direction = 5;
                            }
                        }
                        else if (randomRoom == 6) direction = 5;
                        else if (randomRoom == 8) direction = 1;
                    }
                    else if (position == 5 && (level[1] != 1 && level[1] != 4 && level[1] != 5 && level[1] != 2 && level[1] != 13)) // 5
                    {
                        int[] roomArray = new int[] { 7, 4 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 7) direction = 1;
                        else if (randomRoom == 4) direction = 5;
                    }
                    else if (position == 6 && (level[2] == 1 || level[2] == 4 || level[2] == 5 || level[2] == 2 || level[2] == 13)) // 6
                    {
                        int[] roomArray = new int[] { 9, 6, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 9)
                        {
                            direction = Random.Range(1, 6);
                            if (direction == 3)
                            {
                                direction = 2;
                            }
                            else if (direction == 4)
                            {
                                direction = 5;
                            }
                        }
                        else if (randomRoom == 6) direction = 5;
                        else if (randomRoom == 8) direction = 1;
                    }
                    else if (position == 6 && (level[2] != 1 && level[2] != 4 && level[2] != 5 && level[2] != 2 && level[2] != 13)) // 6
                    {
                        int[] roomArray = new int[] { 7, 4 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 7) direction = 1;
                        else if (randomRoom == 4) direction = 5;
                    }
                    else if (position == 9 && (level[5] == 1 || level[5] == 4 || level[5] == 5 || level[5] == 2 || level[5] == 6 || level[5] == 9 || level[5] == 11 || level[5] == 13)) // 9
                    {
                        int[] roomArray = new int[] { 9, 6, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 9)
                        {
                            direction = Random.Range(1, 6);
                            if (direction == 3)
                            {
                                direction = 2;
                            }
                            else if (direction == 4)
                            {
                                direction = 5;
                            }
                        }
                        else if (randomRoom == 6) direction = 5;
                        else if (randomRoom == 8) direction = 1;
                    }
                    else if (position == 9 && (level[5] != 1 && level[5] != 4 && level[5] != 5 && level[5] != 2 && level[5] != 6 && level[5] != 9 && level[5] != 11 && level[5] != 13)) // 9
                    {
                        int[] roomArray = new int[] { 7, 4 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 7) direction = 1;
                        else if (randomRoom == 4) direction = 5;
                    }
                    else if (position == 10 && (level[6] == 1 || level[6] == 4 || level[6] == 5 || level[6] == 2 || level[6] == 6 || level[6] == 9 || level[6] == 11 || level[6] == 13)) // 10
                    {
                        int[] roomArray = new int[] { 9, 6, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 9)
                        {
                            direction = Random.Range(1, 6);
                            if (direction == 3)
                            {
                                direction = 2;
                            }
                            else if (direction == 4)
                            {
                                direction = 5;
                            }
                        }
                        else if (randomRoom == 6) direction = 5;
                        else if (randomRoom == 8) direction = 1;
                    }
                    else if (position == 10 && (level[6] != 1 && level[6] != 4 && level[6] != 5 && level[6] != 2 && level[6] != 6 && level[6] != 9 && level[6] != 11 && level[6] != 13)) // 10
                    {
                        int[] roomArray = new int[] { 7, 4 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 7) direction = 1;
                        else if (randomRoom == 4) direction = 5;
                    }
                    else if (position == 13 && (level[9] == 1 || level[9] == 4 || level[9] == 5 || level[9] == 2 || level[9] == 6 || level[9] == 9 || level[9] == 11 || level[9] == 13)) // 13
                    {
                        int[] roomArray = new int[] { 10, 8 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 10) stopGeneration = true;
                        else if (randomRoom == 8) direction = 1;
                    }
                    else if (position == 13 && (level[9] != 1 && level[9] != 4 && level[9] != 5 && level[9] != 2 && level[9] != 6 && level[9] != 9 && level[9] != 11 && level[9] != 13)) // 13
                    {
                        int[] roomArray = new int[] { 3, 7 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 3) stopGeneration = true;
                        else if (randomRoom == 7) direction = 1;
                    }
                    else if (position == 14 && (level[10] == 1 || level[10] == 4 || level[10] == 5 || level[10] == 2 || level[10] == 6 || level[10] == 9 || level[10] == 11 || level[10] == 13)) // 14
                    {
                        int[] roomArray = new int[] { 10, 8 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 10) stopGeneration = true;
                        else if (randomRoom == 8) direction = 1;
                    }
                    else if (position == 14 && (level[10] != 1 && level[10] != 4 && level[10] != 5 && level[10] != 2 && level[10] != 6 && level[10] != 9 && level[10] != 11 && level[10] != 13)) // 14
                    {
                        int[] roomArray = new int[] { 3, 7 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 3) stopGeneration = true;
                        else if (randomRoom == 7) direction = 1;
                    }
                    else if (position == 15 && (level[11] == 6 || level[11] == 4 || level[11] == 11)) // 15
                    {
                        Instantiate(roomTypes[10], transform.position, Quaternion.identity);
                        level[position] = 10;
                        stopGeneration = true;
                    }
                    else if (position == 15 && (level[11] != 6 && level[11] != 4 && level[11] != 11)) // 15
                    {
                        Instantiate(roomTypes[3], transform.position, Quaternion.identity);
                        level[position] = 3;
                        stopGeneration = true;
                    }
                }
            }
            else
            {
                direction = 5;
            }
            
        }
        else if(direction == 3 || direction == 4) // MOVE LEFT
        {
            if(transform.position.x > minX)
            {
                position -= 1;
                roomCount++;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                roomTransforms[roomCount] = transform.position;

                if (level[position] == -1)
                {
                    if (position == 0) // 0
                    {
                        Instantiate(roomTypes[1], transform.position, Quaternion.identity);
                        level[position] = 1;
                        direction = 5;
                    }
                    if (position == 1 || position == 2) //1, 2
                    {
                        int[] roomArray = new int[] { 7, 5, 1 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 7) direction = 3;
                        else if (randomRoom == 5) direction = Random.Range(3, 6);
                        else if (randomRoom == 1) direction = 5;
                    }
                    else if (position == 4 && (level[0] == 2 || level[0] == 1)) // 4
                    {
                        Instantiate(roomTypes[13], transform.position, Quaternion.identity);
                        level[position] = 13;
                        direction = 5;
                    }
                    else if (position == 4 && (level[0] != 2 && level[0] != 1)) // 4
                    {
                        Instantiate(roomTypes[1], transform.position, Quaternion.identity);
                        level[position] = 4;
                        direction = 5;
                    }
                    else if (position == 8 && (level[4] == 13 || level[4] == 11 || level[4] == 1)) // 8
                    {
                        Instantiate(roomTypes[13], transform.position, Quaternion.identity);
                        level[position] = 13;
                        direction = 5;
                    }
                    else if (position == 8 && (level[4] != 13 && level[4] != 11 && level[4] != 1)) // 8
                    {
                        Instantiate(roomTypes[1], transform.position, Quaternion.identity);
                        level[position] = 1;
                        direction = 5;
                    }
                    else if (position == 5 && (level[1] == 1 || level[1] == 4 || level[1] == 5 || level[1] == 2 || level[1] == 13)) // 5
                    {
                        int[] roomArray = new int[] { 13, 9, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 13) direction = 5;
                        else if (randomRoom == 9) direction = Random.Range(3, 6);
                        else if (randomRoom == 8) direction = 3;
                    }
                    else if (position == 5 && (level[1] != 1 && level[1] != 4 && level[1] != 5 && level[1] != 2 && level[1] != 13)) // 5
                    {
                        int[] roomArray = new int[] { 5, 1 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 5) direction = Random.Range(3, 6);
                        else if (randomRoom == 1) direction = 5;
                    }
                    else if (position == 6 && (level[2] == 1 || level[2] == 4 || level[2] == 5 || level[2] == 2 || level[2] == 13)) // 6
                    {
                        int[] roomArray = new int[] { 13, 9, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 13) direction = 5;
                        else if (randomRoom == 9) direction = Random.Range(3, 6);
                        else if (randomRoom == 8) direction = 3;
                    }
                    else if (position == 6 && (level[2] != 1 && level[2] != 4 && level[2] != 5 && level[2] != 2 && level[2] != 13)) // 6
                    {
                        int[] roomArray = new int[] { 5, 1 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 5) direction = Random.Range(3, 6);
                        else if (randomRoom == 1) direction = 5;
                    }
                    else if (position == 9 && (level[5] == 1 || level[5] == 4 || level[5] == 5 || level[5] == 2 || level[5] == 6 || level[5] == 9 || level[5] == 11 || level[5] == 13)) // 9
                    {
                        int[] roomArray = new int[] { 13, 9, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 13) direction = 5;
                        else if (randomRoom == 9) direction = Random.Range(3, 6);
                        else if (randomRoom == 8) direction = 3;
                    }
                    else if (position == 9 && (level[5] != 1 && level[5] != 4 && level[5] != 5 && level[5] != 2 && level[5] != 6 && level[5] != 9 && level[5] != 11 && level[5] != 13)) // 9
                    {
                        int[] roomArray = new int[] { 5, 1 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 5) direction = Random.Range(3, 6);
                        else if (randomRoom == 1) direction = 5;
                    }
                    else if (position == 10 && (level[6] == 1 || level[6] == 4 || level[6] == 5 || level[6] == 2 || level[6] == 6 || level[6] == 9 || level[6] == 11 || level[6] == 13)) // 10
                    {
                        int[] roomArray = new int[] { 13, 9, 8 };
                        rand = Random.Range(0, 3);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 13) direction = 5;
                        else if (randomRoom == 9) direction = Random.Range(3, 6);
                        else if (randomRoom == 8) direction = 3;
                    }
                    else if (position == 10 && (level[6] != 1 && level[6] != 4 && level[6] != 5 && level[6] != 2 && level[6] != 6 && level[6] != 9 && level[6] != 11 && level[6] != 13)) // 10
                    {
                        int[] roomArray = new int[] { 5, 1 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 5) direction = Random.Range(3, 6);
                        else if (randomRoom == 1) direction = 5;
                    }
                    else if (position == 13 && (level[9] == 1 || level[9] == 4 || level[9] == 5 || level[9] == 2 || level[9] == 6 || level[9] == 9 || level[9] == 11 || level[9] == 13)) // 13
                    {
                        int[] roomArray = new int[] { 8, 12 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 12) stopGeneration = true;
                        else if (randomRoom == 8) direction = 3;
                    }
                    else if (position == 13 && (level[9] != 1 && level[9] != 4 && level[9] != 5 && level[9] != 2 && level[9] != 6 && level[9] != 9 && level[9] != 11 && level[9] != 13)) // 13
                    {
                        int[] roomArray = new int[] { 7, 0 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 0) stopGeneration = true;
                        else if (randomRoom == 7) direction = 3;
                    }
                    else if (position == 14 && (level[10] == 1 || level[10] == 4 || level[10] == 5 || level[10] == 2 || level[10] == 6 || level[10] == 9 || level[10] == 11 || level[10] == 13)) // 14
                    {
                        int[] roomArray = new int[] { 8, 12 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 12) stopGeneration = true;
                        else if (randomRoom == 8) direction = 3;
                    }
                    else if (position == 14 && (level[10] != 1 && level[10] != 4 && level[10] != 5 && level[10] != 2 && level[10] != 6 && level[10] != 9 && level[10] != 11 && level[10] != 13)) // 14
                    {
                        int[] roomArray = new int[] { 7, 0 };
                        rand = Random.Range(0, 2);
                        randomRoom = roomArray[rand];
                        Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                        level[position] = randomRoom;
                        if (randomRoom == 0) stopGeneration = true;
                        else if (randomRoom == 7) direction = 3;
                    }
                    else if (position == 12 && (level[8] == 13 || level[8] == 11 || level[8] == 1)) // 12
                    {
                        Instantiate(roomTypes[12], transform.position, Quaternion.identity);
                        level[position] = 12;
                        stopGeneration = true;
                    }
                    else if (position == 12 && (level[8] != 13 && level[8] != 11 && level[8] != 1)) // 12
                    {
                        Instantiate(roomTypes[0], transform.position, Quaternion.identity);
                        level[position] = 0;
                        stopGeneration = true;
                    }
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if(direction == 5) // MOVE DOWN
        {
            if(transform.position.y > minY)
            {
                position += 4;
                roomCount++;
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;
                roomTransforms[roomCount] = transform.position;

                if (position == 4) // 4
                {
                    int[] roomArray = new int[] { 12, 11, 13 };
                    rand = Random.Range(0, 3);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 12) direction = 1;
                    else if (randomRoom == 11) direction = 5;
                    else if (randomRoom == 13)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 3) direction = 1;
                        else if(direction == 4) direction = 5;
                    }
                }
                if (position == 8) // 8
                {
                    int[] roomArray = new int[] { 12, 11, 13 };
                    rand = Random.Range(0, 3);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 12) direction = 1;
                    else if (randomRoom == 11) direction = 5;
                    else if (randomRoom == 13)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 3) direction = 1;
                        else if (direction == 4) direction = 5;
                    }
                }
                else if (position == 5) // 5
                {
                    int[] roomArray = new int[] { 6, 8, 9, 10, 11, 12, 13 };
                    rand = Random.Range(0, 7);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if(randomRoom == 6)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 1) direction = 3;
                        else if (direction == 2) direction = 5;
                    }
                    else if (randomRoom == 8)
                    {
                        direction = Random.Range(1, 5);
                    }
                    else if (randomRoom == 9)
                    {
                        direction = Random.Range(1, 6);
                    }
                    else if (randomRoom == 10)
                    {
                        direction = 3;
                    }
                    else if (randomRoom == 11)
                    {
                        direction = 5;
                    }
                    else if (randomRoom == 12)
                    {
                        direction = 1;
                    }
                    else if (randomRoom == 13)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 3) direction = 1;
                        else if (direction == 4) direction = 5;
                    }
                }
                else if (position == 6) // 6
                {
                    int[] roomArray = new int[] { 6, 8, 9, 10, 11, 12, 13 };
                    rand = Random.Range(0, 7);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 6)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 1) direction = 3;
                        else if (direction == 2) direction = 5;
                    }
                    else if (randomRoom == 8)
                    {
                        direction = Random.Range(1, 5);
                    }
                    else if (randomRoom == 9)
                    {
                        direction = Random.Range(1, 6);
                    }
                    else if (randomRoom == 10)
                    {
                        direction = 3;
                    }
                    else if (randomRoom == 11)
                    {
                        direction = 5;
                    }
                    else if (randomRoom == 12)
                    {
                        direction = 1;
                    }
                    else if (randomRoom == 13)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 3) direction = 1;
                        else if (direction == 4) direction = 5;
                    }
                }
                else if (position == 9) // 9
                {
                    int[] roomArray = new int[] { 6, 8, 9, 10, 11, 12, 13 };
                    rand = Random.Range(0, 7);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 6)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 1) direction = 3;
                        else if (direction == 2) direction = 5;
                    }
                    else if (randomRoom == 8)
                    {
                        direction = Random.Range(1, 5);
                    }
                    else if (randomRoom == 9)
                    {
                        direction = Random.Range(1, 6);
                    }
                    else if (randomRoom == 10)
                    {
                        direction = 3;
                    }
                    else if (randomRoom == 11)
                    {
                        direction = 5;
                    }
                    else if (randomRoom == 12)
                    {
                        direction = 1;
                    }
                    else if (randomRoom == 13)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 3) direction = 1;
                        else if (direction == 4) direction = 5;
                    }
                }
                else if (position == 10) // 10
                {
                    int[] roomArray = new int[] { 6, 8, 9, 10, 11, 12, 13 };
                    rand = Random.Range(0, 7);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 6)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 1) direction = 3;
                        else if (direction == 2) direction = 5;
                    }
                    else if (randomRoom == 8)
                    {
                        direction = Random.Range(1, 5);
                    }
                    else if (randomRoom == 9)
                    {
                        direction = Random.Range(1, 6);
                    }
                    else if (randomRoom == 10)
                    {
                        direction = 3;
                    }
                    else if (randomRoom == 11)
                    {
                        direction = 5;
                    }
                    else if (randomRoom == 12)
                    {
                        direction = 1;
                    }
                    else if (randomRoom == 13)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 3) direction = 1;
                        else if (direction == 4) direction = 5;
                    }
                }
                else if (position == 7) // 7
                {
                    int[] roomArray = new int[] { 11, 6, 10 };
                    rand = Random.Range(0, 3);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 11) direction = 5;
                    else if (randomRoom == 6)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 1) direction = 3;
                        else if (direction == 2) direction = 5;
                    }
                    else if (randomRoom == 10) direction = 3;
                }
                else if (position == 11) // 11
                {
                    int[] roomArray = new int[] { 11, 6, 10 };
                    rand = Random.Range(0, 3);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 11) direction = 5;
                    else if (randomRoom == 6)
                    {
                        direction = Random.Range(1, 6);
                        if (direction == 1) direction = 3;
                        else if (direction == 2) direction = 5;
                    }
                    else if (randomRoom == 10) direction = 3;
                }
                else if (position == 12) // 12
                {
                    int[] roomArray = new int[] { 12, 14 };
                    rand = Random.Range(0, 2);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 14) stopGeneration = true;
                    else if (randomRoom == 12) direction = 1;
                }
                else if (position == 13) // 13
                {
                    int[] roomArray = new int[] { 14, 12, 8, 10 };
                    rand = Random.Range(0, 4);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 14) stopGeneration = true;
                    else if (randomRoom == 13) direction = 1;
                    else if (randomRoom == 8)
                    {
                        direction = Random.Range(1, 5);
                    }
                    else if (randomRoom == 10) direction = 3;
                }
                else if (position == 14) // 14
                {
                    int[] roomArray = new int[] { 14, 12, 8, 10 };
                    rand = Random.Range(0, 4);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 14) stopGeneration = true;
                    else if (randomRoom == 13) direction = 1;
                    else if (randomRoom == 8)
                    {
                        direction = Random.Range(1, 5);
                    }
                    else if (randomRoom == 10) direction = 3;
                }
                else if (position == 15) // 15
                {
                    int[] roomArray = new int[] { 14, 10 };
                    rand = Random.Range(0, 2);
                    randomRoom = roomArray[rand];
                    Instantiate(roomTypes[randomRoom], transform.position, Quaternion.identity);
                    level[position] = randomRoom;
                    if (randomRoom == 14) stopGeneration = true;
                    else if (randomRoom == 10) direction = 3;
                }
            }
            else
            {
                //STOP LEVEL GEN
                stopGeneration = true;
            }
        }
    }

    void Crawler()
    {
        position = 0;
        transform.position = startingPositions[0];

        for (int i = 0; i < 16; ++i)
        {
            if (level[i] == -1)
            {
                if (i == 0)
                {
                    //RIGHT ROOM?
                    if (level[i + 1] == 3 || level[i + 1] == 4 || level[i + 1] == 5 || level[i + 1] == 6 ||
                        level[i + 1] == 7 || level[i + 1] == 8 || level[i + 1] == 9 || level[i + 1] == 10)
                        right = true;
                    //BOTTOM ROOM?
                    if (level[i + 4] == 6 || level[i + 4] == 8 || level[i + 4] == 9 || level[i + 4] == 10 ||
                        level[i + 4] == 11 || level[i + 4] == 12 || level[i + 4] == 13 || level[i + 4] == 14)
                        bottom = true;
                }
                else if (i == 1 || i == 2)
                {
                    //LEFT ROOM?
                    if (level[i - 1] == 0 || level[i - 1] == 1 || level[i - 1] == 5 || level[i - 1] == 7 ||
                        level[i - 1] == 8 || level[i - 1] == 9 || level[i - 1] == 12 || level[i - 1] == 13)
                        left = true;
                    //RIGHT ROOM?
                    if (level[i + 1] == 3 || level[i + 1] == 4 || level[i + 1] == 5 || level[i + 1] == 6 ||
                        level[i + 1] == 7 || level[i + 1] == 8 || level[i + 1] == 9 || level[i + 1] == 10)
                        right = true;
                    //BOTTOM ROOM?
                    if (level[i + 4] == 6 || level[i + 4] == 8 || level[i + 4] == 9 || level[i + 4] == 10 ||
                        level[i + 4] == 11 || level[i + 4] == 12 || level[i + 4] == 13 || level[i + 4] == 14)
                        bottom = true;
                }
                else if (i == 3)
                {
                    //LEFT ROOM?
                    if (level[i - 1] == 0 || level[i - 1] == 1 || level[i - 1] == 5 || level[i - 1] == 7 ||
                        level[i - 1] == 8 || level[i - 1] == 9 || level[i - 1] == 12 || level[i - 1] == 13)
                        left = true;
                    //BOTTOM ROOM?
                    if (level[i + 4] == 6 || level[i + 4] == 8 || level[i + 4] == 9 || level[i + 4] == 10 ||
                        level[i + 4] == 11 || level[i + 4] == 12 || level[i + 4] == 13 || level[i + 4] == 14)
                        bottom = true;
                }
                else if (i == 4 || i == 8)
                {
                    //RIGHT ROOM?
                    if (level[i + 1] == 3 || level[i + 1] == 4 || level[i + 1] == 5 || level[i + 1] == 6 ||
                        level[i + 1] == 7 || level[i + 1] == 8 || level[i + 1] == 9 || level[i + 1] == 10)
                        right = true;
                    //TOP ROOM?
                    if (level[i - 4] == 1 || level[i - 4] == 2 || level[i - 4] == 4 || level[i - 4] == 5 ||
                        level[i - 4] == 6 || level[i - 4] == 9 || level[i - 4] == 11 || level[i - 4] == 13)
                        top = true;
                    //BOTTOM ROOM?
                    if (level[i + 4] == 6 || level[i + 4] == 8 || level[i + 4] == 9 || level[i + 4] == 10 ||
                        level[i + 4] == 11 || level[i + 4] == 12 || level[i + 4] == 13 || level[i + 4] == 14)
                        bottom = true;
                }
                else if (i == 5 || i == 6 || i == 9 || i == 10)
                {
                    //LEFT ROOM?
                    if (level[i - 1] == 0 || level[i - 1] == 1 || level[i - 1] == 5 || level[i - 1] == 7 ||
                        level[i - 1] == 8 || level[i - 1] == 9 || level[i - 1] == 12 || level[i - 1] == 13)
                        left = true;
                    //RIGHT ROOM?
                    if (level[i + 1] == 3 || level[i + 1] == 4 || level[i + 1] == 5 || level[i + 1] == 6 ||
                        level[i + 1] == 7 || level[i + 1] == 8 || level[i + 1] == 9 || level[i + 1] == 10)
                        right = true;
                    //TOP ROOM?
                    if (level[i - 4] == 1 || level[i - 4] == 2 || level[i - 4] == 4 || level[i - 4] == 5 ||
                        level[i - 4] == 6 || level[i - 4] == 9 || level[i - 4] == 11 || level[i - 4] == 13)
                        top = true;
                    //BOTTOM ROOM?
                    if (level[i + 4] == 6 || level[i + 4] == 8 || level[i + 4] == 9 || level[i + 4] == 10 ||
                        level[i + 4] == 11 || level[i + 4] == 12 || level[i + 4] == 13 || level[i + 4] == 14)
                        bottom = true;
                }
                else if (i == 7 || i == 11)
                {
                    //LEFT ROOM?
                    if (level[i - 1] == 0 || level[i - 1] == 1 || level[i - 1] == 5 || level[i - 1] == 7 ||
                        level[i - 1] == 8 || level[i - 1] == 9 || level[i - 1] == 12 || level[i - 1] == 13)
                        left = true;
                    //TOP ROOM?
                    if (level[i - 4] == 1 || level[i - 4] == 2 || level[i - 4] == 4 || level[i - 4] == 5 ||
                        level[i - 4] == 6 || level[i - 4] == 9 || level[i - 4] == 11 || level[i - 4] == 13)
                        top = true;
                    //BOTTOM ROOM?
                    if (level[i + 4] == 6 || level[i + 4] == 8 || level[i + 4] == 9 || level[i + 4] == 10 ||
                        level[i + 4] == 11 || level[i + 4] == 12 || level[i + 4] == 13 || level[i + 4] == 14)
                        bottom = true;
                }
                else if (i == 12)
                {
                    //RIGHT ROOM?
                    if (level[i + 1] == 3 || level[i + 1] == 4 || level[i + 1] == 5 || level[i + 1] == 6 ||
                        level[i + 1] == 7 || level[i + 1] == 8 || level[i + 1] == 9 || level[i + 1] == 10)
                        right = true;
                    //TOP ROOM?
                    if (level[i - 4] == 1 || level[i - 4] == 2 || level[i - 4] == 4 || level[i - 4] == 5 ||
                        level[i - 4] == 6 || level[i - 4] == 9 || level[i - 4] == 11 || level[i - 4] == 13)
                        top = true;
                }
                else if (i == 13 || i == 14)
                {
                    //LEFT ROOM?
                    if (level[i - 1] == 0 || level[i - 1] == 1 || level[i - 1] == 5 || level[i - 1] == 7 ||
                        level[i - 1] == 8 || level[i - 1] == 9 || level[i - 1] == 12 || level[i - 1] == 13)
                        left = true;
                    //RIGHT ROOM?
                    if (level[i + 1] == 3 || level[i + 1] == 4 || level[i + 1] == 5 || level[i + 1] == 6 ||
                        level[i + 1] == 7 || level[i + 1] == 8 || level[i + 1] == 9 || level[i + 1] == 10)
                        right = true;
                    //TOP ROOM?
                    if (level[i - 4] == 1 || level[i - 4] == 2 || level[i - 4] == 4 || level[i - 4] == 5 ||
                        level[i - 4] == 6 || level[i - 4] == 9 || level[i - 4] == 11 || level[i - 4] == 13)
                        top = true;
                }
                else if (i == 15)
                {
                    //LEFT ROOM?
                    if (level[i - 1] == 0 || level[i - 1] == 1 || level[i - 1] == 5 || level[i - 1] == 7 ||
                        level[i - 1] == 8 || level[i - 1] == 9 || level[i - 1] == 12 || level[i - 1] == 13)
                        left = true;
                    //TOP ROOM?
                    if (level[i - 4] == 1 || level[i - 4] == 2 || level[i - 4] == 4 || level[i - 4] == 5 ||
                        level[i - 4] == 6 || level[i - 4] == 9 || level[i - 4] == 11 || level[i - 4] == 13)
                        top = true;
                }
                if (left == true && right != true && top != true && bottom != true) // L
                {
                    Instantiate(roomTypes[3], transform.position, Quaternion.identity);
                    level[i] = 3;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left != true && right == true && top != true && bottom != true) // R
                {
                    Instantiate(roomTypes[0], transform.position, Quaternion.identity);
                    level[i] = 0;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left != true && right != true && top == true && bottom != true) // T
                {
                    Instantiate(roomTypes[14], transform.position, Quaternion.identity);
                    level[i] = 14;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left != true && right != true && top != true && bottom == true) // B
                {
                    Instantiate(roomTypes[2], transform.position, Quaternion.identity);
                    level[i] = 2;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left == true && right == true && top != true && bottom != true) // LR
                {
                    Instantiate(roomTypes[7], transform.position, Quaternion.identity);
                    level[i] = 7;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left == true && right != true && top == true && bottom != true) // LT
                {
                    Instantiate(roomTypes[10], transform.position, Quaternion.identity);
                    level[i] = 10;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left == true && right != true && top != true && bottom == true) // LB
                {
                    Instantiate(roomTypes[4], transform.position, Quaternion.identity);
                    level[i] = 4;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left != true && right == true && top == true && bottom != true) // RT
                {
                    Instantiate(roomTypes[12], transform.position, Quaternion.identity);
                    level[i] = 12;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left != true && right == true && top != true && bottom == true) // RB
                {
                    Instantiate(roomTypes[1], transform.position, Quaternion.identity);
                    level[i] = 1;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left != true && right != true && top == true && bottom == true) // TB
                {
                    Instantiate(roomTypes[11], transform.position, Quaternion.identity);
                    level[i] = 11;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left == true && right == true && top == true && bottom != true) // LRT
                {
                    Instantiate(roomTypes[8], transform.position, Quaternion.identity);
                    level[i] = 3;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left == true && right == true && top != true && bottom == true) // LRB
                {
                    Instantiate(roomTypes[5], transform.position, Quaternion.identity);
                    level[i] = 5;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left == true && right != true && top == true && bottom == true) // LTB
                {
                    Instantiate(roomTypes[6], transform.position, Quaternion.identity);
                    level[i] = 6;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left != true && right == true && top == true && bottom == true) // RTB
                {
                    Instantiate(roomTypes[13], transform.position, Quaternion.identity);
                    level[i] = 13;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
                else if (left == true && right == true && top == true && bottom == true) // LRTB
                {
                    Instantiate(roomTypes[9], transform.position, Quaternion.identity);
                    level[i] = 9;
                    roomCount++;
                    roomTransforms[roomCount] = transform.position;
                }
            }
            left = false;
            right = false;
            top = false;
            bottom = false;

            if (i != 3 && i != 7 && i != 11 && i != 15)
            { 
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
            }
            else
            {
                Vector2 newPos = new Vector2(transform.position.x - (moveAmount * 3), transform.position.y - moveAmount);
                transform.position = newPos;
            }
        }
        crawled = true;
    }

    void ShrineSeeder()
    {
        if(roomCount == 3)
        {
            Instantiate(mapObjects[1], roomTransforms[1], Quaternion.identity); // shrine
            Instantiate(mapObjects[1], roomTransforms[2], Quaternion.identity); // shrine
        }
        if(roomCount >= 4 && roomCount < 6){
            //Instantiate(mapObjects[0], roomTransforms[1], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[1], Quaternion.identity); // shrine
            Instantiate(mapObjects[1], roomTransforms[2], Quaternion.identity); // shrine
            Instantiate(mapObjects[1], roomTransforms[3], Quaternion.identity); // shrine
        }
        else if(roomCount >= 6 && roomCount < 8)
        {
            //Instantiate(mapObjects[0], roomTransforms[1], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[1], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[3], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[3], Quaternion.identity); // shrine
            Instantiate(mapObjects[1], roomTransforms[5], Quaternion.identity); // shrine
        }
        else if (roomCount >= 8 && roomCount < 10)
        {
            //Instantiate(mapObjects[0], roomTransforms[1], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[1], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[3], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[3], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[5], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[5], Quaternion.identity); // shrine
            Instantiate(mapObjects[1], roomTransforms[7], Quaternion.identity); // shrine
        }
        else if (roomCount >= 10 && roomCount < 12)
        {
            //Instantiate(mapObjects[0], roomTransforms[1], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[1], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[3], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[3], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[5], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[5], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[7], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[7], Quaternion.identity); // shrine
            Instantiate(mapObjects[1], roomTransforms[9], Quaternion.identity); // shrine
        }
        else if (roomCount >= 12)
        {
            //Instantiate(mapObjects[0], roomTransforms[1], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[1], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[3], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[3], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[5], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[5], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[7], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[7], Quaternion.identity); // shrine
            //Instantiate(mapObjects[0], roomTransforms[9], Quaternion.identity); // lever
            Instantiate(mapObjects[1], roomTransforms[9], Quaternion.identity); // shrine
            Instantiate(mapObjects[1], roomTransforms[11], Quaternion.identity); // shrine
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else if (stopGeneration == false)
        {
            timeBtwRoom -= Time.deltaTime;
        }
        else if (stopGeneration == true && crawled == false)
        {
            Instantiate(mapObjects[2], roomTransforms[roomCount], Quaternion.identity); // red portal
            Instantiate(mapObjects[4], roomTransforms[0], Quaternion.identity); // blue portal
            ShrineSeeder();
            Crawler();
        }
    }
}
