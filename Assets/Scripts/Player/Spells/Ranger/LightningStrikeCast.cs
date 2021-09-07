using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeCast : MonoBehaviour, ISpell
{
    public Sprite SpellIcon;
    public GameObject LightningStrike;
    public float LastUseTime;
    public float Cooldown;
    public float Modifier;

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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            GameObject LightningStrikeMain = Instantiate(LightningStrike, hit.collider.gameObject.transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
            hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDmg(PlayerManager.Power * Modifier);
            PlayerManager.Mana = PlayerManager.Mana - PlayerManager.GetMaxMana() * 0.16f;
            SetLastUse();
            Destroy(LightningStrikeMain, 2f);
        }
    }

    public Sprite GetSprite()
    {
        return SpellIcon;
    }

    public void tooltip()
    {
        TalentTree.ShowLightningStrikeTooltip();
    }
}
