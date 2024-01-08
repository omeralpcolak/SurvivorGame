using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinningBehaviour : SkillBehaviour
{
    private int damage;
    private float rotateSpeed = 1f;

    public override void Init(Skill _skill, SkillProperty _skillProperty)
    {
        base.Init(_skill, _skillProperty);
        damage = skill.skillProperty.damage;
    }

    private void FixedUpdate()
    {
        Spin();
    }

    private void Spin()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 2f)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);

    }

}
