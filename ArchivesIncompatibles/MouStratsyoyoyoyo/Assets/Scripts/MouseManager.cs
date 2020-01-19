using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{


    public Camera cam;
    public Transform target;
    public bool disablePlayer;

    public movementMouse[] allMouse;

    public movementMouse currentMouse;
    int whichMouse;

    [SerializeField]
    float camFollow;
    float camVertical;
    float camHorizontal;
    public float sensibiliteX = 200.0f;
    public float sensibiliteY = 100.0f;

    public int ratOnAware = 0;


    void Start()
    {     
        for (int i = 0; i < allMouse.Length; i++)
        {
            if(target == null)
            {
                target = allMouse[i].transform;
                if (allMouse[i].gameObject.GetComponent<FatMouse>())
                {
                    allMouse[i].gameObject.GetComponent<FatMouse>().enabled = false;
                }
                if (allMouse[i].gameObject.GetComponent<ChargeMouse>())
                {
                    allMouse[i].gameObject.GetComponent<ChargeMouse>().enabled = false;
                }
                if (allMouse[i].gameObject.GetComponent<CatapultMouse>())
                {
                    allMouse[i].gameObject.GetComponent<CatapultMouse>().enabled = false;
                }
                allMouse[i].agent.enabled = false;
                allMouse[i].AI.enabled = false;
                allMouse[i].rigidPlayer.isKinematic = false;
                currentMouse = allMouse[i];
                whichMouse = 0;
            }
            else
            {
                if (allMouse[i].gameObject.GetComponent<FatMouse>())
                {
                    allMouse[i].gameObject.GetComponent<FatMouse>().enabled = true;
                }
                if (allMouse[i].gameObject.GetComponent<ChargeMouse>())
                {
                    allMouse[i].gameObject.GetComponent<ChargeMouse>().enabled = true;
                }
                if (allMouse[i].gameObject.GetComponent<CatapultMouse>())
                {
                    allMouse[i].gameObject.GetComponent<CatapultMouse>().enabled = true;
                }
                allMouse[i].agent.enabled = true;
                allMouse[i].AI.enabled = true;
                allMouse[i].rigidPlayer.isKinematic = true;
                allMouse[i].enabled = false;
            }
        }
    }

    void Update()
    {
        if (!GameManager.gameIsPaused && !disablePlayer)
        {
            CameraManager();

          

            if (Input.GetButtonDown("SwitchMouseRight"))
            {
                SwitchMouseRight();
            }
            if (Input.GetButtonDown("SwitchMouseLeft"))
            {
                SwitchMouseLeft();
            }
        }
    }

    void SwitchMouseRight()
    {
        if (currentMouse.gameObject.GetComponent<FatMouse>())
        {
            currentMouse.gameObject.GetComponent<FatMouse>().enabled = false;
        }
        if (currentMouse.gameObject.GetComponent<ChargeMouse>())
        {
            currentMouse.gameObject.GetComponent<ChargeMouse>().enabled = false;
        }
        if (currentMouse.gameObject.GetComponent<CatapultMouse>())
        {
            currentMouse.gameObject.GetComponent<CatapultMouse>().enabled = false;
        }
        currentMouse.agent.enabled = true;
        currentMouse.AI.enabled = true;
        currentMouse.rigidPlayer.isKinematic = true;
        currentMouse.enabled = false;
       
        whichMouse++;
        if(whichMouse > allMouse.Length - 1)
        {
            whichMouse = 0;
            
        }
        currentMouse = allMouse[whichMouse];
        target = currentMouse.transform;
        currentMouse.agent.enabled = false;
        currentMouse.AI.enabled = false;
        currentMouse.enabled = true;
        currentMouse.rigidPlayer.isKinematic = false;

    }

    void SwitchMouseLeft()
    {
        currentMouse.agent.enabled = true;
        currentMouse.AI.enabled = true;
        currentMouse.rigidPlayer.isKinematic = true;
        currentMouse.enabled = false;

        whichMouse--;
        if (whichMouse < 0)
        {
            whichMouse = allMouse.Length - 1;

        }
        currentMouse = allMouse[whichMouse];
        target = currentMouse.transform;
        currentMouse.agent.enabled = false;
        currentMouse.AI.enabled = false;
        currentMouse.enabled = true;
        currentMouse.rigidPlayer.isKinematic = false;
        if (currentMouse.gameObject.GetComponent<FatMouse>())
        {
            currentMouse.gameObject.GetComponent<FatMouse>().enabled = true;
        }
        if (currentMouse.gameObject.GetComponent<ChargeMouse>())
        {
            currentMouse.gameObject.GetComponent<ChargeMouse>().enabled = true;
        }
        if (currentMouse.gameObject.GetComponent<CatapultMouse>())
        {
            currentMouse.gameObject.GetComponent<CatapultMouse>().enabled = true;
        }

    }

   

    private void FixedUpdate()
    {
        if (!GameManager.gameIsPaused)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, target.position - cam.transform.forward * 3 + cam.transform.up , camFollow * Time.fixedDeltaTime);
        }
    }

    void CameraManager()
    {
        camVertical = Input.GetAxis("Mouse Y") * sensibiliteY * Time.deltaTime;
        camHorizontal = Input.GetAxis("Mouse X") * sensibiliteX * Time.deltaTime;

        cam.transform.RotateAround(target.position, Vector3.up, camHorizontal);
    }
}
