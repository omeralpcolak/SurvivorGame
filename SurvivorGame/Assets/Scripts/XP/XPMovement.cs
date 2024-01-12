using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class XPMovement : MonoBehaviour
{
    private GameObject player;
    private float xpMovementSpeed = 10f;
    Tween rotateTween;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rotateTween = transform.DORotate(new Vector3(360f, 360f, 360f), 1f,RotateMode.Fast)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    private void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.Translate(direction * Time.deltaTime * xpMovementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rotateTween.Kill();
            Destroy(gameObject);
        }
    }
}
