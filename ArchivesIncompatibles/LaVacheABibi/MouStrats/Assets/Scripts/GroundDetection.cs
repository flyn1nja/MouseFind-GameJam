using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public RobotAI robot;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<movementMouse>())
        {
            robot.isMousesOnGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<movementMouse>())
        {
            robot.isMousesOnGround = false;
        }
    }
}
