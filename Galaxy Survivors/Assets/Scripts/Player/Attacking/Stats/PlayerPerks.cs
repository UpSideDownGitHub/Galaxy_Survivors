using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerks : ScriptableObject
{
    // player perks
    public int minusDamageTaken;
    public float[] minusDamageTakenLevels;

    public int damageIncrease;
    public float[] damageIncreaseLevels;

    public int movementSpeed;
    public float[] movementSpeedLevels;

    public int healthIncrease;
    public float[] healthIncreaseLevels;

    public int healthRegen;
    public float[] healthRegenLevels;

    public int fireRate;
    public float[] fireRateLevels;

    public int xpIncrease;
    public float[] xpIncreaseLevels;

    public int coinsIncrease;
    public float[] coinsIncreaseLevels;

}
