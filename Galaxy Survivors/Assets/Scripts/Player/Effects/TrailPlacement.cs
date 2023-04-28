using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPlacement : MonoBehaviour
{
    // public variables
    public Vector3[] positions;
    public int playerID;

    public GameObject trailRenderer1;
    public GameObject trailRenderer2;

    // called when the object is being loaded
    void Awake()
    {
        // move the trail renderer to the correct position on the current player
        trailRenderer1.transform.localPosition = positions[(playerID * 2)];
        trailRenderer2.transform.localPosition = positions[(playerID * 2) + 1];
    }
}
