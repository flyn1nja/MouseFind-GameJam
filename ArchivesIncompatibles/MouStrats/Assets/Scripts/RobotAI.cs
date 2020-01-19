using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. AI;

public class RobotAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public MouseManager mouseManager;

    public float wanderRadius = 25;
    private Vector3 wanderPoint;

    public bool isMousesOnGround;
    void Start()
    {
        wanderPoint = RandomWanderPoint();
    }

   
    void Update()
    {
        if (isMousesOnGround)
        {
            agent.SetDestination(mouseManager.currentMouse.transform.position);          
        }
        else
        {
            Wander();
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - 90, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public void Wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 2f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    //set la destination aleatoire du wander
    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.GetComponent<>)
    }
}
