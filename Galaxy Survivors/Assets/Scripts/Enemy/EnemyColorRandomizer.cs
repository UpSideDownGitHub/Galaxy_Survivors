using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorRandomizer : MonoBehaviour
{
    public Gradient[] colors;
    private SpriteRenderer spriteRenderer;
    private SaveManager _saveManager;
    public int mapID;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        _saveManager = SaveManager.instance;
        mapID = _saveManager.data.currentMap;
    }
        
    public void OnEnable()
    {
        spriteRenderer.color = colors[mapID].Evaluate(Random.value);
    }
}
