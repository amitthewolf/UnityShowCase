using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour, ISpell
{
    public Sprite SpellIcon;
    private GameObject Pet;
    public float LastUseTime;
    public float Cooldown;
    // Start is called before the first frame update

    private void Update()
    {
        Pet = GameObject.FindGameObjectWithTag("Pet");
    }

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
        return 4;
    }

    public void Cast()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            print(hit.transform.name);
            SetLastUse();
            Pet.GetComponent<PetAI>().CastBite(hit.collider);
        }
    }

    public void tooltip()
    {
        TalentTree.ShowBiteTooltip();
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }
}
