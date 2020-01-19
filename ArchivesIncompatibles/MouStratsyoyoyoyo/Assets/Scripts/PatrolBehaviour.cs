/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] List<GameObject> waypointList;
    [SerializeField] int waypointIndex;
    [SerializeField] float deltaPositionWaypoint;
    [SerializeField] float viewDistance;
    [SerializeField] float fov;
    [SerializeField] float passiveSpeed;
    [SerializeField] float aggressiveSpeed;

    public GameObject player; // TODO Should look for the player at Start
    private bool isPassive;
    private bool isDetecting;
    private float loseTimer;
    private float loseThreshold;

    // Start is called before the first frame update
    void Start()
    {
        // For now, each patrol need to have at least one waypoint
        if(waypointList.Count == 0)
        {
            Destroy(gameObject);
        }
        InitializeVar();
    }

    // Update is called once per frame
    void Update()
    {
       // print(isPassive);
        SearchForPlayer();
        
        //TODO add a condition to check less often 
        if (isPassive)
        {
            SwitchDestinationPassive();
            
        }
        else
        {
            AggressiveMode();
        }
    }

    // Initiate the patrol with the first waypoint
    private void StartPatrolling()
    {
        agent.destination = waypointList[waypointIndex].transform.position;
    }

    // Check if a waypoint has been reached and switch to the next one
    private void SwitchDestinationPassive()
    {
        if (Vector3.Distance(transform.position, waypointList[waypointIndex].transform.position) < deltaPositionWaypoint)
        {
            if (waypointIndex == waypointList.Count - 1)
            {
                waypointIndex = 0;
            }
            else
            {
                ++waypointIndex;
            }
            agent.destination = waypointList[waypointIndex].transform.position;
        }
    }

    /*public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2)
        {
            Debug.Log(4);
            if (Vector3.Distance(player.transform.position, transform.position) < viewDistance)
            {
                Debug.Log(3);
                RaycastHit hit;
                Debug.Log(Physics.Linecast(transform.position, player.transform.position, out hit, -1));
                {
                    Debug.Log(2);
                    if (hit.transform.CompareTag("Player"))
                    {
                        Debug.Log(1);
                        AggressiveMode();


                        //if (isPassive)
                        //{
                        //    print(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)));
                        //    print(Vector3.Distance(player.transform.position, transform.position));
                        //
                        //    print("isagresive");
                        //
                        //
                        //    isDetecting = true;
                        //    isPassive = false;
                        //    agent.destination = player.transform.position;
                        //    agent.speed = aggressiveSpeed;
                        //}
                    }
                    else
                    {
                        isDetecting = false;
                    }
                }
                else
                {
                    isDetecting = false;
                }
            }
            else
            {
                isDetecting = false;
            }
        }
        else
        {
            isDetecting = false;
        }
    /*}

    void AggressiveMode()
    {
        Debug.Log("yo");
        isPassive = false;
        agent.destination = player.transform.position;

        if (!isDetecting)
        {
            loseTimer += Time.deltaTime;
            if (loseTimer >= loseThreshold)
            {
                print("is passive again");
                isPassive = true;
                agent.speed = passiveSpeed;
                loseTimer = 0;
                agent.destination = waypointList[waypointIndex].transform.position;
            }
        }
    }


    private void InitializeVar()
    {

        viewDistance = 3f;
        deltaPositionWaypoint = 0.2f;
        isPassive = true;
        waypointIndex = 0;
        fov = 120f;
        agent = GetComponent<NavMeshAgent>();
        StartPatrolling();
        passiveSpeed = 2f;
        aggressiveSpeed = 1f;
        isDetecting = false;
        loseThreshold = 2f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            print("dead");
        }
    }
}*/
