using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemManager : MonoBehaviour
{
    public LayerMask itemLayer;


    // Update is called once per frame
    void Update()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, 5, itemLayer);

        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log(1);
            items[i].GetComponent<Items>().player = gameObject.transform;
            items[i].GetComponent<Items>().isOnRange = true;
        }
    }
}
