using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public float stamina = 2;
    public Transform player;

    public bool isOnRange;
    float temps;
    float randomizer;

    enum TypeItem { Piment, Pain, Sucre }

    [SerializeField]
    TypeItem typeItem;

    private void Start() => randomizer = UnityEngine.Random.Range(0.5f, 1.5f) * Mathf.PI;

    private void Update()
    {
        temps += Time.deltaTime;

        transform.position += Mathf.Sin(temps * randomizer) * Vector3.up/20;
        transform.Rotate(Time.deltaTime * Vector3.up * 25 * randomizer);

        if (isOnRange)
        {
            GoToPlayer();
        }
    }

    void GoToPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * 10);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 10);
        StartCoroutine(DestroyItem());
        
    }

    IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(.3f);
        EffectuerAction();
        Destroy(gameObject);
    }

    private void EffectuerAction()
    {
        
        switch (typeItem)
        {
            case TypeItem.Piment:
                player.gameObject.GetComponent<StaminaManager>().UpdateStaminaOnTakingItem(stamina);
                player.gameObject.GetComponent<movementMouse>().walkSpeed *= (1.5f);

                break;
            case TypeItem.Pain:
                player.gameObject.GetComponent<StaminaManager>().UpdateStaminaOnTakingItem(stamina);
                player.gameObject.GetComponent<movementMouse>().walkSpeed *= 0.9f;
                break;
            case TypeItem.Sucre:
                player.gameObject.GetComponent<StaminaManager>().UpdateStaminaOnTakingItem(stamina);
                break;
            default:
                break;
        }
    }
}
