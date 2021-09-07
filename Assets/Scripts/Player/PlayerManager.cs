using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;
using System;

public class PlayerManager : MonoBehaviour
{
    #region GeneralStats
    public static int Level;
    public static int Power;
    public static float Exp;
    public static int TalentPoints;
    private static float ToLevel;
    public static float Stealth;
    private float Health;
    public float MaxHealth;
    public Image HealthBar;
    public Image ExpBar;
    public GameObject FloatingText;
    public GameObject LevelUpText;
    private static Collider2D Target;
    private static Collider2D AttackingTarget;
    private float RegenTick = 0.1f;
    private float LastTick;
    public static float DamageResistance;
    #endregion

    #region Resources
    private static bool FocusOn;
    private static bool ManaOn;
    private static bool RageOn;
    private static bool SpearsOn;
    public static float Focus;
    private static float MaxFocus = 1;
    public static float Mana;
    private static float MaxMana = 100;
    public static float Rage = 0;
    private static float MaxRage = 100;
    public static int spears = 0;
    private static int MaxSpears = 2;
    #endregion

    public static PlayerManager instance;

    public GameObject Player;
    public static GameObject WM;
    public static Vector3 MousePos;
    public static Vector3 aimDirection;

    private void Awake()
    {
        instance = this;
    }

    public void CheckText()
    {
        Debug.Log("CheckTest Called");
    }

    // Start is called before the first frame update
    void Start()
    {
        TalentPoints = 1;
        FocusOn = false;
        Power = 3;
        Level = 1;
        Exp = 0;
        ToLevel = 30;
        Health = MaxHealth;
        Stealth = 0f;
        Focus = MaxFocus;
        Mana = MaxMana;
        WM = GameObject.FindGameObjectWithTag("GM");
    }

    public static void AddDmgResistance(float AddDmgResistance)
    {
        DamageResistance = DamageResistance + AddDmgResistance;
    }

    public static void RemoveDmgResistance(float AddDmgResistance)
    {
        DamageResistance = DamageResistance - AddDmgResistance;
        if (DamageResistance < 0)
            DamageResistance = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time > LastTick + RegenTick)
            BothRegenTick();
        HealthBar.fillAmount = Health / MaxHealth;
        HandleAiming();
    }

    #region Targeting
    public static Collider2D getTarget()
    {
        return Target;
    }


    internal static Collider2D getAttackingTarget()
    {
        return AttackingTarget;
    }

    internal static void ResetTargets()
    {
        AttackingTarget = null;
        Target = null;
    }

    internal static void ResetTarget()
    {
        Target = null;
    }

    internal static void ResetAttackingTarget()
    {
        AttackingTarget = null;
    }


    public static void SetAttackingTarget(Collider2D target)
    {
        if (AttackingTarget != null)
        {
            AttackingTarget.GetComponent<EnemyGeneral>().TargetedByPet = false;
        }
        AttackingTarget = target;
        AttackingTarget.GetComponent<EnemyGeneral>().TargetedByPet = true;
    }

    public static void SetTarget(Collider2D target)
    {
        if (Target != null)
        {
            target.GetComponent<EnemyGeneral>().Targeted = false;
        }
        Target = target;
        target.GetComponent<EnemyGeneral>().Targeted = true;
    }


    #endregion

    #region regen
    private void BothRegenTick()
    {
        LastTick = Time.time;
        if (Health < MaxHealth)
            Health = Health +0.05f;
        if (Health > MaxHealth)
            Health = MaxHealth;
        ResourceTick();
    }

    private void ResourceTick()
    {
        if(FocusOn)
        {
            if (Focus < MaxFocus)
                Focus = Focus + 0.01f;
            if (Focus > MaxFocus)
                Focus = MaxFocus;
        }
        if(ManaOn)
        {
            if (Mana < MaxMana)
                Mana = Mana + 0.25f;
            if (Focus > MaxMana)
                Mana = MaxMana;
        }
    }

    public void GainExp(int ExpGained)
    {
        Exp += ExpGained;
        ExpBar.fillAmount = Exp / ToLevel;
        GameObject FloatExp = Instantiate(FloatingText, transform.position, Quaternion.identity);
        FloatExp.transform.GetChild(0).GetComponent<TextMesh>().text = ExpGained.ToString() + " Exp";
        FloatExp.transform.GetChild(0).GetComponent<TextMesh>().color = new Color32(235, 200, 40, 255);
        if (Exp >= ToLevel)
        {
            Level++;
            TalentPoints++;
            GameObject LevelUp = Instantiate(LevelUpText, transform.position, Quaternion.identity);
            Power = Power + 2;
            Exp = Exp - ToLevel;
            ToLevel = ToLevel * 3;
            Health = MaxHealth;
            ExpBar.fillAmount = Exp / ToLevel;
        }
    }
    #endregion

    #region Resources

    #region Focus
    public static float GetFocusState()
    {
        return Focus / MaxFocus;
    }

    public static float GetMaxFocus()
    {
        return MaxFocus;
    }

    public static float GetCurrFocus()
    {
        return Focus;
    }

    public static bool GetFocusOn()
    {
        return FocusOn;
    }

    public static void LearnFocus()
    {
        FocusOn = true;
    }
    #endregion

    #region Rage
    public static void AddRage(float ToAdd)
    {
        if(Rage + ToAdd >= MaxRage)
        {
            Rage = MaxRage;
        }
        else
        {
            Rage = Rage + ToAdd;
        }
    }

    public static float GetRageState()
    {
        return Rage / MaxRage;
    }

    public static float GetMaxRage()
    {
        return MaxRage;
    }

    public static float GetCurrRage()
    {
        return Focus;
    }

    public static void LearnRage()
    {
        RageOn = true;
    }
    public static bool GetRageOn()
    {
        return RageOn;
    }
    #endregion

    #region Spears
    public static void AddSpear()
    {
        spears++;
        if (spears > MaxSpears)
            spears = MaxSpears;
        WM.GetComponent<WindowManager>().AddSpear();
    }

    public static void RemoveSpear()
    {
        spears--;
        WM.GetComponent<WindowManager>().RemoveSpear();
    }

    public static int GetSpears()
    {
        return spears;
    }

    public static bool GetSpearsOn()
    {
        return SpearsOn;
    }

    public static void LearnSpears()
    {
        SpearsOn = true;
        spears = MaxSpears;
    }

    #endregion

    #region Mana
    public static float GetManaState()
    {
        return Mana / MaxMana;
    }

    public static float GetMaxMana()
    {
        return MaxMana;
    }

    public static float GetCurrMana()
    {
        return Mana;
    }

    public static bool GetManaOn()
    {
        return ManaOn;
    }

    public static void LearnMana()
    {
        ManaOn = true;
    }
    #endregion

    #endregion

    private void HandleAiming()
    {
        MousePos = UtilsClass.GetMouseWorldPosition();
        aimDirection = (MousePos - transform.position).normalized;

    }

    public static Vector3 GetAim()
    {
        return aimDirection;
    }

    public void TakeDmg(float Damage)
    {
        Damage = Damage * (1-DamageResistance);
        GameObject DmgText = Instantiate(FloatingText, transform.position, Quaternion.identity);
        DmgText.transform.GetChild(0).GetComponent<TextMesh>().text = Damage.ToString();
        Health = Health - Damage;
        if (Health <= 0)
            SceneManager.LoadScene(1);
    }

    public void Heal(float heal)
    {
        GameObject DmgText = Instantiate(FloatingText, transform.position, Quaternion.identity);
        DmgText.transform.GetChild(0).GetComponent<TextMesh>().text = heal.ToString();
        DmgText.transform.GetChild(0).GetComponent<TextMesh>().color = new Color32(0, 255, 0, 255);
        Health = Health + heal;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }
}
