using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private GameObject Player;
    private GameObject PlayerPet;
    private GameObject ChaseTarget;
    public AudioSource audioSource;
    public bool chasing = false;
    public float AttackRange = 0.25f;
    public float AttackSpeed = 1f;
    public float targetRange = 0.5f;
    public float Damage = 1f;
    private float LastAttackTick;
    private bool Snared;



    public bool Attacking;
    private bool ChasingPet;
    private float ActiveSlow;
    private float SnareTime;

    public void Snare(float snareTime)
    {
        Snared = true;
        SnareTime = Time.time + snareTime;
    }
    // Start is called before the first frame update

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
            PlayerPet = GameObject.FindGameObjectWithTag("Pet");
        LastAttackTick = Time.time;
        Attacking = false;
        ActiveSlow = 0f;
    }

    private void FindTarget()
    {
        Attacking = false;
        Vector3 PlayerPos = Player.transform.position;
        PlayerPet = GameObject.FindGameObjectWithTag("Pet");
        if (PlayerPet != null && !PlayerPet.GetComponent<PetAI>().GetDead())
        {
            Vector3 PetPos = PlayerPet.transform.position;
            if (Vector3.Distance(transform.position, PetPos) < targetRange)
            {
                chasing = true;
                ChaseTarget = PlayerPet;
                ChasingPet = true;
            }
        }
        if (Vector3.Distance(transform.position, PlayerPos) < targetRange - PlayerManager.Stealth && ChaseTarget == null)
        {
            chasing = true;
            ChaseTarget = Player;
        }
    }


    public void StartChasePlayer()
    {
        chasing = true;
        ChaseTarget = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (ChaseTarget == null || (ChaseTarget.tag == "Pet" && PlayerPet.GetComponent<PetAI>().GetDead()))
        {
            ChaseTarget = null;
            FindTarget();
        }
        if (chasing && ChaseTarget!=null)
        {
            Vector2 TargetPos = ChaseTarget.transform.position;
            if(!Snared)
                transform.position = Vector2.MoveTowards(transform.position, TargetPos, (speed- speed*ActiveSlow) * Time.deltaTime);
            else
            {
                if (Time.time > SnareTime)
                    Snared = false;
            }
            CanAttack();
        }
        else if(Attacking)
        {
            print("Attacking");
            Vector3 TargetPos = ChaseTarget.transform.position;
            this.GetComponent<AudioSource>().Play();
            if (Vector3.Distance(transform.position, TargetPos) < AttackRange)
            {
                if(ChaseTarget.tag == "Player")
                    ChaseTarget.GetComponent<PlayerManager>().TakeDmg(Damage);
                else
                    ChaseTarget.GetComponent<PetAI>().TakeDmg(Damage);
            }
            Attacking = false;
            chasing = true;
            LastAttackTick = Time.time;
        }
    }

    private void CanAttack()
    {
        Vector3 TargetPos = ChaseTarget.transform.position;
        if (Vector3.Distance(transform.position, TargetPos) < AttackRange && (Time.time >= LastAttackTick + AttackSpeed))
        {
            Attacking = true;
            chasing = false;
        }
    }

    public void Slow(float SlowAmount)
    {
        ActiveSlow = ActiveSlow+SlowAmount;
    }
}
