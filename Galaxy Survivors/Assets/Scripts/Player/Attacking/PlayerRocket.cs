using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;
using static UnityEngine.GraphicsBuffer;

public class PlayerRocket : MonoBehaviour
{
    [Header("Following")]
    public GameObject toFollow;
    public float turnSpeed;
    public float movingSpeed;
    private Rigidbody2D _rb;

    [Header("Rocket")]
    public float damage;
    public float explosionRadius;

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = toFollow.transform.position - transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        _rb.angularVelocity = -turnSpeed * rotateAmount;
        _rb.velocity = transform.up * movingSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // NEED TO DEAL DAMAGE TO THE ENEMY HERE
            Destroy(gameObject);
        }
    }
}
