using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapCaster : MonoBehaviour, ISpell
{
    public Sprite SpellIcon;
    public GameObject BearTrap;
    public float LastUseTime;
    public float Cooldown;

    public bool ConditionsMet()
    {
        return Time.time >= LastUse() + GetCooldown();
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
        return 1;
    }

    public void Cast()
    {
        Vector3 Aim = PlayerManager.GetAim();
        GameObject bearTrap = Instantiate(BearTrap, gameObject.transform.position, Quaternion.identity);
        SetLastUse();
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    public void tooltip()
    {
        TalentTree.ShowBearTrapTooltip();
    }
}
