using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Pistol : Weapon
{
    [Header("Stats")]
    public PlayerStats playerStats;

    [Header("Objects")]
    public GameObject bullet;
    public GameObject firePoint;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed;

    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, _bulletSpeed, playerStats);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            base.fire(bullet, firePoint);
        }
    }
}
