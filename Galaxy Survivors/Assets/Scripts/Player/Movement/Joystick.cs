using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    [Header("Shooting Joystick")]
    public bool shooting;
    public bool currentlyShooting;

    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y /3; //Makes the joystick area to move bigger. Decrease or increase this
    }

    //Pointer down code 
    public void OnPointerDown(PointerEventData baseEventData)
    {
        if (shooting)
            currentlyShooting = true;
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        joystick.SetActive(true);
        joystickTouchPos = joystick.transform.position;
    }

    //Drag code 
    public void OnDrag(PointerEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        else
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
    }
    //Pointer up code 
    public void OnPointerUp(PointerEventData eventData)
    {
        if (shooting)
            currentlyShooting = false;
        joystick.SetActive(false);
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
    }
}
