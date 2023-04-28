using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // public variables
    public static SaveManager instance;

    public SaveData data;

    // Start is called before the first frame update
    public void initiliseData()
    {
        PlayerInformation info1 = new();
        PlayerInformation info2 = new();
        PlayerInformation info3 = new();
        PlayerInformation info4 = new();
        PlayerInformation info5 = new();
        PlayerInformation info6 = new();
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

        Perks perk1 = new();
        Perks perk2 = new();
        Perks perk3 = new();
        Perks perk4 = new();
        Perks perk5 = new();
        Perks perk6 = new();
        Perks perk7 = new();
        Perks perk8 = new();
        perk1.ID = 0;
        perk2.ID = 1;
        perk3.ID = 2;
        perk4.ID = 3;
        perk5.ID = 4;
        perk6.ID = 5;
        perk7.ID = 6;
        perk8.ID = 7;
        perk1.unlocked = false;
        perk2.unlocked = false;
        perk3.unlocked = false;
        perk4.unlocked = false;
        perk5.unlocked = false;
        perk6.unlocked = false;
        perk7.unlocked = false;
        perk8.unlocked = false;

        SaveData tempData = new SaveData();
        tempData.playerInformation = new PlayerInformation[] { info1, info2, info3, info4, info5, info6 };
        tempData.perks = new Perks[] { perk1, perk2, perk3, perk4, perk5, perk6, perk7, perk8 };
        tempData.currentMap = 0;
        tempData.mapsUnlocked = new bool[] { true, false, false, false, false, false, false };
        tempData.currentPlayer = 0;
        tempData.currentPerk1 = -1;
        tempData.currentPerk2 = -1;
        tempData.currentPerk3 = -1;
        tempData.gold = 0;
        tempData.perksUnlocked = new bool[] {false, false};
        data = tempData;

    }

    // called when the object is being loaded
    void Awake()
    {
        // if there is no instance then set this to the instance
        // if there is an instance then kill this version
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // if there is no save data then create one
        if (!fileExists())
        {
            initiliseData();
            saveIntoJson();
        }

        loadFromJson();
    }

    // save the current data to the file
    public void saveIntoJson()
    {
        string temp = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", temp);
    }

    // load the file into the current data
    public void loadFromJson()
    {
        if (fileExists())
        {
            string temp = File.ReadAllText(Application.persistentDataPath + "/saveData.json");
            data = JsonUtility.FromJson<SaveData>(temp);
        }
        else
        {
            // if there is no file then create one
            initiliseData();
            saveIntoJson();
        }
    }

    // check if the save file exists
    public bool fileExists()
    {
        return File.Exists(Application.persistentDataPath + "/saveData.json");
    }
}

// save data classes
[Serializable]
public class SaveData
{
    public int gold;
    public int currentPlayer;
    public int currentPerk1;
    public int currentPerk2;
    public int currentPerk3;
    public int currentMap;
    public bool[] mapsUnlocked;
    public bool[] perksUnlocked;
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