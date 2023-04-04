using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorRandomizer : MonoBehaviour
{
    public Gradient color;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnEnable()
    {
        spriteRenderer.color = color.Evaluate(Random.value);
    }
}
