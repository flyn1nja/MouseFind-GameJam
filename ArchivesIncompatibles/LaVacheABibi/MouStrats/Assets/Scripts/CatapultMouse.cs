using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultMouse : MonoBehaviour
{
    public GameObject catapult;
    public Vector3 catapultMoveAmount;
    public StaminaManager staminaManager;
    public float staminaCost;
    [Space(10)]
    public MouseManager mouseManager;
    public float jumpForce;
    public Rigidbody rigidBody;
    private Transform transform;
    private bool inAnimation;

    void Start() {
        transform = GetComponent<Transform>();
        inAnimation = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!inAnimation)
        {
            //&& staminaManager.currentStamina > 0
            if (other.transform.gameObject == mouseManager.currentMouse.gameObject)
            {
                StartCoroutine(doStuff(other));
            }
        }
    }

    private IEnumerator doStuff(Collider other)
    {
         inAnimation = true;
         mouseManager.disablePlayer = true;

         rigidBody.velocity = Vector3.zero;
         other.attachedRigidbody.isKinematic = true;

         StartAnimation(other);
  
         yield return new WaitForSeconds(1f);

         other.attachedRigidbody.isKinematic = false;

         mouseManager.currentMouse.GetComponent<Rigidbody>().velocity = transform.up * jumpForce + transform.forward * 12;
        // staminaManager.UpdateStaminaOnAction(staminaCost);

         inAnimation = false;
         mouseManager.disablePlayer = false;
     

        yield return new WaitForSeconds(1f);

        catapult.transform.localEulerAngles = Vector3.zero;

    }

    private void StartAnimation(Collider other)
    {
        other.transform.rotation = transform.rotation;
        other.transform.position = transform.position + transform.up /8 -transform.forward /2;
    }
}
