using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    [SerializeField] private float health;

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
    }

    public void Update()
    {
        if (SheildUnlocked)
        {
            if (sheildActive)
            {

            }
            else if (Time.time > sheidCoolDownTime + _timeSinceLastSheild)
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
            _timeSinceLastSheild = Time.timeScale;
            sheildActive = false;
            return;
        }
        
        if (health - val < 0)
        {
            // Kill Player and end the round
            return;
        }
        health -= val;
    }
}
