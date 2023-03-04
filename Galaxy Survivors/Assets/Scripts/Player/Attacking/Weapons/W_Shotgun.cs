using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Shotgun : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;

    [Header("Objects")]
    public GameObject bullet;
    public GameObject firePoint;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;
    public float spread;
    public int bulletCount;

    [Header("Shooting")]
    public float shootRate;
    public float shootRateModifyer;
    private float _timeOfLastShot;

    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Time.time > shootRate * shootRateModifyer + _timeOfLastShot)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                base.fire(bullet, firePoint, spread);
            }
            _timeOfLastShot = Time.time;
        }
    }
}
