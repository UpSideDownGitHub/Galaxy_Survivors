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

    public void Start()
    {
        currentLevel = level.sheildLevel;

        regenHealth = perks.healthRegen == 0 ? false : true;
        regenRate = regenHealth ? perks.healthRegenLevels[perks.healthRegen - 1] : 0;

        var temp = maxHealth * (perks.healthIncrease == 0 ? 1 : perks.healthIncreaseLevels[perks.healthIncrease - 1]);
        maxHealth = (int)temp;

        health = maxHealth;
        sheildActive = true;
        slider.minValue = 0;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        _timeSinceLastSheild = 0;
        damageReduction = (perks.minusDamageTaken == 0 ? 1 : perks.minusDamageTakenLevels[perks.minusDamageTaken - 1]);
    }

    public void Update()
    {
        if (regenHealth & health < maxHealth)
        {
            if (Time.time > regenRate + _timeSiceLastRegen)
            {
                increaseHealth(1);
            }
        }

        if (SheildUnlocked && !sheildActive)
        {
            if (Time.time > sheidCoolDownTime + _timeSinceLastSheild)
            {
                sheildActive = true;
                currentBlocks = maxBlocks[currentLevel];
            }
        }
    }

    public void removeHealth(float val)
    {
        // decrease the ammount of damage done
        val = val * damageReduction;
        val = val * stats.reduceDamageTakenModifyer;

        if (SheildUnlocked && sheildActive)
        {
            if (currentBlocks - 1 > 0)
            {
                currentBlocks--;
                return;
            }
            _timeSinceLastSheild = Time.time;
            sheildActive = false;
            return;
        }
        
        if (health - val < 0)
        {
            // Kill Player and end the round
            ParticlePooler.instance.spawnParticle(3, transform.position, Color.blue);
            gameObject.SetActive(false);
            return;
        }
        health -= val;

        // update the health UI
        slider.value = health;

    }

    public void increaseHealth(float val)
    {
        // increase health recovered
        val = val / stats.reduceDamageTakenModifyer;

        health = (health + val) > maxHealth ? maxHealth : health + val;
        slider.value = health;
    }

    public void increaseMaxHealth()
    {
        var temp = maxHealth * stats.healthModifyer;
        maxHealth = (int)temp;
    }

    public void updateLevel()
    {
        currentLevel = level.sheildLevel;
    }
}
