using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMouse : MonoBehaviour
{
    public Collider collider;
    public Rigidbody rigid;
    public StaminaManager staminaManager;
    public float staminaCost;
    public float chargeForce;

    private void Start()
    {
        collider.enabled = false;
    }

    bool canStund = false;
    void FixedUpdate()
    {
        if (Input.GetButtonDown("SpecialAttribute") && staminaManager.currentStamina - staminaCost  > 0)
        {
            canStund = true;
            collider.enabled = true;
            rigid.AddForce(transform.forward * chargeForce, ForceMode.Impulse);
            staminaManager.UpdateStaminaOnAction(staminaCost);
            StartCoroutine(CloseCollider());
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Rat") && canStund)
        {
            canStund = false;
            other.GetComponent<RatManager>().GetHitByChargeMouse();
        }
    }

    IEnumerator CloseCollider()
    {
        yield return new WaitForSeconds(.5f);
        collider.enabled = false;
        canStund = false;
    }
}
