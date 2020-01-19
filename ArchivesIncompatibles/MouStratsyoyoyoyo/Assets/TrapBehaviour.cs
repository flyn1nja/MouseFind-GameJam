using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    public movementMouse leaderMouse;

    // Start is called before the first frame update
    void Start()
    {
        leaderMouse = GameObject.Find("MouseManager").GetComponent<MouseManager>().currentMouse;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(leaderMouse.transform.name);
    }
}
