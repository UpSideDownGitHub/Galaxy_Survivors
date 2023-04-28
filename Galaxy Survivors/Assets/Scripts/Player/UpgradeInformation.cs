using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInformation : ScriptableObject
{
    // weapon info
    public Sprite[] weaponSprites;
    public string[] weaponTitle;
    public string[] weaponDescription;

    // passive info
    public Sprite[] passiveSprites;
    public string[] passiveTitle;
    public string[] passiveDescription;
}
