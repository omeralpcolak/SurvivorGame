using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeleeController : MonoBehaviour
{
    private int meleeDamage;
    private float spinningSpeed;
    private Vector3 maxScale;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(maxScale, 1f);
    }

    public void MeleeUpgrade(int newDamage, float spinningSpeed,Vector3 maxScale)
    {
        meleeDamage = newDamage;
        this.spinningSpeed = spinningSpeed;
        this.maxScale = maxScale;
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
        }
    }
}
