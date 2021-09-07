using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LungeHitbox : MonoBehaviour
{
    private List<GameObject> OptionalTargets;
    public float DmgModifier;

    // Start is called before the first frame update
    void Start()
    {
        OptionalTargets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OptionalTargets.Count > 0)
        {
            Debug.Log("damaging enemies");
            damageEnemies();
        }
    }

    private void damageEnemies()
    {
        OptionalTargets.Where(x => x.CompareTag("Enemy")).ToList().ForEach(x =>
        {
            x.GetComponent<EnemyHealth>().TakeDmg((float)Math.Floor(PlayerManager.Power * DmgModifier));
        });
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Enemy"))
        {
            OptionalTargets.Add(Other.gameObject);
        }
    }

}
