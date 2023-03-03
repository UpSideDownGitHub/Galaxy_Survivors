using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PassiveLevel : ScriptableObject
{
    public float[] damageModifyer;
    public float[] reduceDamageTakenModifyer;
    public float[] healthModifyer;
    public float[] attackSpeed;
    public int[] projectileCount;
    public float[] movementSpeed;
    public float[] pickupModifyer;
    public float[] xpModifyer;
}