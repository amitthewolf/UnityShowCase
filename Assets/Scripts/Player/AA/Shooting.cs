using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class Shooting : MonoBehaviour, IAutoAtt
{
    public GameObject ArrowPrefab;
    public Animator animator;
    public GameObject BowPosition;
    Vector3 MousePos;
    Vector3 aimDirection;
    public static bool ActiveAA;
    private static bool ActivatedSprite;
    public GameObject WindowManager;

    public void Activate()
    {
        ActiveAA = true;
    }

    public void Deactivate()
    {
        ActiveAA = false;
    }

    private void ShootArrow()
    {
        aimDirection = PlayerManager.aimDirection;
        GameObject arrow = Instantiate(ArrowPrefab, BowPosition.transform.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetDmg(CalcDmg());
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(8*aimDirection.x, 8 * aimDirection.y);
        arrow.transform.Rotate(0f, 0f, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
        Destroy(arrow, 2f);
        animator.SetBool("Shooting", false);
    }

    private float CalcDmg()
    {
        float damage = (float)Math.Ceiling(PlayerManager.GetCurrFocus() * PlayerManager.GetCurrFocus() * PlayerManager.Power * 1.75f);
        PlayerManager.Focus = PlayerManager.GetCurrFocus() - PlayerManager.GetMaxFocus() * 0.4f;
        if (PlayerManager.Focus < 0)
            PlayerManager.Focus = 0;
        return damage;
    }

    public void AutoAttack(float Power)
    {
        Invoke("ShootArrow", 0.25f);
        animator.SetBool("Shooting", true);
    }
}
