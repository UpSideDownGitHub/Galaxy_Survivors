using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    [Header("Button")]
    public Button powerupButton;

    [Header("Powerup Settings")]
    public float powerupCooldown;
    public float powerupTime;
    private float _timeSinceLastPowerup;
    private bool _canActivatePowerup = true;
    private bool _powerupEnabled;

    private int powerupID;

    [Header("Movement Speed")]
    public Movement movement;
    [Header("Invincibility/Extra Life")]
    public PlayerHealth playerHealth;
    [Header("Fire Rate")]
    public W_Shotgun shotgun;
    public W_Pistol pistol;
    public W_Lightning lightning;
    public W_Cannon cannnon;
    public W_Acid acid;
    public W_Knife kinfe;
    public W_Rockets rockets;
    public W_Drone drone;
    public W_Orbs orbs;
    [Header("Damage Increase")]
    public Weapon[] weapons;
    private float[] oldDamages;



    private SaveManager _saveManager;

    // Start is called before the first frame update
    void Start()
    {
        oldDamages = new float[weapons.Length];

        powerupButton.onClick.AddListener(buttonPressed);
        _saveManager = SaveManager.instance;
        powerupID = _saveManager.data.currentPlayer;

        if (powerupID == 4)
        {
            powerupButton.gameObject.SetActive(false);
            playerHealth.extraLifePowerup = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > powerupTime + _timeSinceLastPowerup && _powerupEnabled)
        {
            _powerupEnabled = false;
            disablePowerup();
        }
        if (Time.time > powerupCooldown + _timeSinceLastPowerup && _canActivatePowerup == false)
        {
            _canActivatePowerup = true;
            powerupButton.interactable = true;
        }
    }

    public void buttonPressed()
    {
        if (_canActivatePowerup)
        {
            _timeSinceLastPowerup = Time.time;
            _canActivatePowerup = false;
            powerupButton.interactable = false;
            _powerupEnabled = true;
            enablePowerup();
        }
    }

    public void enablePowerup()
    {
        switch (powerupID)
        {
            case 0:
                movement.movementPowerup = 2;
                break;
            case 1:
                playerHealth.invinsiblePowerup = true;
                break;
            case 2:
                shotgun.shootRatePowerup = 0.5f;
                pistol.shootRatePowerup = 0.5f;
                lightning.shootRatePowerup = 0.5f;
                cannnon.shootRatePowerup = 0.5f;
                acid.shootRatePowerup = 0.5f;
                kinfe.shootRatePowerup = 0.5f;
                rockets.shootRatePowerup = 0.5f;
                drone.shootRatePowerup = 0.5f;
                orbs.shootRatePowerup = 0.5f;
                break;
            case 3:
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<EnemyHealth>().killEnemy();
                }
                break;
            case 4:
                // do nothing as already done in Start()
                break;
            case 5:
                for (int i = 0; i < weapons.Length; i++)
                {
                    oldDamages[i] = weapons[i].getDamage();
                    weapons[i].setDamage(oldDamages[i] * 2);
                }
                break;
            default:
                print("No Player");
                break;
        }
    }

    public void disablePowerup()
    {
        switch (powerupID)
        {
            case 0:
                movement.movementPowerup = 1;
                break;
            case 1:
                playerHealth.invinsiblePowerup = false;
                break;
            case 2:
                shotgun.shootRatePowerup = 1;
                pistol.shootRatePowerup = 1;
                lightning.shootRatePowerup = 1;
                cannnon.shootRatePowerup = 1;
                acid.shootRatePowerup = 1;
                kinfe.shootRatePowerup = 1;
                rockets.shootRatePowerup = 1;
                drone.shootRatePowerup = 1;
                orbs.shootRatePowerup = 1;
                break;
            case 3:
                // Dont need to do anything as this is a one time thing
                break;
            case 4:
                // Nothing
                break;
            case 5:
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].setDamage(oldDamages[i]);
                }
                break;
            default:
                print("No Player");
                break;
        }
    }
}
