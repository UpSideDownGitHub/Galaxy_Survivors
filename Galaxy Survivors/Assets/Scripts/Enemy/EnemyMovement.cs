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

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        _rb.angularVelocity = -_turnSpeed * rotateAmount;
        _rb.velocity = transform.up * _moveSpeed;
    }
}
