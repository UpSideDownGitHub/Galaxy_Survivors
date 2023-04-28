using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [Header("Gameobjects")]
    public GameObject player;

    [Header("Camera Movement")]
    public float lerpTime;

    /*
     * called after all update()'s have been called this will be called
    */ 
    void LateUpdate()
    {
        // if game is currently running I.E. the timescale is greateer than 0
        if (Time.timeScale > 0)
        {
            // lerp the camera to the players position, increase the speed of the lerp the further away the 
            // camera gets
            float speedChanger = Vector2.Distance(transform.position, player.transform.position);
            Vector3 newPos = Vector3.Lerp(transform.position, player.transform.position, lerpTime * speedChanger * Time.deltaTime);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}
