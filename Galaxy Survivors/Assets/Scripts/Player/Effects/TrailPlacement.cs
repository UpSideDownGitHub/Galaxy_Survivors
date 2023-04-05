using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPlacement : MonoBehaviour
{
    public Vector3[] positions;
    public int playerID;

    public GameObject trailRenderer1;
    public GameObject trailRenderer2;

    void Awake()
    {
        trailRenderer1.transform.localPosition = positions[(playerID * 2)];
        trailRenderer2.transform.localPosition = positions[(playerID * 2) + 1];
    }
}
