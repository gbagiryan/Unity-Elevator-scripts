﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customElevator : MonoBehaviour
{

    public float MaxRange {get{return maxRange;}}
    private const float maxRange = 100f;    
    public GameObject InnerDoorLeft;
    public GameObject InnerDoorRight;
    private Vector3 inLeftDoorPos;
    private Vector3 inRightDoorPos;
    private Vector3 inLeftDoorTarg;
    private Vector3 inRightDoorTarg;

    bool move = false;
    bool arrived = false;
    public float speed=1f;
    public float doorSpeed=1f;
    Vector3 TargetFloor;
    interButton floor;
    public bool isActive=false; 
    ElevatorState elevator = ElevatorState.Closed;

    private void Awake()
    {
        inLeftDoorPos = InnerDoorLeft.transform.localPosition;
        inLeftDoorTarg.x = inLeftDoorPos.x-0.35f;

        inRightDoorPos = InnerDoorRight.transform.localPosition;
        inRightDoorTarg.x = inRightDoorPos.x+0.35f;
    }

    enum ElevatorState {
        Delay,
        Closed,
        Closing,
        Opened,
        Opening
    }

    public void ActivateElevator(interButton floor){
        if(floor!=null){
            isActive = true;
            this.floor = floor;
            move = true;
            TargetFloor = floor.transform.position;
        }
    }

    private void FixedUpdate() {
        if(elevator == ElevatorState.Opened){            
            StartCoroutine(CloseDoorsDelay());
        }
        else if (move == false && arrived == true && elevator == ElevatorState.Closed)
        {
            elevator = ElevatorState.Opening;
        }
        else if(move==true && elevator == ElevatorState.Closed){
            MoveElevator();
        }
        else if(elevator == ElevatorState.Closing)
        {
            CloseDoors();
        }
        else if(elevator == ElevatorState.Opening)
        {
            OpenDoors();
        }
    }
    public void MoveElevator(){

        transform.position = Vector3.MoveTowards(transform.position, TargetFloor, speed*Time.smoothDeltaTime);
            if(transform.position == TargetFloor){
                move=false;
                arrived = true;
            }
    }

    public IEnumerator CloseDoorsDelay(){
        elevator = ElevatorState.Delay;
        yield return new WaitForSeconds(3);
        elevator = ElevatorState.Closing;
    }

    public void OpenDoors(){
        floor.OuterDoorLeft.transform.localPosition = Vector3.MoveTowards(floor.OuterDoorLeft.transform.localPosition, floor.LeftDoorTarg, doorSpeed*Time.smoothDeltaTime);
        floor.OuterDoorRight.transform.localPosition = Vector3.MoveTowards(floor.OuterDoorRight.transform.localPosition, floor.RightDoorTarg, doorSpeed*Time.smoothDeltaTime);
        
        InnerDoorLeft.transform.localPosition = Vector3.MoveTowards(InnerDoorLeft.transform.localPosition, inLeftDoorTarg, doorSpeed*Time.smoothDeltaTime);
        InnerDoorRight.transform.localPosition = Vector3.MoveTowards(InnerDoorRight.transform.localPosition, inRightDoorTarg, doorSpeed*Time.smoothDeltaTime);
        
        if(floor.OuterDoorLeft.transform.localPosition == floor.LeftDoorTarg && floor.OuterDoorRight.transform.localPosition == floor.RightDoorTarg){
            elevator = ElevatorState.Opened;            
        }

    }


    public void CloseDoors(){
                
        floor.OuterDoorLeft.transform.localPosition = Vector3.MoveTowards(floor.OuterDoorLeft.transform.localPosition, floor.LeftDoorPos, doorSpeed*Time.smoothDeltaTime);
        floor.OuterDoorRight.transform.localPosition = Vector3.MoveTowards(floor.OuterDoorRight.transform.localPosition, floor.RightDoorPos, doorSpeed*Time.smoothDeltaTime);
        
        InnerDoorLeft.transform.localPosition = Vector3.MoveTowards(InnerDoorLeft.transform.localPosition, inLeftDoorPos, doorSpeed*Time.smoothDeltaTime);
        InnerDoorRight.transform.localPosition = Vector3.MoveTowards(InnerDoorRight.transform.localPosition, inRightDoorPos, doorSpeed*Time.smoothDeltaTime);

        if(floor.OuterDoorLeft.transform.localPosition == floor.LeftDoorPos && floor.OuterDoorRight.transform.localPosition == floor.RightDoorPos){            
            arrived = false;
            isActive = false;
            elevator = ElevatorState.Closed;
        }

    }
}

