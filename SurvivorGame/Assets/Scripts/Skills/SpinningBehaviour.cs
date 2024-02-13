using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinningBehaviour : SkillBehaviour
{
    private bool canSpin = false;
    private bool isSpinning = false;
    public float rotateSpeed = 50f;
    private Vector3 skillScale = new Vector3 (2, 2, 2);
    

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
    }

    public override void Upgrade()
    {
        skill.damage *= 2;
        skill.skillScale *= 1.5f;
        transform.localScale = skill.skillScale;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(skillScale, 0.5f).OnComplete(delegate
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
        transform.DORotate(new Vector3(0f, 360f, 0f), skill.rotateTime)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }

}
