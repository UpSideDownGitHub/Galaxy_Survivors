using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [Header("Gameobjects")]
    public GameObject player;

    [Header("Camera Movement")]
    public float lerpTime;

    void LateUpdate()
    {
        if (Time.timeScale > 0)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, player.transform.position, lerpTime * Time.deltaTime);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}
