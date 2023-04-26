using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.TextCore.Text;
using static UnityEngine.ParticleSystem;


public abstract class Weapon : MonoBehaviour
{
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

    public void initiate(float val1, float val2, float val3, float val4, PlayerStats playerStats)
    {
        damage = val1;
        lifeTime = val2;
        attackTime = val3;
        attackTimeModifyer = val4;
        stats = playerStats;
        projPool = ProjectilePool.instance;
    }

    public void initiate(float val1, float val3, PlayerStats playerStats)
    {
        damage = val1;
        bulletSpeed = val3;
        stats = playerStats;
        projPool = ProjectilePool.instance;
    }

    public void initiate(float val1, PlayerStats playerStats)
    {
        damage = val1;
        stats = playerStats;
        projPool = ProjectilePool.instance;
    }

    public virtual float getDamage() { return damage; }
    public virtual float getBuletSpeed() { return bulletSpeed; }
    public virtual void setDamage(float val) { damage = val; }
    public virtual void setBuletSpeed(float val) { bulletSpeed = val; }

    // fire a straight bullet
    public void fire(int bulletID, GameObject firePoint)
    {
        StartCoroutine(fire1(bulletID, firePoint));
    }
    public IEnumerator fire1(int bulletID, GameObject firePoint)
    {
        for (int i = -1; i < stats.projectileCount; i++)
        {
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            if (tempBullet == null)
                continue;
            tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.up * bulletSpeed);
            tempBullet.GetComponent<PlayerBullet>().damage = damage * stats.damageModifyer;

            yield return new WaitForSeconds(0.2f);
        }
    }

    // fire a bullet with given spread
    public void fire(int bulletID, GameObject firePoint, float spread)
    {
            StartCoroutine(fire2(bulletID, firePoint, spread));
    }
    public IEnumerator fire2(int bulletID, GameObject firePoint, float spread)
    {
        var rand1 = Random.Range(-spread, spread);
        var rand2 = Random.Range(-spread, spread);
        for (int i = -1; i < stats.projectileCount; i++)
        {
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            if (tempBullet == null)
                continue;
            //var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
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
    public IEnumerator fire3(int bulletID, GameObject firePoint, Vector2 center, float radius)
    {
        for (int j = -1; j < stats.projectileCount; j++)
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
            //if (closest == null)
            //StopCoroutine("fire3");

            // then need to shoot a bullet towards "closest"
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            if (tempBullet == null)
                continue;
            //var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            //tempBullet.transform.LookAt(closest.transform.position);

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

            yield return new WaitForSeconds(0.2f);
        }
    }

    // Knife fire method
    public void fire(int bulletID, GameObject firePoint, int peirceCount)
    {
        StartCoroutine(fire4(bulletID, firePoint, peirceCount));
    }
    public IEnumerator fire4(int bulletID, GameObject firePoint, int peirceCount)
    {
        for (int i = -1; i < stats.projectileCount; i++)
        {
            var tempBullet = projPool.spawnPickup(bulletID, firePoint.transform.position, firePoint.transform.rotation);
            if (tempBullet == null)
                continue;
            //var tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            tempBullet.GetComponent<Rigidbody2D>().AddForce(tempBullet.transform.up * bulletSpeed);

            tempBullet.GetComponent<PlayerKnife>().damage = damage * stats.damageModifyer;
            tempBullet.GetComponent<PlayerKnife>().maxPeirce = peirceCount;
            tempBullet.GetComponent<PlayerKnife>().pierceCount = peirceCount;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void placeAcid(int acidID, float size)
    {
        var acidTemp = projPool.spawnPickup(acidID, transform.position, Quaternion.identity);
        if (acidTemp == null)
            return;
        //GameObject acidTemp = Instantiate(acid, transform.position, Quaternion.identity);
        acidTemp.transform.localScale = new Vector3(size, size, size);
        PlayerAcid acidObj = acidTemp.GetComponent<PlayerAcid>();
        acidObj.particlesReference.transform.localScale = new Vector3(size/2, size/2, size/2);
        acidObj.attackTime = attackTime;
        acidObj.damage = damage * stats.damageModifyer;
        acidObj.deathTime = lifeTime;
    }

    public void placeLightning(Vector2 center, float radius, int _lightningCount)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

        List<Collider2D> colList = new();
        List<float> colDistList = new();


        for (int i = 0; i < _lightningCount + stats.projectileCount; i++) { colList.Add(null); }
        for (int i = 0; i < _lightningCount + stats.projectileCount; i++) { colDistList.Add(Mathf.Infinity); }

        //Debug.Log("Col List Size: " + colList.Count);
        //Debug.Log("Col Dist List Size: " + colDistList.Count);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].CompareTag("Enemy"))
                continue;
            for (int j = 0; j < colList.Count; j++)
            {
                float dist = (center - (Vector2)colliders[i].transform.position).sqrMagnitude;
                if (dist < colDistList[j] || colDistList[j] == Mathf.Infinity)
                {
                    colDistList.Insert(j, dist);
                    colList.Insert(j, colliders[i]);

                    colDistList.RemoveAt(colDistList.Count - 1);
                    colList.RemoveAt(colList.Count - 1);
                    break;
                }
            }
        }
        //print("Lighing Count: " + _lightningCount);
        for (int i = 0; i < colList.Count; i++)
        {
            //print("Collider " + i + ": " + colList[i].name);
            try
            {
                colList[i].GetComponent<EnemyHealth>().takeDamage(damage * stats.damageModifyer);
                ParticlePooler.instance.spawnParticle(4, colList[i].transform.position, Color.white);
            }
            catch { continue; }
        }
    }

    public int getWeaponLevel() { return _currentWeaponLevel; }
    public void setWeaponLevel(int weaponLevel) { _currentWeaponLevel = weaponLevel; }

    public virtual void startFrame() { }
    public virtual void updateFrame() { }
    public virtual void updateWeaponLevel() { }
}
