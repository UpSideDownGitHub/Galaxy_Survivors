using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorRandomizer : MonoBehaviour
{
    public Gradient[] colors;
    private SpriteRenderer spriteRenderer;
    private SaveManager _saveManager;
    public int mapID;

    // called when the object is being loaded
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // loaded the map id from the save data
    public void Start()
    {
        _saveManager = SaveManager.instance;
        mapID = _saveManager.data.currentMap;
    }
        
    // set the color to be a random color for the color of the current map
    public void OnEnable()
    {
        spriteRenderer.color = colors[mapID].Evaluate(Random.value);
    }
}
