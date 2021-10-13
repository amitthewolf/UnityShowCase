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

    /*
     * Singleton Design Pattern
     */
    private void Awake()
    {
        instance = this;
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

    /// <summary>
    /// Add flat percentage of damage mitigation.
    /// </summary>
    /// <param name="AddDmgResistance"> Flat percentage of damage mitigation to add.</param>
    public static void AddDmgResistance(float AddDmgResistance)
    {
        DamageResistance = DamageResistance + AddDmgResistance;
    }

    /// <summary>
    /// Remove flat percentage of damage mitigation.
    /// </summary>
    /// <param name="AddDmgResistance"> Flat percentage of damage mitigation to remove.</param>
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

    /// <summary>
    /// Used to Pull the current target from the player to the AI Pet.
    /// </summary>
    /// <returns></returns>
    internal static Collider2D getAttackingTarget()
    {
        return AttackingTarget;
    }

    internal static void ResetTargets()
    {
        AttackingTarget = null;
        Target = null;
    }

    /// <summary>
    /// Called when current target dies. Resets the player target field.
    /// </summary>
    internal static void ResetTarget()
    {
        Target = null;
    }

    /// <summary>
    /// Called when pet target dies. Resets the AttackingTarget field.
    /// </summary>
    internal static void ResetAttackingTarget()
    {
        AttackingTarget = null;
    }

    /// <summary>
    /// Called when dealing damage to a target, transfers target to pet.
    /// </summary>
    /// <param name="target">Damaged target</param>
    public static void SetAttackingTarget(Collider2D target)
    {
        if (AttackingTarget != null)
        {
            AttackingTarget.GetComponent<EnemyGeneral>().TargetedByPet = false;
        }
        AttackingTarget = target;
        AttackingTarget.GetComponent<EnemyGeneral>().TargetedByPet = true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
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
    /// <summary>
    /// Regeneration of health and resources.
    /// </summary>
    private void BothRegenTick()
    {
        LastTick = Time.time;
        if (Health < MaxHealth && Health < MaxHealth)
            Health = Health +0.05f;
        if (Health > MaxHealth)
            Health = MaxHealth;
        ResourceTick();
    }

    /// <summary>
    /// Manages resource regeneration
    /// </summary>
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

    /// <summary>
    /// Add experience points to player. Called when enemies die or quests are completed.
    /// </summary>
    /// <param name="ExpGained">Experience to gain</param>
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
    /// <summary>
    /// Returns the relational state of the current focus amount.
    /// </summary>
    /// <returns>Relational state of the current focus amount</returns>
    public static float GetFocusState()
    {
        return Focus / MaxFocus;
    }
    /// <summary>
    /// Returns the Max focus value.
    /// </summary>
    /// <returns>Max focus value</returns>
    public static float GetMaxFocus()
    {
        return MaxFocus;
    }
    /// <summary>
    /// Returns the current focus amount.
    /// </summary>
    /// <returns>Current focus amount</returns>
    public static float GetCurrFocus()
    {
        return Focus;
    }
    /// <summary>
    /// Returns true if focus is enabled and false otherwise.
    /// </summary>
    /// <returns>True if focus is enabled and false otherwise</returns>
    public static bool GetFocusOn()
    {
        return FocusOn;
    }
    /// <summary>
    /// Enables the focus resource
    /// </summary>
    public static void LearnFocus()
    {
        FocusOn = true;
    }
    #endregion

    #region Rage
    /// <summary>
    /// Adds rage resource
    /// </summary>
    /// <param name="ToAdd">Float amount of rage to add</param>
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
    /// <summary>
    /// Returns the relational state of the current rage amount.
    /// </summary>
    /// <returns>Relational state of the current rage amount.</returns>
    public static float GetRageState()
    {
        return Rage / MaxRage;
    }
    /// <summary>
    /// Returns the max amount of rage possible.
    /// </summary>
    /// <returns>Max amount of rage possible</returns>
    public static float GetMaxRage()
    {
        return MaxRage;
    }
    /// <summary>
    /// Returns the current amount of rage possible.
    /// </summary>
    /// <returns>Current amount of rage possible</returns>
    public static float GetCurrRage()
    {
        return Focus;
    }
    /// <summary>
    /// Enables the Rage resource
    /// </summary>
    public static void LearnRage()
    {
        RageOn = true;
    }
    /// <summary>
    /// returns true if the rage resource is enabled.
    /// </summary>
    /// <returns>True if rage is enabled and false otherwise</returns>
    public static bool GetRageOn()
    {
        return RageOn;
    }
    #endregion

    #region Spears
    /// <summary>
    /// Adds a spear to the spear resource
    /// </summary>
    public static void AddSpear()
    {
        spears++;
        if (spears > MaxSpears)
            spears = MaxSpears;
        WM.GetComponent<WindowManager>().AddSpear();
    }
    /// <summary>
    /// Removes 1 spear from the spear resource
    /// </summary>
    public static void RemoveSpear()
    {
        spears--;
        WM.GetComponent<WindowManager>().RemoveSpear();
    }
    /// <summary>
    ///  returns the current amount of spears.
    /// </summary>
    /// <returns>Current amount of spears</returns>
    public static int GetSpears()
    {
        return spears;
    }
    /// <summary>
    /// returns true if the spear resource is enabled and false otherwise
    /// </summary>
    /// <returns>True if the spear resource is enabled and false otherwise</returns>
    public static bool GetSpearsOn()
    {
        return SpearsOn;
    }
    /// <summary>
    /// Enables the spear resource.
    /// </summary>
    public static void LearnSpears()
    {
        SpearsOn = true;
        spears = MaxSpears;
    }

    #endregion

    #region Mana
    /// <summary>
    /// Returns the relational state of the current mana amount.
    /// </summary>
    /// <returns>Relational state of the current mana amount</returns>
    public static float GetManaState()
    {
        return Mana / MaxMana;
    }
    /// <summary>
    /// returns the max amount of mana possible.
    /// </summary>
    /// <returns>Max amount of mana possible.</returns>
    public static float GetMaxMana()
    {
        return MaxMana;
    }
    /// <summary>
    /// returns the current amount of mana.
    /// </summary>
    /// <returns>Current amount of mana</returns>
    public static float GetCurrMana()
    {
        return Mana;
    }
    /// <summary>
    /// Returns true if the mana resource is enabled and false otherwise.
    /// </summary>
    /// <returns>True if the mana resource is enabled and false otherwise.</returns>
    public static bool GetManaOn()
    {
        return ManaOn;
    }
    /// <summary>
    /// Enables the mana resource, called when the Druid branch is taken.
    /// </summary>
    public static void LearnMana()
    {
        ManaOn = true;
    }
    #endregion

    #endregion
    /// <summary>
    /// This function is called periodically to track the player's aiming.
    /// </summary>
    private void HandleAiming()
    {
        MousePos = UtilsClass.GetMouseWorldPosition();
        aimDirection = (MousePos - transform.position).normalized;

    }
    /// <summary>
    /// returns the current aim direction as a Vector3 variable.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetAim()
    {
        return aimDirection;
    }

    /// <summary>
    /// Reduces health by the given amount in the argument.
    /// </summary>
    /// <param name="Damage">The amount of health to reduce.</param>
    public void TakeDmg(float Damage)
    {
        Damage = Damage * (1-DamageResistance);
        GameObject DmgText = Instantiate(FloatingText, transform.position, Quaternion.identity);
        DmgText.transform.GetChild(0).GetComponent<TextMesh>().text = Damage.ToString();
        Health = Health - Damage;
        if (Health <= 0)
            SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Adds a given amount of health to the player.
    /// </summary>
    /// <param name="heal">Amount of health to add.</param>
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
