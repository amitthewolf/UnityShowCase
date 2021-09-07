using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemyAI : MonoBehaviour
{
    public float LastAttack { get; set; }
    public float AttackSpeed;
    public float AttackPause;
    public float SightRadius = 10f;
    public float AttackDmg;
    public float WanderRadius;
    private float LastWander;
    public float WanderTimer;
    private float OGSpeed;
    public Vector3 OGPosition;
    public GameObject Sprite;
    public GameObject AttackRoot;
    private bool FaceLeft;
    private bool needSwitch;
    public bool OGFacingLeft = true;
    public float range;
    private bool chasing;

    private bool OGpositionSet;
    private float SnareOver;
    private bool Snared;
    Transform Target;
    NavMeshAgent agent;
    float Scale;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        Target = PlayerManager.instance.Player.transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        chasing = false;
        LastAttack = Time.time - AttackSpeed;
        OGSpeed = agent.speed;
        OGpositionSet = false;
        FaceLeft = OGFacingLeft;
    }

    public void StartChasing()
    {
        chasing = true;
        agent.speed = OGSpeed;
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
                    if (Time.time >= LastAttack + AttackSpeed)
                    {
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
        AttackRoot.GetComponent<EnemyRoot>().EnableAttack();
        LastAttack = Time.time;
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
