using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public PlayerStats stats;
    public PlayerPerks perks;
    [Header("JoySick Movement")]
    public Joystick movementStick;

    [Header("Movement")]
    private float _inputHorzontal;
    private float _inputVertical;
    public Vector2 moveVelocity;
    public float maxSpeed = 180f;
    public float acceleration = 1800f;
    public float friction = 1800f;
    public float movementPowerup = 1f;

    [Header("Gameobject Links")]
    public GameObject healthBar;

    // called before the first update
    public void Start()
    {
        var temp = perks.movementSpeed == 0 ? 1 : perks.movementSpeedLevels[perks.movementSpeed - 1];
        maxSpeed = maxSpeed * temp;
        increaseMoveementSpeed();
    }

    // called once per frame
    public void Update()
    {
        // move the player in the given direction
        Move();
        transform.Translate(moveVelocity * Time.deltaTime, Space.World);
    }

    /*
    *   this will manage the movement of the player, it will also check what input type they
    *   are using and then move the player based upon that input
    */
    public void Move()
    {
         // TOUCH CONTROLS
        Vector2 moveDirection = movementStick.joystickVec;
        _inputVertical = moveDirection.y;
        _inputHorzontal = moveDirection.x;
        // KEYBOARD CONTROLS
        // if there has been no touch input
        if (_inputHorzontal == 0 && _inputVertical == 0)
        {
            _inputVertical = Input.GetAxisRaw("Vertical");
            _inputHorzontal = Input.GetAxisRaw("Horizontal");
        }
        // find the magnitude of the movement
        float num = moveVelocity.magnitude;

        // if the player has done any input
        if (_inputHorzontal != 0 || _inputVertical != 0)
        {
            // if the players current movement speed is less than the max
            if (num <= maxSpeed * movementPowerup)
            {
                // apply the acceleration to the movement, and limit the movement to the minimum
                // if it is above the minimum
                num += acceleration * Time.deltaTime;
                num = Mathf.Min(num, maxSpeed * movementPowerup);
            }
            else
            {
                // slow down the player using the given friction
                float num2 = friction * Time.deltaTime;
                // if the current movement is still larger than the max
                if (num - num2 >= maxSpeed * movementPowerup)
                {
                    // take of the friction
                    num -= num2;
                }
                else
                {
                    // set the speed to the max speed
                    num = maxSpeed * movementPowerup;
                }
            }
            // calculate the move velocity for the current movement
            Vector2 vector = new Vector2(_inputHorzontal, _inputVertical);
            moveVelocity = vector.normalized * num;
        }
        else if (num > 0f)
        {
            // apply friction until not moving
            num = -friction * Time.deltaTime;
            num = Mathf.Max(num, 0f);
            moveVelocity = moveVelocity.normalized * num;
        }
    }

    /*
    *   this is called to increase the movement speed of the player,
    *   using the stats value for movement speed.
    */
    public void increaseMoveementSpeed()
    {
        maxSpeed = maxSpeed * stats.movementSpeed;
    }
}
