using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickupItemController : MonoBehaviour
{
    public GameObject itemEffect;

    public virtual void Movement()
    {

    }

    public virtual void OnTriggeringWithThePlayer()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTriggeringWithThePlayer();
        }
    }

    

}
