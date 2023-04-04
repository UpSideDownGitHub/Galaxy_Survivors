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

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
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
        Debug.Log("X: " + distX + "\nY: " + distY + "\n");
        if (distX > _maxDistanceX || distY > _maxDistanceY)
        {
            // despawn the enemy
            _enemy.setFree();
        }
    }
}
