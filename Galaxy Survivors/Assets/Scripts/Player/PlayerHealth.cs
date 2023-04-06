using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    [SerializeField] private float health;

    [Header("UI")]
    public Slider slider;

    [Header("Sheild")]
    public bool SheildUnlocked;
    public bool sheildActive;
    public float sheidCoolDownTime;
    private float _timeSinceLastSheild;

    public int maxBlocks;
    public int currentBlocks;

    public void Start()
    {
        health = maxHealth;
        sheildActive = true;
        slider.minValue = 0;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        _timeSinceLastSheild = 0;
    }

    public void Update()
    {
        if (SheildUnlocked && !sheildActive)
        {
            if (Time.time > sheidCoolDownTime + _timeSinceLastSheild)
            {
                sheildActive = true;
                currentBlocks = maxBlocks;
            }
        }
    }

    public void removeHealth(float val)
    {
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
}
