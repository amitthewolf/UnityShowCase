using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Daku : MonoBehaviour, Totem
{
    public GameObject SolomonSpot;
    public float DmgDelta;
    public float Dmg;
    private float LastAttack;
    private float LastDmgTick;
    private bool PlayerInRange;
    private bool Active;
    private bool AtDest;
    private bool SolomonUp;
    private ParticleSystem PS;
    private Transform Target;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        Target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        PS = GetComponentInChildren<ParticleSystem>();
        PlayerInRange = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SolomonUp && !Active && !AtDest && SolomonSpot!=null)
        {
            agent.SetDestination(SolomonSpot.transform.position);
        }
        if (Active && !AtDest)
        {
            agent.SetDestination(Target.position);
            ReachedDest();
        }
        if (AtDest && Active)
        {
            if (Active && PlayerInRange && Time.time >= LastDmgTick + DmgDelta)
            {
                PlayerManager.instance.TakeDmg(Dmg);
                LastDmgTick = Time.time;
            }
        }

        if (Time.time >= LastAttack + 2f && AtDest)
        {
            Active = false;
            AtDest = false;
        }
    }


    public void Wakeup()
    {
        SolomonUp = true;
    }
    private void ReachedDest()
    {
        float dist = agent.remainingDistance;
        if (agent.remainingDistance <= 1f)
        {
            LastAttack = Time.time;
            PlayPS();
            AtDest = true;
        }
    }

    private void PlayPS()
    {
        LastAttack = Time.time;
        PS.Play();
    }

    public void Cast()
    {
        agent.SetDestination(Target.position);
        Active = true;
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
