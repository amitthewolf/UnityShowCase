using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class SpearThrust : MonoBehaviour, IAutoAtt
{
    Vector3 MousePos;
    Vector3 aimDirection;
    public GameObject Spearthrow;
    public GameObject SpearRange;
    public List<Collider2D> Targets;
    public GameObject Spear;
    private bool facingRight = true;
    public bool ActiveAA;
    private bool ActivatedSprite;
    // Start is called before the first frame update
    private void Start()
    {
        Targets = new List<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        if (ActiveAA)
        {
            if (!ActivatedSprite)
            {
                Spear.transform.GetComponent<SpriteRenderer>().enabled = true;
                ActivatedSprite = true;
            }
            HandleThrusts();
        }
        else
            Spear.transform.GetComponent<SpriteRenderer>().enabled = false;
    }



    public  void Activate()
    {
        ActiveAA = true;
        ActivatedSprite = false;
    }

    public  void Deactivate()
    {
        ActiveAA = false;
    }


    public void ActivateSprite()
    {
        Spear.transform.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void ChangeToFalse()
    {
        Spear.GetComponent<Animator>().SetBool("Attacking", false);
    }

    private void HandleThrusts()
    {
        if (PlayerMovement.facingRight & !facingRight)
        {
            Flip();
        }
        else if(!PlayerMovement.facingRight & facingRight)
        {
            Flip();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)- player.transform.position;
        float NewAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(NewAngle, Vector3.forward);
        SpearRange.transform.rotation = Quaternion.Slerp(SpearRange.transform.rotation, rotation, 10f * Time.deltaTime);
    }

    private void HandleAiming()
    {
        MousePos = UtilsClass.GetMouseWorldPosition();
        aimDirection = (MousePos - gameObject.transform.position).normalized;

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Targets.Add(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Targets.Remove(other);
    }

    public void AutoAttack(float Power)
    {
        if (PlayerManager.GetSpears()>0)
        {
            Spear.GetComponent<Animator>().SetBool("Attacking", true);
            if (Targets.Count > 0)
                for (int i = 0; i < Targets.Count; i++)
                {
                    //PlayerManager.SetAttackingTarget(Targets[i]);
                    Targets[i].GetComponent<EnemyHealth>().TakeDmg((float)Math.Ceiling(Power * 1.20f));
                }
            Invoke("ChangeToFalse", 0.2f);
            //Invoke("ClearTargets",0.5f);
        }
    }

    private void ClearTargets()
    {
        Targets = new List<Collider2D>();
    }

   
}
