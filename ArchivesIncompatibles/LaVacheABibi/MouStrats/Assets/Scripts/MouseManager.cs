using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseManager : MonoBehaviour
{


    public Camera cam;
    public Transform target;
    public bool disablePlayer;

    //public movementMouse[] allMouses;
    public List<GameObject> allMouses;
    //public movementMouse[] allMouse;

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
        //List<GameObject> allMouses = new List<GameObject>();
        //allMouses = GameObject.FindGameObjectsWithTag("Player").OrderBy(n => n.name).ToList();
       //allMouses = FindObjectsOfType<movementMouse>().ToList();

        for (int i = 0; i < allMouses.Count; i++)
        {
            if(target == null)
            {
                target = allMouses[i].transform;
                if (allMouses[i].gameObject.GetComponent<FatMouse>())
                {
                    allMouses[i].gameObject.GetComponent<FatMouse>().enabled = false;
                }
                if (allMouses[i].gameObject.GetComponent<ChargeMouse>())
                {
                    allMouses[i].gameObject.GetComponent<ChargeMouse>().enabled = false;
                }
                if (allMouses[i].gameObject.GetComponent<CatapultMouse>())
                {
                    allMouses[i].gameObject.GetComponent<CatapultMouse>().enabled = false;
                }
                allMouses[i].GetComponent<movementMouse>().agent.enabled = false;
                allMouses[i].GetComponent<movementMouse>().AI.enabled = false;
                allMouses[i].GetComponent<movementMouse>().rigidPlayer.isKinematic = false;
                currentMouse = allMouses[i].GetComponent<movementMouse>();
                whichMouse = 0;
            }
            else
            {
                if (allMouses[i].gameObject.GetComponent<FatMouse>())
                {
                    allMouses[i].gameObject.GetComponent<FatMouse>().enabled = true;
                }
                if (allMouses[i].gameObject.GetComponent<ChargeMouse>())
                {
                    allMouses[i].gameObject.GetComponent<ChargeMouse>().enabled = true;
                }
                if (allMouses[i].gameObject.GetComponent<CatapultMouse>())
                {
                    allMouses[i].gameObject.GetComponent<CatapultMouse>().enabled = true;
                }
                allMouses[i].GetComponent<movementMouse>().agent.enabled = true;
                allMouses[i].GetComponent<movementMouse>().AI.enabled = true;
                allMouses[i].GetComponent<movementMouse>().rigidPlayer.isKinematic = true;
                allMouses[i].GetComponent<movementMouse>().enabled = false;
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
        if(whichMouse > allMouses.Count - 1)
        {
            whichMouse = 0;
            
        }
        currentMouse = allMouses[whichMouse].GetComponent<movementMouse>();
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
            whichMouse = allMouses.Count - 1;

        }
        currentMouse = allMouses[whichMouse].GetComponent<movementMouse>();
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

    public void KillPlayer(string name)
    {
        
        if(allMouses.Count == 1)
        {
            Debug.Log("Game Over");
        }
        
        int i = 0;
        int deadMouseIndex = 0;
        foreach (GameObject mouse in allMouses)
        {
            if(mouse.transform.name == name)
            {
                deadMouseIndex = i;
            }
            else
            {
                i++;
            }
        }

        if(currentMouse.transform.name == allMouses[i].transform.name)
        {
            Debug.Log("swwwiiitch");
            SwitchMouseRight();
        }
        allMouses.RemoveAt(i);
        Debug.Log("pouf");
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
