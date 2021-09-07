using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LashvineCast : MonoBehaviour, ISpell
{
    public Sprite SpellIcon;
    public GameObject Lashvine;
    public float LastUseTime;
    public float Cooldown;

    public bool ConditionsMet()
    {
        return Time.time >= LastUse() + GetCooldown() && PlayerManager.GetCurrMana() > PlayerManager.GetMaxMana() * 0.16f;
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
        return 2;
    }

    public void Cast()
    {
        Vector3 MousePos = UtilsClass.GetMouseWorldPosition();
        GameObject LashvineGO = Instantiate(Lashvine, MousePos, Quaternion.identity);
        PlayerManager.Mana = PlayerManager.Mana - PlayerManager.GetMaxMana() * 0.16f;
        SetLastUse();
        Destroy(LashvineGO, 10f);
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    public void tooltip()
    {
        TalentTree.ShowLashvineTooltip();
    }
}
