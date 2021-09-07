using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private float Penetrate;
    private float Dmg;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SetDmg(float Damage)
    {
        Dmg = Damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
           PlayerManager.SetAttackingTarget(other);
           other.GetComponent<EnemyHealth>().TakeDmg(Dmg);
           Destroy(gameObject);
        }
    }
}
