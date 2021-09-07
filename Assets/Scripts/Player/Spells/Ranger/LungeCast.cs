using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungeCast : MonoBehaviour, ISpell
{
    private Vector2 LungeSpeed;
    public float InitialLungeSpeed;
    private Vector3 LungeDirection;
    private List<GameObject> TargetsHit;
    public GameObject Hitbox;
    public float DmgModifier;
    private float LastUseTime;
    public float Cooldown;
    public Sprite SpellIcon;
    private bool Active = false;


    public bool IsActive()
    {
        return Active;
    }
    void Update()
    {
        if (Active)
        {
            SlowDown();
        }
    }

    private void SlowDown()
    {
        this.GetComponentInParent<Rigidbody2D>().velocity -= LungeSpeed / 10f;
        LungeSpeed = this.GetComponentInParent<Rigidbody2D>().velocity;
        if (this.GetComponentInParent<Rigidbody2D>().velocity.magnitude < 3f)
        {
            Active = false;
            PlayerMovement.MovementAbility = false;
            this.GetComponentInParent<Collider2D>().isTrigger = false;
        }

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
        return 1;
    }

    public void Cast()
    {
        Vector3 Aim = PlayerManager.GetAim();
        LungeDirection = Aim;
        //this.GetComponentInParent<Rigidbody2D>().transform.position += InitialLungeSpeed * Aim * Time.deltaTime;
        PlayerMovement.SetMovementAbility(true);
        PlayerMovement.rb.velocity = (InitialLungeSpeed * Aim);
        this.GetComponentInParent<Collider2D>().isTrigger = true;
        LungeSpeed = PlayerMovement.rb.velocity;
        Active = true;
        GameObject hitbox = Instantiate(Hitbox, gameObject.transform.position, Quaternion.identity);
        hitbox.transform.Rotate(0f, 0f, (Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg));
        SetLastUse();
        Destroy(hitbox, 0.7f);
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    public void ResetCD()
    {
        LastUseTime = LastUseTime - GetCooldown();
    }

    public void tooltip()
    {
        TalentTree.ShowLungeTooltip();
    }
}
