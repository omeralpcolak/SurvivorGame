using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeleeController : MonoBehaviour
{
    private int meleeDamage;
    private float spinningSpeed;


    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
    }

    public void MeleeUpgrade(int newDamage, float spinningSpeed)
    {
        meleeDamage = newDamage;
        this.spinningSpeed = spinningSpeed;
    }

    private void FixedUpdate()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 10 / spinningSpeed, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(meleeDamage);
            Debug.Log(meleeDamage);
        }
    }
}
