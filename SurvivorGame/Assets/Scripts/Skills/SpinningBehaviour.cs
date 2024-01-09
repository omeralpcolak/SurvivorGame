using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinningBehaviour : SkillBehaviour
{
    private int damage;
    private bool canSpin = false;
    private bool isSpinning = false;
    private float rotateTime = 2f;

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
        damage = skill.skillProperty.damage;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate
        {
            canSpin = true;
        });
    }

    private void FixedUpdate()
    {
        if (!canSpin || isSpinning)
        {
            return;
        }

        Spin();
    }

    private void Spin()
    {
        isSpinning = true;
        transform.DORotate(new Vector3(0f, 360f, 0f),rotateTime)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }

}
