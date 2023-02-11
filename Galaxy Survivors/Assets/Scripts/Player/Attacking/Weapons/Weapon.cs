using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    private float damage;
    private float damageModifyer;
    private float bulletSpeed;

    public void initiate(float val1, float val2, float val3)
    {
        damage = val1;
        damageModifyer = val2;
        bulletSpeed = val3;
    }

    public void initiate(float val1, float val2)
    {
        damage = val1;
        damageModifyer = val2;
    }

    public virtual float getDamage() { return damage; }
    public virtual float getDamageModifyer() { return damageModifyer; }
    public virtual float getBuletSpeed() { return bulletSpeed; }

    public virtual void setDamage(float val) { damage = val; }
    public virtual void setDamageModifyer(float val) { damageModifyer = val; }
    public virtual void setBuletSpeed(float val) { bulletSpeed = val; }

    // fire a straight bullet
    public void fire(GameObject bullet, GameObject firePoint)
    {
        var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.up * bulletSpeed);
        tempBullet.GetComponent<PlayerBullet>().damage = damage * damageModifyer;
    }

    // fire a bullet with given spread
    public void fire(GameObject bullet, GameObject firePoint, float spread)
    {
        var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        Vector3 up = tempBullet.transform.up;
        Vector3 calculatedSpread = new Vector3(up.x + Random.Range(-spread, spread), up.y + Random.Range(-spread, spread), up.z);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(calculatedSpread * bulletSpeed);
        tempBullet.GetComponent<PlayerBullet>().damage = damage * damageModifyer;
    }

    // fire a bullet towards the closest enemy
    public void fire(GameObject bullet, GameObject firePoint, Vector2 center, float radius)
    {
        // need to find the closest enemies first
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

        Collider2D closest = null;
        float minDist = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Enemy"))
                continue;
            float dist = (center - (Vector2)colliders[i].transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = colliders[i];
            }
        }
        if (closest == null)
            return;

        // then need to shoot a bullet towards "closest"
        var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        //tempBullet.transform.LookAt(closest.transform.position);
        tempBullet.GetComponent<Rigidbody2D>().AddForce((closest.transform.position - firePoint.transform.position) * bulletSpeed);
        tempBullet.GetComponent<PlayerBullet>().damage = damage * damageModifyer;

    }

    public virtual void startFrame() { }
    public virtual void updateFrame() { }
}
