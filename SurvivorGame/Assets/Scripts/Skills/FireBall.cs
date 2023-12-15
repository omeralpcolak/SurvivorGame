using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FireBall : MonoBehaviour
{
    
    bool canMove = false;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3 (0.25f,0.25f,0.25f), 0.1f).OnComplete(delegate
        {
            canMove = true;
            transform.parent = null;
        });
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20);
        }
               
    }

}
