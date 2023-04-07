using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [HideInInspector] public static StatsManager instance;

    [SerializeField] private int _kills;
    [SerializeField] private int _level;
    [SerializeField] private int _XP;
    [SerializeField] private int _coins;

    void Start() { instance = this; }

    public void setKills(int kills, bool add) { _kills = add ? _kills + kills : kills; }
    public int getKills() { return _kills; }

    public void setLevel(int level, bool add) { _level = add ? _level + level : level; }
    public int getLevel() { return _level; }

    public void setXP(int XP, bool add) { _XP = add ? _XP + XP : XP; }
    public int getXP() { return _XP; }

    public void setCoins(int coins, bool add) { _coins = add ? _coins + coins : coins; }
    public int getCoins() { return _coins; }
}
