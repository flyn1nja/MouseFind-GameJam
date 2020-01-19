﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSetup : MonoBehaviour
{
    public movementMouse leaderMouse;

    public bool isAlwaysActive;
    public bool isOnTimer;
    public bool isInsideHurtBox;

    public float secForTimer;

    // Start is called before the first frame update
    void Start()
    {
        isInsideHurtBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAlwaysActive && other.transform.name == leaderMouse.transform.name)
        {
            // HURTS THE PLAYER
        }
        else if (isOnTimer && other.transform.name == leaderMouse.transform.name)
        {
            isInsideHurtBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInsideHurtBox && other.transform.name == leaderMouse.transform.name)
        {
            isInsideHurtBox = false;
        }
    }
}
