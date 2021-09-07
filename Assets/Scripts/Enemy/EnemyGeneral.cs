using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyGeneral : MonoBehaviour
{
    public int ExpWorth;
    public bool ForQuest;
    public string QuestName;
    public bool spawner;
    public bool TargetedByPet;
    public GameObject TargetPoint;
    public GameObject Spawn;

    public bool Targeted { get; internal set; }
    public static event EventHandler<EnemyKilledEventArgs> EnemyKilled;

    public class EnemyKilledEventArgs : EventArgs
    {
        public int ExpWorth;
        public bool ForQuest;
        public string QuestName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Die()
    {
        EnemyKilled?.Invoke(this, new EnemyKilledEventArgs { ExpWorth = this.ExpWorth, ForQuest = this.ForQuest, QuestName = this.QuestName }) ;
        if (spawner)
        {
            GameObject Spawned1 = Instantiate(Spawn, transform.position, Quaternion.identity);
            GameObject Spawned2 = Instantiate(Spawn, transform.position, Quaternion.identity);
            Spawned1.GetComponent<EnemyAI>().StartChasePlayer();
            Spawned2.GetComponent<EnemyAI>().StartChasePlayer();
        }
        if(TargetedByPet)
            PlayerManager.ResetAttackingTarget();
        if (Targeted)
            PlayerManager.ResetTarget();
        Debug.Log("destroying");
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        PlayerManager.SetTarget(GetComponent<Collider2D>());
    }

    private void OnMouseEnter()
    {
        this.GetComponentInChildren<Light2D>().enabled = true;
    }

    private void OnMouseExit()
    {
        this.GetComponentInChildren<Light2D>().enabled = false;
    }
}
