using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class movementMouse : MonoBehaviour
{


    public Vector3 rotationOffset;



    public Rigidbody rigidPlayer;

    public NavMeshAgent agent;

    public MouseAI AI;

    public GameObject mainCam;
    Vector3 direction { get; set; }

    [HideInInspector]
    public bool isRunning;
    public bool isGrounded = false;


    [Space(10)]
    public float walkSpeed = 5;
    public float runSpeed = 25;
    public float jumpForce = 250;

    float horizontal;
    float vertical;
    float moveAmount;
    float camVertical;
    float camHorizontal;
    float timer;
    float delta;

    public float groundedDis = 2f;

    void Start()
    {
       
    }   
    void Update()
    {

   
        delta = Time.deltaTime;
        GetInput();
        GetLookRotation();      
        RunManager();
        IsGrounded();
        JumpManager();

        if (Input.GetButtonDown("StayHere"))
        {
            AI.stayHere = !AI.stayHere;
        }
    }
    void GetInput()
    {
        float delta = Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
      
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Math.Abs(vertical));
    }
    void RunManager()
    {
        if (Input.GetButton("Run"))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }    
    void JumpManager()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
             rigidPlayer.AddForce(Vector3.up * jumpForce + transform.forward * jumpForce / 3, ForceMode.Impulse);
         
            //rigidPlayer.velocity = new Vector3(0, jumpForce, 0);
        }

        if (!isGrounded)
        {
            Quaternion rt = Quaternion.LookRotation(rigidPlayer.velocity);
            transform.rotation = rt;
        }
    }
    void IsGrounded()
    {
        Vector3 origin = transform.position;
        
        Vector3 dir = -Vector3.up;
        float dis = groundedDis;

        RaycastHit hit;

        Debug.DrawRay(origin, dir * dis);
        if (Physics.Raycast(origin, dir, dis))
        {
          
            isGrounded = true;
        }
        else
        {
            
            isGrounded = false;
        }



        if (moveAmount > 0)
        {
            rigidPlayer.drag = 0;
        }
        if (moveAmount == 0 && isGrounded)
        {
            rigidPlayer.drag = 4;
        }
    }
    void GetLookRotation()
    {
        float delta = Time.deltaTime;
        Vector3 targetDir = mainCam.transform.forward * vertical;
        targetDir += mainCam.transform.right * horizontal;
        targetDir.Normalize();

        targetDir.y = 0;
        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * 10);

        transform.rotation = targetRotation;
       
    }
    private void FixedUpdate()
    {
        Vector3 currentVelocity = rigidPlayer.velocity;

        if (isGrounded)
        {
            if (isRunning)
                direction = transform.forward * runSpeed;
            else
                direction = transform.forward * walkSpeed;
        }
      

        direction = new Vector3(direction.x, currentVelocity.y, direction.z);

        if (moveAmount > 0 && isGrounded)
        {
            rigidPlayer.velocity = Vector3.Lerp(rigidPlayer.velocity, direction, 0.2f);
        }     
    }  
}
