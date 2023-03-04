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
    private float _timeSinceLastPowerup;
    private bool _canActivatePowerup = true;

    // Start is called before the first frame update
    void Start()
    {
        powerupButton.onClick.AddListener(buttonPressed);
    }

    // Update is called once per frame
    void Update()
    {
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
            // TODO -- Implement powerups and then make it so that here in the code the powerup is activated
        }
    }

}
