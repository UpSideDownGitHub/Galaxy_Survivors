using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    public PlayerStats stats;
    public PlayerPerks perks;

    [Header("Level")]
    public WeaponLevels level;
    public int currentLevel;

    [Header("Health")]
    public int maxHealth;
    [SerializeField] private float health;
    public float damageReduction;

    [Header("UI")]
    public Slider slider;

    [Header("Health Regen")]
    public bool regenHealth;
    public float regenRate;
    private float _timeSiceLastRegen;

    [Header("Sheild")]
    public bool SheildUnlocked;
    public bool sheildActive;
    public float sheidCoolDownTime;
    private float _timeSinceLastSheild;
    public int[] maxBlocks;
    public int currentBlocks;

    [Header("Power Ups")]
    public bool invinsiblePowerup;
    public bool extraLifePowerup;

    [Header("End Screen")]
    public GameObject endScreen;

    // called before the first update frame
    public void Start()
    {
        // set the current level
        currentLevel = level.sheildLevel;

        // set the regen rate & if the player has regen enabled
        regenHealth = perks.healthRegen == 0 ? false : true;
        regenRate = regenHealth ? perks.healthRegenLevels[perks.healthRegen - 1] : 0;

        // sets the max health of the player
        var temp = maxHealth * (perks.healthIncrease == 0 ? 1 : perks.healthIncreaseLevels[perks.healthIncrease - 1]);
        maxHealth = (int)temp;

        // setup the slider to show the health of the player
        health = maxHealth;
        sheildActive = true;
        slider.minValue = 0;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        _timeSinceLastSheild = 0;

        // setup the damage reduction
        damageReduction = (perks.minusDamageTaken == 0 ? 1 : perks.minusDamageTakenLevels[perks.minusDamageTaken - 1]);
    }

    // called once per frame
    public void Update()
    {
        // if regen health is enabled and the health is less than max
        if (regenHealth & health < maxHealth)
        {
            // if enough time has passed to regen health
            if (Time.time > regenRate + _timeSiceLastRegen)
            {
                // reset the regen time and increase the health
                _timeSiceLastRegen = Time.time;
                increaseHealth(1);
            }
        }

        // if the sheild is unlocked and not active then if enough time has passed enable the sheild
        if (SheildUnlocked && !sheildActive)
        {
            // if enough time has passed to enable the sheild
            if (Time.time > sheidCoolDownTime + _timeSinceLastSheild)
            {
                // enable the shield]
                sheildActive = true;
                currentBlocks = maxBlocks[currentLevel];
            }
        }
    }

    /*
     * this function is called whenever the player takes damage
    */ 
    public void removeHealth(float val)
    {
        // if the invincible powerup is enabled then no damage can be done so return
        if (invinsiblePowerup)
            return;

        // decrease the ammount of damage done
        val = val * damageReduction;
        val = val * stats.reduceDamageTakenModifyer;

        // if the player has the sheild active
        if (SheildUnlocked && sheildActive)
        {
            // if the player has blocks left
            if (currentBlocks - 1 > 0)
            {
                // decrease the player blocks by 1 and stop (to make sure no damage is done)
                currentBlocks--;
                return;
            }
            // update the time to make sure the shieild is not instantly replenished
            _timeSinceLastSheild = Time.time;
            sheildActive = false;
            return;
        }
        
        // if the health - the damage is less than 0 then kill the player
        if (health - val < 0)
        {
            // if the player has the extra life powerup then give the player max health, and allow
            // them to continue playing
            if (extraLifePowerup)
            {
                // restart the players life but disable the extra life powerup
                extraLifePowerup = false;
                health = maxHealth;

                // spawn the player death particle to show that they might have died
                ParticlePooler.instance.spawnParticle(3, transform.position, Color.blue);
                return;
            }
            // Kill Player and end the round
            ParticlePooler.instance.spawnParticle(3, transform.position, Color.blue);
            endScreen.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        health -= val;

        // update the health UI
        slider.value = health;
    }

    /*
     *  when called will increase the health by a given ammount,
     *  will also apply
    */ 
    public void increaseHealth(float val)
    {
        // increase health recovered
        val = val / stats.reduceDamageTakenModifyer;

        // incrase the health but if above the max then just set health to be the max
        health = (health + val) > maxHealth ? maxHealth : health + val;
        slider.value = health;
    }

    /*
     * this is used to increase the max health of the player 
    */ 
    public void increaseMaxHealth()
    {
        // increase the max health based on the modifyer in the stats object
        var temp = maxHealth * stats.healthModifyer;
        maxHealth = (int)temp;
    }

    /*
     *  update the current level of the player
    */
    public void updateLevel()
    {
        // increase the leve for the player to be the level of the sheild
        currentLevel = level.sheildLevel;
    }
}
