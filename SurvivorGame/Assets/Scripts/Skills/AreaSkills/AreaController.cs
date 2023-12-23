using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    private Vector3 maxScale;
    private int areaDamage;

    private void Start()
    {
        transform.localScale = maxScale;
    }

    public void AreaUpgrade(Vector3 newScale, int newDamage)
    {
        maxScale = newScale;
        areaDamage = newDamage;
    }
}
