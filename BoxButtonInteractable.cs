﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxButtonInteractable : MonoBehaviour, IInteractable
{
    public float MaxRange {get{return maxRange;}}
    private const float maxRange = 100f;
    public Transform Elev;
    private interButton old;
    private void Awake()
    {
        old = transform.parent.GetComponentInChildren<interButton>();
        transform.SetParent(Elev);
    }

    public void OnInteract()
    {
        old.OnInteract();
    }

    public void OnStartHover()
    {
        
    }
    public void OnEndHover()
    {
        
    }

}
