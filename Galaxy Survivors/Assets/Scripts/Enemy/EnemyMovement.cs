using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rb;

    [SerializeField]private float _turnSpeed = 10;
    [SerializeField]private float _moveSpeed = 10;

    [Header("Enemy Spawning")]
    [SerializeField] private float _maxDistanceX = 30;
    [SerializeField] private float _maxDistanceY = 60;
    private Enemy _enemy;

    [Header("Attacking")]
    public bool melee;
    public float damage;
    public float attackDistance;

    public GameObject projectile;
    public float projectileForce;

    [Header("Attack Timer")]
    public float attackTime;
    private float _timeSinceLastAttack;

    [Header("Trails")]
    public GameObject trail1;
    public GameObject trail2;

    [Header("DELETE THESE ITEMS")]
    public float attack_Distance;

    public void OnEnable()
    {
        Invoke("turnOnTrails", 1f);
    }

    public void turnOffTrails()
    {
        trail1.SetActive(false);
        trail2.SetActive(false);
    }

    public void turnOnTrails()
    {
        trail1.SetActive(true);
        trail2.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
        _timeSinceLastAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        _rb.angularVelocity = -_turnSpeed * rotateAmount;
        _rb.velocity = transform.up * _moveSpeed;

        // if not within a certain distance from the player for a given ammount of time

        float distX = Mathf.Abs(transform.position.x - _player.transform.position.x);
        float distY = Mathf.Abs(transform.position.y - _player.transform.position.y);
        //Debug.Log("X: " + distX + "\nY: " + distY + "\n");
        if (distX > _maxDistanceX || distY > _maxDistanceY)
        {
            // despawn the enemy
            turnOffTrails();
            _enemy.setFree();
        }

        // shooting
        if (Time.time < _timeSinceLastAttack + attackTime)
            return;
        _timeSinceLastAttack = Time.time;
        attack_Distance = Vector2.Distance(_player.transform.position, transform.position);
        if (Vector2.Distance(_player.transform.position, transform.position) < attackDistance)
        {
            if (melee)
            {
                // deal damage to the player
                _player.GetComponent<PlayerHealth>().removeHealth(damage);
            }
            else
            {
                // fire a projectile at the player
                GameObject tempBullet = Instantiate(projectile, transform.position, transform.rotation);
                tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.up * projectileForce);
                tempBullet.GetComponent<EnemyBullet>().damage = damage;
            }
        }
    }
}
