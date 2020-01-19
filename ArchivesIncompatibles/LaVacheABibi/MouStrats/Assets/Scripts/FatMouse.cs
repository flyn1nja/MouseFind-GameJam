using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatMouse : MonoBehaviour
{
    GameObject carryableObject;
    GameObject carryingObject;

    public StaminaManager staminaManager;
    public float howManyStaminaOnCarrying;


    bool carryAnObject;
    void Update()
    {
       

        if(Input.GetButtonDown("SpecialAttribute") && !carryAnObject && staminaManager.currentStamina > 0)
            if(carryableObject != null)
            {
                carryingObject = carryableObject;
                carryAnObject = true;
                carryableObject.GetComponent<BoxCollider>().enabled = false;
                carryableObject.GetComponent<Rigidbody>().isKinematic = true;
                carryableObject.transform.rotation = transform.rotation;          
                carryableObject.transform.parent = transform;
                carryableObject.transform.position =  transform.position + transform.up * 3.1f;               
                return;
            }
      

        if (Input.GetButtonDown("SpecialAttribute") && carryAnObject)
        {
            carryAnObject = false;
            carryingObject.GetComponent<BoxCollider>().enabled = true;
            carryingObject.GetComponent<Rigidbody>().isKinematic = false;
            carryingObject.transform.parent = null;
            carryingObject.transform.position = transform.position + transform.forward * 3.5f;
        }

        if (carryAnObject == true)
        {
            if (staminaManager.currentStamina > 0)
                staminaManager.currentStamina -= Time.deltaTime * howManyStaminaOnCarrying;
            if (staminaManager.currentStamina <= 0)
            {
                staminaManager.currentStamina = 0;
                carryAnObject = false;
                carryingObject.GetComponent<BoxCollider>().enabled = true;
                carryingObject.GetComponent<Rigidbody>().isKinematic = false;
                carryingObject.transform.parent = null;
                carryingObject.transform.position = transform.position + transform.forward * 3.5f;
            }
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 11 && carryableObject == null)
        {
            carryableObject = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.layer == 11)
        {
            carryableObject = null;

        }
    }
}

