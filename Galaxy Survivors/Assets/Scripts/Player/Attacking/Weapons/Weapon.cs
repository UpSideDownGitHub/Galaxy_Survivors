using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    // all
    private float damage;
    private float damageModifyer;
    private float bulletSpeed;

    // acid
    private float lifeTime;
    private float attackTime;
    private float attackTimeModifyer;


    public void initiate(float val1, float val2, float val3, float val4, float val5)
    {
        damage = val1;
        damageModifyer = val2;
        lifeTime = val3;
        attackTime = val4;
        attackTimeModifyer = val4;
    }

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

        try
        { 
        tempBullet.GetComponent<PlayerBullet>().damage = damage * damageModifyer;
        }
        catch
        {
            tempBullet.GetComponent<PlayerKnife>().damage = damage * damageModifyer;
        }
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
        
        try
        {
            tempBullet.GetComponent<PlayerBullet>().damage = damage * damageModifyer;
            tempBullet.GetComponent<Rigidbody2D>().AddForce((closest.transform.position - firePoint.transform.position) * bulletSpeed);
        }
        catch
        {
            tempBullet.GetComponent<PlayerRocket>().damage = damage * damageModifyer;
            tempBullet.GetComponent<PlayerRocket>().toFollow = closest.gameObject;

        }
    }

    public void placeAcid(GameObject acid)
    {
        GameObject acidTemp = Instantiate(acid, transform.position, Quaternion.identity);
        PlayerAcid acidObj = acidTemp.GetComponent<PlayerAcid>();
        acidObj.attackTime = attackTime;
        acidObj.attackTimeModifyer = attackTimeModifyer;
        acidObj.damage = damage * damageModifyer;
        acidObj.deathTime = lifeTime;
    }

    public void placeLightning(Vector2 center, float radius, int _lightningCount)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

        Collider2D[] closest = new Collider2D[_lightningCount];
        float[] closestValues = new float[_lightningCount];

        for (int i = 0; i < _lightningCount; i++)
        {
            closestValues[i] = Mathf.Infinity;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Enemy"))
                continue;


            float dist = (center - (Vector2)colliders[i].transform.position).sqrMagnitude;

            for (int j = 0; j < _lightningCount; j++)
            {
                if (dist < closestValues[j])
                {
                    // move all items down 1
                    for (int k = _lightningCount; k < j; k--)
                    {
                        closest[k] = closest[k - 1];
                        closestValues[k] = closestValues[k - 1];
                    }

                    // set cuyrrent as the current distance
                    closestValues[j] = dist;
                    closest[j] = colliders[i];
                    break;
                }
            }
        }

        print("VALUES:");
        for (int i = 0; i < closest.Length; i++)
        {
            print(i + ": ");
            print("    - " + closestValues[i]);
            print("    - " + closest[i]);
        }

        /*
         * find the point and add it if it is greater than the closest point 
        */

    }

    public virtual void startFrame() { }
    public virtual void updateFrame() { }
}
