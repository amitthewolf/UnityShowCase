using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspCast : MonoBehaviour, ISpell
{
    public Sprite SpellIcon;
    public GameObject WaspSwarm;
    public float LastUseTime;
    public float Cooldown;

    public bool ConditionsMet()
    {
        return Time.time >= LastUse() + GetCooldown() && PlayerManager.GetCurrMana() > PlayerManager.GetMaxMana() * 0.12f;
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
        Vector3 Aim = PlayerManager.GetAim();
        GameObject Swarm = Instantiate(WaspSwarm, gameObject.transform.position, Quaternion.identity);
        Swarm.GetComponent<Rigidbody2D>().velocity = new Vector2(8 * Aim.x, 8 * Aim.y);
        Swarm.transform.Rotate(0f, 0f, Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg);
        PlayerManager.Mana = PlayerManager.Mana - PlayerManager.GetMaxMana()*0.12f;
        SetLastUse();
        Destroy(Swarm, 2f);
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    public void tooltip()
    {
        TalentTree.ShowWaspSwarmTooltip();
    }
}
