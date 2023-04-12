using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public PlayerStats stats;

    [Header("JoySick Movement")]
    public Joystick movementStick; // left

    [Header("Movement")]
    private float _inputHorzontal;
    private float _inputVertical;
    public Vector2 moveVelocity;
    public float maxSpeed = 180f;
    public float acceleration = 1800f;
    public float friction = 1800f;

    [Header("Gameobject Links")]
    public GameObject healthBar;

    public void Start()
    {
        increaseMoveementSpeed();
    }

    public void Update()
    {
        Move();
        transform.Translate(moveVelocity * Time.deltaTime, Space.World);
    }

    public void Move()
    {
        Vector2 moveDirection = movementStick.joystickVec;
        // TOUCH CONTROLS
        _inputVertical = moveDirection.y;
        _inputHorzontal = moveDirection.x;
        // KEYBOARD CONTROLS
        if (_inputHorzontal == 0 && _inputVertical == 0)
        {
            _inputVertical = Input.GetAxisRaw("Vertical");
            _inputHorzontal = Input.GetAxisRaw("Horizontal");
        }


        float num = moveVelocity.magnitude;

        if (_inputHorzontal != 0 || _inputVertical != 0)
        {
            if (num <= maxSpeed)
            {
                num += acceleration * Time.deltaTime;
                num = Mathf.Min(num, maxSpeed);
            }
            else
            {
                float num2 = friction * Time.deltaTime;
                if (num - num2 >= maxSpeed)
                {
                    num -= num2;
                }
                else
                {
                    num = maxSpeed;
                }
            }
            Vector2 vector = new Vector2(_inputHorzontal, _inputVertical);
            moveVelocity = vector.normalized * num;
        }
        else if (num > 0f)
        {
            num = -friction * Time.deltaTime;
            num = Mathf.Max(num, 0f);
            moveVelocity = moveVelocity.normalized * num;
        }
    }

    public void increaseMoveementSpeed()
    {
        maxSpeed = maxSpeed * stats.movementSpeed;
    }
}
