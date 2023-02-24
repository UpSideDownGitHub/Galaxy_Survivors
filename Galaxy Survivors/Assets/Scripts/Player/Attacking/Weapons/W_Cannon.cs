using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Cannon : Weapon
{
    public GameObject[] firePoints;
    public GameObject bullet;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _damageModifyer;
    [SerializeField] private float _bulletSpeed;

    // shooting
    public float shootRate;
    private float _timeOfLastShot;


    // Start is called before the first frame update
    public override void startFrame()
    {
        base.initiate(_damage, _damageModifyer, _bulletSpeed);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        if (Time.time > shootRate + _timeOfLastShot)
        {
            for (int i = 0; i < firePoints.Length; i++)
            {
                base.fire(bullet, firePoints[i]);
            }
            _timeOfLastShot = Time.time;
        }
    }
}
