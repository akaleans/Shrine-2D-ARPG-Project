﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] roomTypes;
    public int[] level = new int[16];
    
    private int rand;
    private int randomRoom;
    private int position;
    private int direction;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    private bool stopGeneration;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 16; ++i)
        {
            level[i] = 0;
        }

        int randStartingPos = Random.Range(0, startingPositions.Length);
        if (randStartingPos == 0)
        {
            position = randStartingPos;
            transform.position = startingPositions[randStartingPos].position;
            Instantiate(roomTypes[1], transform.position, Quaternion.identity);
            level[position] = 1;
        }
        else if (randStartingPos == 1 || randStartingPos == 2)
        {
            position = randStartingPos;
            transform.position = startingPositions[randStartingPos].position;
            Instantiate(roomTypes[5], transform.position, Quaternion.identity);
            level[position] = 5;
        }
        else if (randStartingPos == 3)
        {
            position = randStartingPos;
            transform.position = startingPositions[randStartingPos].position;
            Instantiate(roomTypes[4], transform.position, Quaternion.identity);
            level[position] = 4;
        }

        direction = Random.Range(1, 6);
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // MOVE RIGHT
        {
            if(transform.position.x < maxX)
            {
                position += 1;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                if (level[position] == 0)
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
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                if (level[position] == 0)
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
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

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

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }
}
