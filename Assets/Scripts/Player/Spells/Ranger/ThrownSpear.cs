using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownSpear : MonoBehaviour
{
    private float LastUseTime;
    public float Cooldown;
    private float Dmg;
    private float Degree;
    public GameObject CollectableSpear;
    public GameObject Player;
    public void SetDmg(float Damage)
    {
        Dmg = Damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerManager.SetTarget(other);
            other.GetComponent<EnemyHealth>().TakeDmg(Dmg);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject collectableSpear = Instantiate(CollectableSpear, transform.position, Quaternion.identity);
        collectableSpear.transform.Rotate(0f, 0f, Degree);
    }

    internal void SetDegree(float degree)
    {
        Degree = degree;
    }
}
