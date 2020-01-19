using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] int actualHP;
    int maxHP;
    [SerializeField] float invulnerabilityTimer;
    [SerializeField] bool isInvulnerable;


    // Start is called before the first frame update
    void Start()
    {
        maxHP = 3;
        actualHP = maxHP;
        isInvulnerable = false;
    }

    public void GettingHurtABit()
    {
        if(!isInvulnerable)
        {
            if(actualHP > 1)
            {
                actualHP--;
                isInvulnerable = true;
                Invoke("StopInvulnerability", invulnerabilityTimer);
            }
            else
            {
                GameObject.Find("MouseManager").GetComponent<MouseManager>().KillPlayer(transform.name);
            }
        }
    }

    public void KillOneShot()
    {
        GameObject.Find("MouseManager").GetComponent<MouseManager>().KillPlayer(transform.name);
    }

    private void StopInvulnerability()
    {
        isInvulnerable = false;
    }


}
