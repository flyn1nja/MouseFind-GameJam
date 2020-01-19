﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertTrapBoxCheck : MonoBehaviour
{

    public bool isAlerted;
    // Start is called before the first frame update
    void Start()
    {
        isAlerted = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(GetComponentInParent<TrapSetup>().isOnTimer && other.transform.name == GetComponentInParent<TrapSetup>().leaderMouse.transform.name)
        {
            isAlerted = true;
            Invoke("CountdownToHurt", GetComponentInParent<TrapSetup>().secForTimer);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (GetComponentInParent<TrapBehaviour>().leaderMouse.transform.name == other.transform.name && !isAlerted)
        {
            isAlerted = false;
        }
    }

    void CountdownToHurt()
    {
        if(GetComponentInParent<TrapSetup>().isInsideHurtBox)
        {
            // HURTS THE PLAYER
        }
        if(isAlerted)
        {
            Invoke("CountdownToHurt", GetComponentInParent<TrapSetup>().secForTimer);
        }
    }
}
