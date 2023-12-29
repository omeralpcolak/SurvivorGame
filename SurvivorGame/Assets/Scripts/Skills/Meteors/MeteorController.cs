using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private Vector3 maxScale;
    private int meteorDamage;
    private float timePerDamage;
    private Dictionary<Collider, Coroutine> damageCoroutines = new Dictionary<Collider, Coroutine>();

    private void Start()
    {
        transform.localScale = maxScale;
        StartCoroutine(DestroyMeteor());
    }

    public void MeteorUpgrade(Vector3 newScale, int newDamage, float timePerDamage)
    {
        maxScale = newScale;
        meteorDamage = newDamage;
        this.timePerDamage = timePerDamage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !damageCoroutines.ContainsKey(other))
        {
            Coroutine newDamageRoutine = StartCoroutine(DamageOverTime(other));
            damageCoroutines[other] = newDamageRoutine;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && damageCoroutines.ContainsKey(other))
        {
            StopCoroutine(damageCoroutines[other]);
            damageCoroutines.Remove(other);
        }
    }

    IEnumerator DamageOverTime(Collider enemy)
    {
        while (enemy != null && enemy.gameObject.CompareTag("Enemy"))
        {
            enemy.GetComponent<Health>().TakeDamage(meteorDamage);
            Debug.Log($"Dealing {meteorDamage} damage to {enemy.name}");
            yield return new WaitForSeconds(timePerDamage);
        }
        if (damageCoroutines.ContainsKey(enemy))
        {
            damageCoroutines.Remove(enemy);
        }
    }

    IEnumerator DestroyMeteor()
    {
        yield return new WaitForSeconds(5f);
        foreach (var routine in damageCoroutines.Values)
        {
            StopCoroutine(routine);
        }
        Destroy(gameObject);
    }
}
