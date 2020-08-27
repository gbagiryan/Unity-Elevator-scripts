﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interButton : MonoBehaviour, IInteractable
{

    public float MaxRange {get{return maxRange;}}
    private const float maxRange = 100f;

    public customElevator Elevator;
    public GameObject OuterDoorLeft;
    public GameObject OuterDoorRight;
    public Vector3 LeftDoorTarg;
    public Vector3 LeftDoorPos;
    public Vector3 RightDoorPos;
    public Vector3 RightDoorTarg;

    // Start is called before the first frame update
    private void Awake() 
    {
        LeftDoorPos = OuterDoorLeft.transform.localPosition;
        LeftDoorTarg.x = LeftDoorPos.x+0.35f;

        RightDoorPos = OuterDoorRight.transform.localPosition;
        RightDoorTarg.x = RightDoorPos.x-0.35f;
    }
    public void OnInteract()
    {
        if (Elevator.isActive==false)
        {
            Elevator.ActivateElevator(this);
        }        
    }
   
    public void OnStartHover()
    {
        
    }
    public void OnEndHover()
    {
        
    }
}
