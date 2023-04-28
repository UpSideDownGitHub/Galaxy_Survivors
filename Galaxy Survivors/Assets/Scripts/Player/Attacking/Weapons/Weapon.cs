using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.TextCore.Text;
using static UnityEngine.ParticleSystem;


public abstract class Weapon : MonoBehaviour
{
    // private variables
    PlayerStats stats;
    WeaponLevels levels;
    ProjectilePool projPool;

    private int _currentWeaponLevel;

    // all
    private float damage;
    private float damageModifyer;
    private float bulletSpeed;

    // acid
    private float lifeTime;
    private float attackTime;
    private float attackTimeModifyer;

    /*
    *   this will initialize the weapon script for the weapon selected
    */
    public void initiate(float val1, float val2, float val3, float val4, PlayerStats playerStats)
    {
        damage = val1;
        lifeTime = val2;
        attackTime = val3;
        attackTimeModifyer = val4;
        stats = playerStats;
        projPool = ProjectilePool.instance;
    }

    /*
    *   this will initialize the weapon script for the weapon selected
    */
    public void initiate(float val1, float val3, PlayerStats playerStats)
    {
        damage = val1;
        bulletSpeed = val3;
        stats = playerStats;
        projPool = ProjectilePool.instance;
    }

    /*
    *   this will initialize the weapon script for the weapon selected
    */
    public void initiate(float val1, PlayerStats playerStats)
    {
        damage = val1;
        stats = playerStats;
        projPool = ProjectilePool.instance;
    }

    // getter & setters
    public virtual float getDamage() { return damage; }
    public virtual float getBuletSpeed() { return bulletSpeed; }
    public virtual void setDamage(float val) { damage = val; }
    public virtual void setBuletSpeed(float val) { bulletSpeed = val; }

    // fire a straight bullet
    public void fire(int bulletID, GameObject firePoint)
    {
        StartCoroutine(fire1(bulletID, firePoint));
    }
    // IEnumerator for fire1 (this is because, i need to be able to wait for a small amount of time,
    // allowing for the multiple bullet to be shot)
    public IEnumerator fire1(int bulletID, GameObject firePoint)
    {
        // for the amount of bullets to shoot
        for (int i = -1; i < stats.projectileCount; i++)
        {
            // shoot the bullet (using the bullet pool)
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            if (tempBullet == null)
                continue;
            tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.up * bulletSpeed);
            tempBullet.GetComponent<PlayerBullet>().damage = damage * stats.damageModifyer;
            
            // wait a bit before shooting the next bullet
            yield return new WaitForSeconds(0.2f);
        }
    }

    // fire a bullet with given spread
    public void fire(int bulletID, GameObject firePoint, float spread)
    {
            StartCoroutine(fire2(bulletID, firePoint, spread));
    }
    // IEnumerator for fire2
    public IEnumerator fire2(int bulletID, GameObject firePoint, float spread)
    {
        // calculate the spread for each shot
        var rand1 = Random.Range(-spread, spread);
        var rand2 = Random.Range(-spread, spread);
        // loop through all of the bullets that are needed
        for (int i = -1; i < stats.projectileCount; i++)
        {
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            // if the bullet cannot be pooled then continue
            if (tempBullet == null)
                continue;
            //var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            // spawn the bullet
            Vector3 up = tempBullet.transform.up;
            Vector3 calculatedSpread = new Vector3(up.x + rand1, up.y + rand2, up.z);
            tempBullet.GetComponent<Rigidbody2D>().AddForce(calculatedSpread * bulletSpeed);
            tempBullet.GetComponent<PlayerBullet>().damage = damage * stats.damageModifyer;
            yield return new WaitForSeconds(0.2f);
        }
    }

    // fire a bullet towards the closest enemy
    public void fire(int bulletID, GameObject firePoint, Vector2 center, float radius)
    {
        StartCoroutine(fire3(bulletID, firePoint, center, radius));
    }
    // IEnumerator for fire3
    public IEnumerator fire3(int bulletID, GameObject firePoint, Vector2 center, float radius)
    {
        // loop through all of the bullets to shoot
        for (int j = -1; j < stats.projectileCount; j++)
        {
            // need to find the closest enemies first
            Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

            // get all of the enemies within a certain distance from the player
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
            //if (closest == null)
            //StopCoroutine("fire3");

            // then need to shoot a bullet towards "closest"
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            if (tempBullet == null)
                continue;
            //var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            //tempBullet.transform.LookAt(closest.transform.position);

            // try and spawn a player bullet if it fails, then i know that it is a rocket that 
            // needs shooting so spawn a rocket
            try
            {
                tempBullet.GetComponent<PlayerBullet>().damage = damage * stats.damageModifyer;
                if (closest != null)
                    tempBullet.GetComponent<Rigidbody2D>().AddForce((closest.transform.position - firePoint.transform.position) * bulletSpeed);
                else
                    tempBullet.GetComponent<Rigidbody2D>().AddForce((transform.position - firePoint.transform.position) * bulletSpeed);
            }
            catch
            {
                tempBullet.GetComponent<PlayerRocket>().damage = damage * stats.damageModifyer;
                if (closest != null)
                    tempBullet.GetComponent<PlayerRocket>().toFollow = closest.gameObject;
                else
                    tempBullet.GetComponent<PlayerRocket>().toFollow = null;
            }
            // wait a small amount of time before shooting the next bullets
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Knife fire method
    public void fire(int bulletID, GameObject firePoint, int peirceCount)
    {
        StartCoroutine(fire4(bulletID, firePoint, peirceCount));
    }
    // IEnumerator for fire4
    public IEnumerator fire4(int bulletID, GameObject firePoint, int peirceCount)
    {
        // for all of the bullets to shoot
        for (int i = -1; i < stats.projectileCount; i++)
        {
            // spawn the bullet (using pool)
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            if (tempBullet == null)
                continue;
            //var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.up * bulletSpeed);

            // fire the bullet
            tempBullet.GetComponent<PlayerKnife>().damage = damage * stats.damageModifyer;
            tempBullet.GetComponent<PlayerKnife>().maxPeirce = peirceCount;
            tempBullet.GetComponent<PlayerKnife>().pierceCount = peirceCount;
            yield return new WaitForSeconds(0.2f);
        }
    }

    /*
    *   place acid at the given size and position
    */
    public void placeAcid(int acidID, float size)
    {
        // spawn the acid
        var acidTemp = projPool.spawnPickup(acidID, transform.position, Quaternion.identity);
        if (acidTemp == null)
            return;
        //GameObject acidTemp = Instantiate(acid, transform.position, Quaternion.identity);
        // spawn the acid
        acidTemp.transform.localScale = new Vector3(size, size, size);
        PlayerAcid acidObj = acidTemp.GetComponent<PlayerAcid>();
        acidObj.particlesReference.transform.localScale = new Vector3(size/2, size/2, size/2);
        acidObj.attackTime = attackTime;
        acidObj.damage = damage * stats.damageModifyer;
        acidObj.deathTime = lifeTime;
    }

    /*
    *   place lightning at the given position, also only spawn the correct,
    *   amount of lightning
    */
    public void placeLightning(Vector2 center, float radius, int _lightningCount)
    {
        // initialize the list that will be used to find the enemies
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);
        List<Collider2D> colList = new();
        List<float> colDistList = new();

        // add the colliders to the list's, depending on the amount of lightnings allowed
        for (int i = 0; i < _lightningCount + stats.projectileCount; i++) { colList.Add(null); }
        for (int i = 0; i < _lightningCount + stats.projectileCount; i++) { colDistList.Add(Mathf.Infinity); }

        //Debug.Log("Col List Size: " + colList.Count);
        //Debug.Log("Col Dist List Size: " + colDistList.Count);

        // for the amount of lighting colldiers that are needed
        for (int i = 0; i < colliders.Length; i++)
        {
            // if the collider is not an enemy
            if (!colliders[i].CompareTag("Enemy"))
                continue;
            // for the ammount of colliders
            for (int j = 0; j < colList.Count; j++)
            {
                // if within a certain distance
                float dist = (center - (Vector2)colliders[i].transform.position).sqrMagnitude;
                if (dist < colDistList[j] || colDistList[j] == Mathf.Infinity)
                {
                    // add it to the list and remove the item it replaced
                    colDistList.Insert(j, dist);
                    colList.Insert(j, colliders[i]);

                    colDistList.RemoveAt(colDistList.Count - 1);
                    colList.RemoveAt(colList.Count - 1);
                    break;
                }
            }
        }
        // for all of the lightnings found
        for (int i = 0; i < colList.Count; i++)
        {
            // try and spawn the lightning
            try
            {
                colList[i].GetComponent<EnemyHealth>().takeDamage(damage * stats.damageModifyer);
                ParticlePooler.instance.spawnParticle(4, colList[i].transform.position, Color.white);
            }
            catch { continue; }
        }
    }

    // getters & setters
    public int getWeaponLevel() { return _currentWeaponLevel; }
    public void setWeaponLevel(int weaponLevel) { _currentWeaponLevel = weaponLevel; }

    public virtual void startFrame() { }
    public virtual void updateFrame() { }
    public virtual void updateWeaponLevel() { }
}
