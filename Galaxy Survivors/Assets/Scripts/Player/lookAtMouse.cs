using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class lookAtMouse : MonoBehaviour
{
    public Joystick lookingStick; // right stick
    public int angleOffset;
    public float rotateSpeed;
    private Quaternion previousRotation;

    void Update()
    {
        // TOUCH CONTROLS
        Vector2 dir = lookingStick.joystickVec;
        if (dir.y != 0 && dir.x != 0)
        {
            Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, dir);
            previousRotation = toRotation2;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation2, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, previousRotation, rotateSpeed * Time.deltaTime);
            // MOUSE CONTROLS
            var dir2 = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg + angleOffset;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
