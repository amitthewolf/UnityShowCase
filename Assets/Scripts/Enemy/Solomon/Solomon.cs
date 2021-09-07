using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Solomon : MonoBehaviour
{
    public float LastAttack { get; set; }
    public float AttackSpeed;
    public float AttackPause;
    public float SightRadius = 10f;
    public float AttackDmg;
    public float WanderRadius;
    private float LastWander;
    public float WanderTimer;
    public float OGSpeed;
    public Vector3 OGPosition;
    public GameObject Sprite;
    private bool FaceLeft;
    private bool needSwitch;
    public bool OGFacingLeft = true;
    public float range;
    private bool chasing;
    public GameObject Daku;
    public GameObject PomaKoma;
    private bool OGpositionSet;
    private float SnareOver;
    private bool Snared;
    Transform Target;
    public NavMeshAgent agent;
    private int WeaveCounter;
    float Scale;

    void Start()
    {
        Target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        chasing = false;
        LastAttack = Time.time - AttackSpeed;
        OGSpeed = agent.speed;
        OGpositionSet = false;
        FaceLeft = OGFacingLeft;
        WeaveCounter = 1;
    }

    public void StartChasing()
    {
        chasing = true;
        agent.speed = OGSpeed;
        Daku.GetComponent<Totem>().Wakeup();
    }
    public void SetOGPosition()
    {
        OGPosition = transform.position;
        OGpositionSet = true;
    }


    public float GetOriginalSpeed()
    {
        return OGSpeed;
    }

    public void SpeedChange(float speedToAdd)
    {
        agent.speed = agent.speed - speedToAdd;
    }

    public void Snare(float Snaretime)
    {
        Snared = true;
        SnareOver = Time.time + Snaretime;
        agent.isStopped = true;
    }

    void Update()
    {
        Facing();
        if (!Snared)
        {
            float distance = Vector2.Distance(Target.position, transform.position);
            if (chasing)
            {
                if (distance < range)
                {
                    agent.isStopped = true;
                    float temptime = Time.time;
                    float NextAttack = LastAttack + AttackSpeed;
                    if (temptime >= NextAttack)
                    {
                        Debug.Log("SolomonAttack - "+ Time.time);
                        Attack();
                    }
                }
                else
                {
                    agent.isStopped = false;
                    agent.SetDestination(Target.position);
                }
            }
            else if (distance <= SightRadius)
            {
                chasing = true;
                agent.speed = OGSpeed;
            }
            else
            {
                agent.speed = 1.5f;
                Wander();
            }
        }
        else if (Time.time > SnareOver)
        {
            agent.isStopped = false;
            Snared = false;
        }

    }

    private void Wander()
    {
        LastWander += Time.deltaTime;
        if (LastWander > WanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, WanderRadius, -1);
            agent.SetDestination(newPos);
            LastWander = 0;
        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        while (OGpositionSet && Vector3.Distance(navHit.position, OGPosition) > WanderRadius)
        {
            randDirection = Random.insideUnitSphere * dist;
            randDirection += origin;
            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        }
        return navHit.position;
    }




    private void Attack()
    {
        agent.isStopped = true;
        Snared = true;
        SnareOver = Time.time + AttackPause;
        if (WeaveCounter == 1)
            PomaKoma.GetComponent<Totem>().Cast();
        else if(WeaveCounter == 2)
            Daku.GetComponent<Totem>().Cast();
        LastAttack = Time.time;
        Debug.Log("Next Attack - " + (Time.time + AttackSpeed));
        WeaveCounter++;
        if (WeaveCounter == 3)
            WeaveCounter = 1;
    }


    private void Facing()
    {
        if (needSwitch)
        {
            Sprite.transform.localScale = Vector3.Scale(Sprite.transform.localScale, new Vector3(-1, 1, 1));
            needSwitch = false;
        }

        if (agent.velocity.x < 0 && !FaceLeft)
        {
            needSwitch = true;
            FaceLeft = true;
        }

        if (agent.velocity.x > 0 && FaceLeft)
        {
            needSwitch = true;
            FaceLeft = false;
        }
    }
}
