using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class XPMovement : MonoBehaviour
{
    private GameObject player;
    private float xpMovementSpeed = 30f;
    private float upwardsForce = 5f;
    private Rigidbody rb;
    Tween rotateTween;
    private bool moveToThePlayer = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        rotateTween = transform.DORotate(new Vector3(360f, 360f, 360f), 1f,RotateMode.Fast)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        StartCoroutine(MoveUpwards());
    }

    IEnumerator MoveUpwards()
    {
        rb.AddForce(Vector3.up * upwardsForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        moveToThePlayer = true;
    }

    private void FixedUpdate()
    {
        if (!moveToThePlayer)
        {
            return;
        }
        Vector3 direction = player.transform.position - transform.position;

        transform.Translate(direction.normalized * Time.deltaTime * xpMovementSpeed);
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
