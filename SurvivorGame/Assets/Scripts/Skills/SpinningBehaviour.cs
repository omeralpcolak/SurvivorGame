using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinningBehaviour : SkillBehaviour
{
    private bool canSpin = false;
    private bool isSpinning = false;
    private float rotateTime = 2f;
    private Vector3 skillScale = new Vector3 (2, 2, 2);
    

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
    }

    public override void Upgrade()
    {
        skill.skillScale *= 1.5f;
        transform.DOScale(skill.skillScale, 0.5f);
        skill.damage *= 2;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(skillScale, 0.5f).OnComplete(delegate
        {
            Debug.Log(transform.localScale);
            canSpin = true;
        });
    }

    private void FixedUpdate()
    {
        if (!canSpin || isSpinning)
        {
            return;
        }

        //Spin();
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
            other.GetComponent<Health>().TakeDamage(skill.damage);
        }
    }

}
