using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LashvineUnit : MonoBehaviour
{
    GameObject CurrTarget;
    List<GameObject> OptionalTargets;
    private float LastAttackTime;
    public float Attackspeed;
    public float DmgModifier;

    // Start is called before the first frame update
    void Start()
    {
        OptionalTargets = new List<GameObject>();
        LastAttackTime = Time.time- Attackspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (OptionalTargets.Count> 0)
        {
            if (CurrTarget == null)
                CurrTarget = OptionalTargets[0];
        }
        if ( CurrTarget != null)
        {
            if(Time.time >= LastAttackTime + Attackspeed)
            {
                CurrTarget.GetComponent<EnemyHealth>().TakeDmg(PlayerManager.Power * DmgModifier);
                LastAttackTime = Time.time;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Enemy"))
        {
            OptionalTargets.Add(Other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("Enemy"))
        {
            OptionalTargets.Remove(Other.gameObject);
        }
        if( CurrTarget.Equals(Other))
        {
            CurrTarget = null;
        }
    }
}
