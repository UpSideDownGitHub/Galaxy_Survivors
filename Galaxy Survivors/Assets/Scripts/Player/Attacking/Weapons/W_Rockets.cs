using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Rockets : Weapon
{
    [Header("Shooting")]
    public GameObject bullet;
    public GameObject firePoint;
    public float maxDistance;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _damageModifyer;
    [SerializeField] private float _bulletSpeed;

    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, _damageModifyer, _bulletSpeed);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            base.fire(bullet, firePoint, transform.position, maxDistance);
        }
    }
}
