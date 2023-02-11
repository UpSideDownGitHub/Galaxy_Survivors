using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Shotgun : Weapon
{
    [Header("Objects")]
    public GameObject bullet;
    public GameObject firePoint;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _damageModifyer;
    [SerializeField] private float _bulletSpeed;
    public float spread;
    public int bulletCount;

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
            for (int i = 0; i < bulletCount; i++)
            {
                base.fire(bullet, firePoint, spread);
            }
        }
    }
}
