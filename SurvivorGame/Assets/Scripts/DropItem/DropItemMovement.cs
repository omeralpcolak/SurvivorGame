using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropItemMovement : MonoBehaviour
{
    private GameObject player;
    private float xpMovementSpeed = 30f;
    private float upwardsForce = 5f;
    private Rigidbody rb;
    private bool moveToThePlayer = false;

    private Tween rotateTween;

    public bool isItXP;
    public GameObject coinPickUpEffect;
    public GameObject xpPickUpEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (isItXP)
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(MoveUpwards());
        }

        if (!isItXP)
        {
            rotateTween = transform.DORotate(new Vector3(0f, 360f, 0f), 2f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
    }

    IEnumerator MoveUpwards()
    {
        rb.AddForce(Vector3.up * upwardsForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        moveToThePlayer = true;
    }

    private void FixedUpdate()
    {
        if (moveToThePlayer && isItXP)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * xpMovementSpeed);
        }
    }

    private void InstantiateEffect()
    {
        GameObject effectPrefab = isItXP ? xpPickUpEffect : coinPickUpEffect;

        if (isItXP)
        {
            Instantiate(effectPrefab, player.transform);
        }
        else
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            InstantiateEffect();
            if (!isItXP)
            {
                rotateTween.Kill();
                GameSessionManager.instance.coin++;
            }
            Destroy(gameObject);
        }
    }
}
