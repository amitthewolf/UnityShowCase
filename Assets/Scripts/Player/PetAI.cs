using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class PetAI : MonoBehaviour
{
    public float speed = 0.4f;
    public float Health;
    public float MaxHealth;
    private Collider2D target;
    private GameObject BeastMaster;
    public GameObject FloatingText;
    private bool Following = true;
    private bool chasing = false;
    public float AttackRange = 0.25f;
    public float AttackSpeed = 1f;
    public float FollowRange = 25f;
    public float Damage = 1f;
    public bool Bite;
    public Image HealthBar;
    private float LastAttack;
    private bool Attacking;
    private float ActiveSlow;
    public static bool dead;
    private float TimeofDeath;

    // Start is called before the first frame update

    void Start()
    {
        BeastMaster = GameObject.FindGameObjectWithTag("Player");
        LastAttack = Time.time;
        Attacking = false;
        ActiveSlow = 0f;
        dead = false;
    }

    public bool GetDead()
    {
        return dead;
    }

    public void SetTarget(GameObject Target)
    {
        target = PlayerManager.getTarget();
        Following = false;
        Attacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = Health / MaxHealth;
        if(!dead)
        {
            if (Following)
            {
                Vector2 PlayerPos = BeastMaster.transform.position;
                float Dis = Vector3.Distance(transform.position, PlayerPos);
                if (Vector3.Distance(transform.position, PlayerPos) > FollowRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector3(PlayerPos.x + 0.1f, PlayerPos.y - 0.4f), (speed - ActiveSlow) * Time.deltaTime);
                }
                target = PlayerManager.getAttackingTarget();
                if (target != null)
                {
                    Following = false;
                    chasing = true;
                }
            }
            else if (chasing)
            {
                CanAttack();
                if (target != null)
                {
                    Vector2 TargetPos = target.transform.position;
                    transform.position = Vector2.MoveTowards(transform.position, TargetPos, (speed - ActiveSlow) * Time.deltaTime);
                }
                else
                {
                    Following = true;
                    chasing = false;
                }
            }
            else if (Attacking)
            {
                if (target != null)
                {
                    Vector3 TargetPos = target.transform.position;
                    if (Vector3.Distance(transform.position, TargetPos) < AttackRange && Time.time >= LastAttack + AttackSpeed)
                    {
                        if (Bite)
                        {
                            target.GetComponent<EnemyHealth>().TakeDmg(PlayerManager.Power);
                            PlayerManager.AddRage(12f);
                            Bite = false;
                        }
                        target.GetComponent<EnemyHealth>().TakeDmg((float)Math.Ceiling(PlayerManager.Power * 0.75f));
                        LastAttack = Time.time;
                        PlayerManager.AddRage(5f);
                    }
                    else
                    {
                        chasing = true;
                        Attacking = false;
                    }
                }
                else
                {
                    Following = true;
                    chasing = false;
                    Attacking = false;
                }
            }
        }
        else
        {
            if (Time.time >= TimeofDeath + 10f)
            {
                Health = MaxHealth;
                dead = false;
            }
        }
    }

    public void CastBite(Collider2D Target)
    {
        print("pet = "+Target.transform.name);
        target = Target;
        Attacking = true;
        Bite = true;
        Following = false;
    }

    private void CanAttack()
    {
        if(target != null)
        {
            Vector3 TargetPos = target.transform.position;
            if (Vector3.Distance(transform.position, TargetPos) < AttackRange && (Time.time >= LastAttack + AttackSpeed))
            {
                Attacking = true;
                chasing = false;
                Following = false;
            }
        }
        else
        {
            Attacking = false;
            chasing = false;
            Following = true;
        }
       
    }

    public void Slow(float SlowAmount)
    {
        ActiveSlow = SlowAmount;
    }

    public void TakeDmg(float Damage)
    {
        print("pet Taking damage = " + Damage);
        GameObject DmgText = Instantiate(FloatingText, transform.position, Quaternion.identity);
        DmgText.transform.GetChild(0).GetComponent<TextMesh>().text = Damage.ToString();
        Health = Health - Damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        TimeofDeath = Time.time;
    }
}
