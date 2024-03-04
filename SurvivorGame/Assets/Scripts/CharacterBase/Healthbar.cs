using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public Image healthbarImg;
    public Health health;
    private Camera cam;
    private float target;

    private void Start()
    {
        cam = Camera.main;
        health.OnTakeDamage += UpdateHealthBar;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    public void UpdateHealthBar()
    {
        target = health.GetHealthPercent();
        healthbarImg.fillAmount = target;
    }
}
