using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebspitterProjectile : MonoBehaviour
{
    public float Dmg;

    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().TakeDmg(Dmg);
            Destroy(gameObject);
        }
    }
}
