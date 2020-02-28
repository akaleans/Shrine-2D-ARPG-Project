using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    public GameObject level;

    // Start is called before the first frame update
    void Awake()
    {
        if(control = null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/levelData.dat", FileMode.Open);

        LevelData data = new LevelData();
    }
}

[Serializable]
class LevelData
{
    public GameObject level;
}
