using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesBulwarkCaster : MonoBehaviour,ISpell
{
    public Sprite SpellIcon;
    public GameObject NaturesBulwark;
    public float LastUseTime;
    public float Cooldown;

    public bool ConditionsMet()
    {
        return Time.time >= LastUse() + GetCooldown() && PlayerManager.GetCurrMana() > PlayerManager.GetMaxMana()*0.12f;
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
        GameObject NB = Instantiate(NaturesBulwark, gameObject.transform.position, Quaternion.identity);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<HotManager>().AddHot(NB.GetComponent<NaturesBulwark>());
        PlayerManager.Mana = PlayerManager.Mana - PlayerManager.GetMaxMana() * 0.12f;
        SetLastUse();
    }


    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    public void tooltip()
    {
       TalentTree.ShowNaturesBulwarkTooltip();
    }
}
