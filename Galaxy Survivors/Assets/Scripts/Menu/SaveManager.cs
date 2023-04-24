#define INSERT_DATA
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public SaveData data;

    // Start is called before the first frame update
    void Awake()
    {
#if INSERT_DATA
        PlayerInformation info1 = new PlayerInformation();
        PlayerInformation info2 = new PlayerInformation();
        PlayerInformation info3 = new PlayerInformation();
        PlayerInformation info4 = new PlayerInformation();
        PlayerInformation info5 = new PlayerInformation();
        PlayerInformation info6 = new PlayerInformation();
        info1.ID = 0;
        info2.ID = 1;
        info3.ID = 2;
        info4.ID = 3;
        info5.ID = 4;
        info6.ID = 5;
        info1.unlocked = true;
        info2.unlocked = false;
        info3.unlocked = false;
        info4.unlocked = false;
        info5.unlocked = false;
        info6.unlocked = false;

        SaveData tempData = new SaveData();
        tempData.playerInformation = new PlayerInformation[] { info1, info2, info3, info4, info5, info6 };
        data = tempData;
#endif

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        saveIntoJson();
    }

    public void saveIntoJson()
    {
        string temp = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", temp);
    }

    public void loadFromJson()
    {
        if (fileExists())
        {
            string temp = File.ReadAllText(Application.persistentDataPath + "/saveData.json");
            data = JsonUtility.FromJson<SaveData>(temp);
        }
        else
            saveIntoJson();
    }

    public bool fileExists()
    {
        return File.Exists(Application.persistentDataPath + "/saveData.json");
    }
}

[Serializable]
public class SaveData
{
    public int gold;
    public int currentPlayer;
    public int currentPerk1;
    public int currentPerk2;
    public int currentPerk3;
    public PlayerInformation[] playerInformation;
    public Perks[] perks;
}

[Serializable]
public class Perks
{
    public int ID;
    public bool unlocked;
}

[Serializable]
public class PlayerInformation
{
    public int ID;
    public bool unlocked;
}