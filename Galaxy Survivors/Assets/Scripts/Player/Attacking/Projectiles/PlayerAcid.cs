using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAcid : MonoBehaviour
{
    public float damage;

    public float deathTime;

    public float attackTime;
    public float attackTimeModifyer;
    private float _timeSinceLastAttack;

    public void Start()
    {
        Destroy(gameObject, deathTime);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;
        if (Time.time > attackTime * attackTimeModifyer + _timeSinceLastAttack)
        {
            _timeSinceLastAttack = Time.time;
            collision.GetComponent<EnemyHealth>().takeDamage(damage);
        }
    }
}
