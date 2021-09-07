using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalShot : MonoBehaviour, ISpell
{
    public Sprite SpellIcon;
    public GameObject ArrowPrefab;
    public GameObject BowPosition;
    public float LastUseTime;
    public float Cooldown;
    // Start is called before the first frame update

    private void Start()
    {
        BowPosition = GameObject.FindGameObjectWithTag("BowPosition");
    }
    public bool ConditionsMet()
    {
        return Time.time >= LastUse() + GetCooldown() && PlayerManager.Focus > PlayerManager.GetMaxFocus() * 0.5f;
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
        GameObject arrow = Instantiate(ArrowPrefab, BowPosition.transform.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetDmg((float)Math.Ceiling(PlayerManager.Power * 2.25f));
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(8 * Aim.x, 8 * Aim.y);
        arrow.transform.Rotate(0f, 0f, Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg);
        ExpendResource();
        SetLastUse();
        Destroy(arrow, 2f);
    }

    public void tooltip()
    {
        TalentTree.ShowVitalShotTooltip();
    }

    private void ExpendResource()
    {
        PlayerManager.Focus = PlayerManager.GetCurrFocus() - PlayerManager.GetMaxFocus() * 0.5f;
        if (PlayerManager.Focus < 0)
            PlayerManager.Focus = 0;
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }
}
