using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public Image healthbarImg;
    private Camera cam;
    private float target = 1f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthbarImg.fillAmount = Mathf.MoveTowards(healthbarImg.fillAmount, target,0.8f*Time.deltaTime);
    }

    public void UpdateHealthBar(int maxHealth, int currentHealth)
    {
        target = currentHealth / maxHealth;
        
    }
}
