using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CripplingShotCast : MonoBehaviour, ISpell
{
    public Sprite SpellIcon;
    public GameObject CrippleShot;
    public float LastUseTime;
    public float Cooldown;

    public bool ConditionsMet()
    {
        return Time.time >= LastUse() + GetCooldown() && PlayerManager.Focus > PlayerManager.GetMaxFocus() * 0.2f;
    }

    public float GetCooldown()
    {
        return Cooldown;
    }

    public void SetLastUse()
    {
        LastUseTime = Time.time;
    }

    public float LastUse()
    {
        return LastUseTime;
    }

    public float CooldownState()
    {
        return (GetCooldown() - (Time.time - LastUse())) / GetCooldown();
    }

    public int Subclass()
    {
        return 3;
    }

    public void Cast()
    {
        Vector3 Aim = PlayerManager.GetAim();
        GameObject CShot = Instantiate(CrippleShot, gameObject.transform.position, Quaternion.identity);
        CShot.GetComponent<Rigidbody2D>().velocity = new Vector2(8 * Aim.x, 8 * Aim.y);
        CShot.transform.Rotate(0f, 0f, Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg);
        ExpendResource();
        SetLastUse();
        Destroy(CShot, 1.4f);
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    private void ExpendResource()
    {
        PlayerManager.Focus = PlayerManager.GetCurrFocus() - PlayerManager.GetMaxFocus() * 0.2f;
        if (PlayerManager.Focus < 0)
            PlayerManager.Focus = 0;
    }

    public void tooltip()
    {
        TalentTree.ShowCripplingShotTooltip();
    }
}
