using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Rockets : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;

    [Header("Shooting")]
    public GameObject bullet;
    public GameObject firePoint;
    public float maxDistance;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;

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
            base.fire(bullet, firePoint, transform.position, maxDistance);
            _timeOfLastShot = Time.time;
        }
    }
}
