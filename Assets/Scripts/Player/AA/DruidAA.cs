using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class DruidAA : MonoBehaviour, IAutoAtt
{
    public GameObject WildBoltPrefab;
    public Animator animator;
    public GameObject BowPosition;
    Vector3 MousePos;
    Vector3 aimDirection;
    public static bool ActiveAA;
    private static bool ActivatedSprite;
    public GameObject WindowManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveAA)
        {
            HandleAiming();
        }
    }

    public void Activate()
    {
       ActiveAA = true;
    }

    public void Deactivate()
    {
        ActiveAA = false;
       
    }


    private void ShootWildBolt()
    {
        if (PlayerManager.Mana >= PlayerManager.GetMaxMana()*0.08f)
        {
            GameObject Bolt = Instantiate(WildBoltPrefab, BowPosition.transform.position, Quaternion.identity);
            Bolt.GetComponent<Rigidbody2D>().velocity = new Vector2(8 * aimDirection.x, 8 * aimDirection.y);
            Bolt.transform.Rotate(0f, 0f, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
            Destroy(Bolt, 2f);
            animator.SetBool("Shooting", false);
            PlayerManager.Mana = PlayerManager.Mana - 0.08f * PlayerManager.GetMaxMana();
        }
    }


    private void HandleAiming()
    {
        MousePos = UtilsClass.GetMouseWorldPosition();
        aimDirection = (MousePos - BowPosition.transform.position).normalized;

    }

    public void AutoAttack(float Power)
    {
        Invoke("ShootWildBolt", 0.25f);
        animator.SetBool("Shooting", true);
    }
}
