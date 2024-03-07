using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinController : PickupItemController
{
    private Tween rotateTween;
    

    private void Start()
    {
        rotateTween = transform.DORotate(new Vector3(0f, 360f, 0f), 2f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
    }


    public override void OnTriggeringWithThePlayer()
    {
        rotateTween.Kill();
        GameSessionManager.instance.AddAndUpdateInGameCoinValue();
        Instantiate(itemEffect, transform.position,Quaternion.identity);
        base.OnTriggeringWithThePlayer();
    }

}
