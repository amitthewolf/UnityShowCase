using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;

public class SpearThrow : MonoBehaviour,ISpell
{
    public Sprite SpellIcon;
    public GameObject ThrownSpear;
    public float LastUseTime;
    public float Cooldown;

    public bool ConditionsMet()
    {
       return Time.time >= LastUse() + GetCooldown() && PlayerManager.GetSpears() >= 1;
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
        return  (GetCooldown()- (Time.time - LastUse()))/ GetCooldown();
    }

    public int Subclass()
    {
        return 1;
    }

    public void Cast()
    {
        Vector3 Aim = PlayerManager.GetAim();
        GameObject spear = Instantiate(ThrownSpear, gameObject.transform.position, Quaternion.identity);
        spear.GetComponent<ThrownSpear>().SetDmg((float)Math.Ceiling(PlayerManager.Power*1.75f));
        spear.GetComponent<ThrownSpear>().SetDegree(Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg);
        spear.GetComponent<Rigidbody2D>().velocity = new Vector2(8 * Aim.x, 8 * Aim.y);
        spear.transform.Rotate(0f, 0f, Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg);
        PlayerManager.RemoveSpear();
        SetLastUse();
        Destroy(spear, 0.5f);
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    public void tooltip()
    {
        TalentTree.ShowSpearThrowTooltip();
    }
}
