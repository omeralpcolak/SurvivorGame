using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : PlayerAttacks
{
    public FireBall fireBallPrefab;
    public Transform fireBallSpawnPoint;

    public override void Attack()
    {
        FireBall fireBall = Instantiate(fireBallPrefab,fireBallSpawnPoint.position,fireBallSpawnPoint.transform.rotation);
    }
}
