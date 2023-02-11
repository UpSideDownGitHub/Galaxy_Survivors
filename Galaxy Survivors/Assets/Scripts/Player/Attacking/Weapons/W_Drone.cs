using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Drone : Weapon
{
    public GameObject droneObject;
    private GameObject _drone;
    public GameObject firePoint;
    public GameObject bullet;
    public Vector3 droneSpawnOffset;

    [Header("Values")]
    [SerializeField] private float _damage;
    [SerializeField] private float _damageModifyer;
    [SerializeField] private float _bulletSpeed;

    public float maxDistance;

    // shooting
    public float shootRate;
    private float _timeOfLastShot;

    [Header("Drone Movement")]
    public float lerpTime;

    // Start is called before the first frame update
    public override void startFrame()
    {
        _drone = Instantiate(droneObject, transform.position + droneSpawnOffset, Quaternion.identity);
        firePoint = _drone.transform.GetChild(0).gameObject;

        base.initiate(_damage, _damageModifyer, _bulletSpeed);
    }

    // Update is called once per frame
    public override void updateFrame()
    {
        _drone.transform.position = Vector3.Lerp(_drone.transform.position, transform.position + droneSpawnOffset, lerpTime);

        if (Time.time > shootRate + _timeOfLastShot)
        {
            base.fire(bullet, firePoint, transform.position, maxDistance);
            _timeOfLastShot = Time.time;
        }
    }
}
