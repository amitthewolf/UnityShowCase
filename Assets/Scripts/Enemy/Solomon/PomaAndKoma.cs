using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PomaAndKoma : MonoBehaviour, Totem
{
    public GameObject SolomonSpot;
    public GameObject Totem;
    public float DmgDelta;
    public float Dmg;
    private float LastAttack;
    private bool Interval;
    private bool PlayerInRange;
    private bool Active;
    private bool AtDest;
    public ParticleSystem PS;
    public ParticleSystem PSTeleport;
    private Transform Target;
    private NavMeshAgent agent;
    private int SmashCounter;
    public Vector3 SpeedDown;

    // Start is called before the first frame update
    void Start()
    {
        Target = PlayerManager.instance.Player.transform;
        PlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active && !AtDest)
        {
            if (!(Totem.transform.localPosition.y <= 2.5))
            {
                Totem.transform.localPosition -= SpeedDown;
            }
            else
            {
                ReachedDest();
            }
        }
        if (AtDest && Active)
        {
            if (PlayerInRange)
            {
                PlayerManager.instance.TakeDmg(Dmg);
               
            }
            AtDest = false;
            Active = false;
            Interval = true;
            SmashCounter++;
            LastAttack = Time.time;
        }
        if (SmashCounter == 2)
        {
            AtDest = false;
            Active = false;
            Interval = false;
            SmashCounter = 0;
            Invoke("StopCast", 2f);
        }
        if (Interval && Time.time >= LastAttack + DmgDelta)
        {
            TeleToPlayer();
            Interval = false;
        }
    }

    private void ReachedDest()
    {
        PlayPS();
        AtDest = true;
    }

    private void PlayPS()
    {
        
        PS.Play();
    }

    public void Wakeup()
    {
        StopCast();
    }
    private void TeleToPlayer()
    {
        transform.position = PlayerManager.instance.transform.position + (new Vector3(0, 0));
        Totem.transform.localPosition += new Vector3(0, 9);
        PSTeleport.Play();
        Active = true;
        AtDest = false;
    }

    public void Cast()
    {
        SmashCounter = 0;
        TeleToPlayer();
        transform.parent = null;
    }

    private void StopCast()
    {
        Active = false;
        Interval = false;
        AtDest = false;
        transform.parent = SolomonSpot.transform;
        transform.localPosition = Vector3.zero;
        Totem.transform.localPosition += new Vector3(0, 9);
        PSTeleport.Play();
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
