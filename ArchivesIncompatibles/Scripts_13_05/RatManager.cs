using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatManager : MonoBehaviour
{
    public MouseManager mouseManager;
    public float health = 100;

    [Space(10)]
    public float fov = 120f;
    public float viewDistance = 15f;
    public float loseThreshold = 3;
    [Space(10)]
    public float wanderRadius = 9;
    public float wanderSpeed = 3.5f;
    public float chaseSpeed = 7;
    [Space(10)]
    public float attackDistance = 2;

    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool isAware = false;
    private bool isDetecting = false;
    private Vector3 wanderPoint;
    private Rigidbody rigid;
    private Animator anim;
    private float loseTimer;
    private float destroyTimer;
    private ParticleSystem[] bloodPart;


    public bool playSound;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderPoint = RandomWanderPoint();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isAware)
        {
            agent.SetDestination(mouseManager.currentMouse.transform.position);
          
            AttackPlayer();

            if (!isDetecting)
            {
                loseTimer += Time.deltaTime;
                if (loseTimer >= loseThreshold)
                {
                    playSound = false;
                    mouseManager.ratOnAware--;
                    isAware = false;
                    loseTimer = 0;
                }
            }
        }
        else
        {
            agent.stoppingDistance = 0;
            Wander();
            agent.speed = wanderSpeed;
        }

        SearchForPlayer();
    }
    //cherche le joueur
    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(mouseManager.currentMouse.transform.position)) < fov / 2)
        {
            if (Vector3.Distance(mouseManager.currentMouse.transform.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, mouseManager.currentMouse.transform.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
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
    }

    //attack le joueur
    public void AttackPlayer()
    {
        if (Vector3.Distance(mouseManager.currentMouse.transform.position, transform.position) < attackDistance)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, mouseManager.currentMouse.transform.position, out hit, -1))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    ///DAMAGE SUR SOURIEs
                }
            }
        }
    }

    public void GetHitByChargeMouse()
    {
        agent.isStopped = true;
        rigid.isKinematic = true;
        StartCoroutine(Go());
    }
    IEnumerator Go()
    {
        yield return new WaitForSeconds(8f);
        rigid.isKinematic = false;
        agent.isStopped = false;
    }

    // a trouver le joueur
    public void OnAware()
    {
        if(playSound == false)
        {
            mouseManager.ratOnAware++;
            playSound = true;
        }

        isAware = true;
        isDetecting = true;
        loseTimer = 0;
    }

    //se promene de facon aleatoire
    public void Wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 5f)
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
}
