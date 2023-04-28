using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Movement Joystick")]
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    // Start is called before the first frame update
    void Start()
    {
        // get the original position and the radius of the joystick at the start of the game
        Input.multiTouchEnabled = true;
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y /3;
    }

    //Pointer down code 
    public void OnPointerDown(PointerEventData baseEventData)
    {
        // if the player is currently shooting then 
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        joystick.SetActive(true);
        joystickTouchPos = joystick.transform.position;
    }

    //Drag code 
    public void OnDrag(PointerEventData baseEventData)
    {
        // get the position of the pointer
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        // move the joystick based on its position, but if above the max set it to the max
        if (joystickDist < joystickRadius)
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        else
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
    }
    //Pointer up code 
    public void OnPointerUp(PointerEventData eventData)
    {
        // move the joystick back to the original position and disable it so it
        // cant be seen
        joystick.SetActive(false);
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
    }
}
