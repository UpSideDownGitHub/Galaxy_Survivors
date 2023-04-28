using System.Collections;
using System.Collections.Generic;
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
        // create a list with the length of weapons
        oldDamages = new float[weapons.Length];

        // add a listener to the powerup buttons
        powerupButton.onClick.AddListener(buttonPressed);

        // load the save manager and the powerup ID
        _saveManager = SaveManager.instance;
        powerupID = _saveManager.data.currentPlayer;

        // if the powerupID that means, that it is the extra life powerup so don't show the,
        // power up button as it is not needed 
        if (powerupID == 4)
        {
            // turn off the powerup button
            powerupButton.gameObject.SetActive(false);
            playerHealth.extraLifePowerup = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if enough time has passed since the powerup was last used
        if (Time.time > powerupTime + _timeSinceLastPowerup && _powerupEnabled)
        {
            // turn off the powerup
            _powerupEnabled = false;
            disablePowerup();
        }
        // if the powerup cool down has passed, then turn on the powerup button, so it can
        // be used
        if (Time.time > powerupCooldown + _timeSinceLastPowerup && _canActivatePowerup == false)
        {
            // turn back on the powerup button
            _canActivatePowerup = true;
            powerupButton.interactable = true;
        }
    }
    
    /*
    *   if the powerup button is pressed then apply the powerup, if possible
    */
    public void buttonPressed()
    {
        // if the powerup can be activated
        if (_canActivatePowerup)
        {
            // enable the powerup
            _timeSinceLastPowerup = Time.time;
            _canActivatePowerup = false;
            powerupButton.interactable = false;
            _powerupEnabled = true;
            enablePowerup();
        }
    }

    /*
    *   depending on what powerup that is being used then, apply the effect of the powerup
    */
    public void enablePowerup()
    {
        // switch depending on what powerup that is currently selected
        switch (powerupID)
        {
            // movement powerup
            case 0:
                movement.movementPowerup = 2;
                break;
            // Invisible powerup
            case 1:
                playerHealth.invinsiblePowerup = true;
                break;
            // Shootrate powerup
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
            // Kill all enemies powerup
            case 3:
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<EnemyHealth>().killEnemy();
                }
                break;
            // Extra life powerup
            case 4:
                // do nothing as already done in Start()
                break;
            // Extra Damage Powerup
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

    /*
    *   disable the powerup the has been selected
    */
    public void disablePowerup()
    {
        switch (powerupID)
        {
            // Movement power up
            case 0:
                movement.movementPowerup = 1;
                break;
            // Invisible powerup 
            case 1:
                playerHealth.invinsiblePowerup = false;
                break;
            // shoot rate powerup
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
            // Kill all enemies
            case 3:
                // Don't need to do anything as this is a one time thing
                break;
            // Extra life powerup
            case 4:
                // Nothing
                break;
            // Increase Damage Powerup
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
