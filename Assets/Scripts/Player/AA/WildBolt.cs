using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBolt : MonoBehaviour
{

    public float Dmg;
    public GameObject Exposure;

    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerManager.SetTarget(other);
            other.GetComponent<EnemyHealth>().TakeDmg((float)Math.Ceiling(PlayerManager.Power * Dmg));
            bool Exists = other.GetComponent<DotManager>().AddDot(Exposure.GetComponent<Exposure>());
            if(!Exists)
            {
                GameObject Dot = Instantiate(Exposure);
                Dot.transform.SetParent(other.GetComponent<DotManager>().transform);
                Dot.transform.position = other.transform.position;
                other.GetComponent<DotManager>().saveDot(Dot);
            }
            Destroy(gameObject);
        }
    }
}
