using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalentTree : MonoBehaviour
{
    public GameObject WindowManager;
    public GameObject Player;
    public GameObject TalentPointsText;
    public GameObject ToolTip;
    public static Tooltip tt;
    private int talentPoints;
    private int NumofResources;

    //LearnedFlags
    Hashtable LearnedFlags;

    //Buttons
    public GameObject DruidButton;
    public GameObject TrapperButton;
    public GameObject MarksmanButton;
    public GameObject SpearThrowButton;
    public GameObject BearTrapButton;
    public GameObject LungeButton;
    public GameObject VitalShotButton;
    public GameObject CripplingShotButton;
    public GameObject BiteButton;
    public GameObject NaturesBulwarkButton;
    public GameObject WaspSwarmButton;
    public GameObject LashvineButton;
    public GameObject LightningStrikeButton;


    //Spells
    public GameObject CripplingShot;
    public GameObject SpearThrow;
    public GameObject BearTrap;
    public GameObject Lunge;
    public GameObject VitalShot;
    public GameObject Bite;
    public GameObject NaturesBulwark;
    public GameObject WaspSwarm;
    public GameObject Lashvine;
    public GameObject LightningStrike;

    void Start()
    {
        LearnedFlags = new Hashtable();
        tt = ToolTip.GetComponent<Tooltip>();
        NumofResources = 0;
    }

    // Update is called once per frame
    void Update()
    {
        talentPoints = PlayerManager.TalentPoints;
        TalentPointsText.GetComponent<TMP_Text>().text = talentPoints.ToString();
    }

    public void HideTooltipTT()
    {
        HideTooltip();
    }

    public static void HideTooltip()
    {
        tt.HideTooltip();
    }

    #region Marksman
    public void LearnMarksman()
    {
        if(talentPoints > 0 && NumofResources < 2 && !LearnedFlags.ContainsKey("Marksman"))
        {
            Player.GetComponent<AAHandler>().ChangeAutoAttack(0);
            WindowManager.GetComponent<WindowManager>().GainFocus();
            PlayerManager.LearnFocus();
            PlayerManager.TalentPoints = PlayerManager.TalentPoints-1;
            NumofResources++;
            VitalShotButton.GetComponent<Button>().interactable = true;
            CripplingShotButton.GetComponent<Button>().interactable = true;
            LearnedFlags.Add("Marksman", true);
        }
    }

    public void ShowMarksmanTooltip()
    {
        tt.ShowTooltip("Marksman", "Focus", "This subclass focuses on long range abilities. Sniping enemies from a distance utilizing crit, mobility and resource management. Auto-attack deals more damage the more Focus the player has.");
    }

    public void LearnVitalShot()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("VitalShot"))
        {
            GameObject NewAbility = Instantiate(VitalShot);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<VitalShot>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("VitalShot", true);
        }
    }

    public void ShowVitalShotTooltipTT()
    {
        ShowVitalShotTooltip();
    }

    public static void ShowVitalShotTooltip()
    {
        tt.ShowTooltip("Vital Shot", "50% Focus", "Aim for a target's vitals, dealing damage with double crit chance and +50% crit damage.");
    }


    public void LearnCripplingShot()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("CripplingShot"))
        {
            GameObject NewAbility = Instantiate(CripplingShot);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<CripplingShotCast>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("CripplingShot", true);
        }
    }

    public void ShowCripplingShotTooltipTT()
    {
        ShowCripplingShotTooltip();
    }

    public static void ShowCripplingShotTooltip()
    {
        tt.ShowTooltip("Crippling Shot", "20% Focus", "Cripple an enemy at range, dealing damage low damage and slowing the target for 3 seconds.");
    }
    #endregion

    #region Druid
    public void LearnDruid()
    {
        if (talentPoints > 0 && NumofResources < 2 && !LearnedFlags.ContainsKey("Druid"))
        {
            Player.GetComponent<AAHandler>().ChangeAutoAttack(2);
            WindowManager.GetComponent<WindowManager>().GainMana();
            PlayerManager.LearnMana();
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            NumofResources++;
            NaturesBulwarkButton.GetComponent<Button>().interactable = true;
            WaspSwarmButton.GetComponent<Button>().interactable = true;
            LearnedFlags.Add("Druid", true);
        }
    }

    public void ShowDruidTooltip()
    {
        tt.ShowTooltip("Druid", "Mana", "This subclass focuses on magical abilities that can both heal and harm foes. Specializing in damage over time effects and burst damage.");
    }

    public void LearnNaturesBulwark()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("NaturesBulwark"))
        {
            GameObject NewAbility = Instantiate(NaturesBulwark);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<NaturesBulwarkCaster>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("NaturesBulwark", true);
            LashvineButton.GetComponent<Button>().interactable = true;
            LightningStrikeButton.GetComponent<Button>().interactable = true;
        }
    }

    public void ShowNaturesBulwarkTooltipTT()
    {
        ShowNaturesBulwarkTooltip();
    } 

    public static void ShowNaturesBulwarkTooltip()
    {
        tt.ShowTooltip("Natures Bulwark", "12% Mana", "Summon living armor to reduce damage taken by 20% for 6s. Heal over duration.");
    }

    public void LearnWaspSwarm()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("WaspSwarm"))
        {
            GameObject NewAbility = Instantiate(WaspSwarm);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<WaspCast>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("WaspSwarm", true);
            LashvineButton.GetComponent<Button>().interactable = true;
            LightningStrikeButton.GetComponent<Button>().interactable = true;
        }
    }

    public void ShowWaspSwarmTooltipTT()
    {
        ShowWaspSwarmTooltip();
    }

    public static void ShowWaspSwarmTooltip()
    {
        tt.ShowTooltip("Wasp Swarm", "12% Mana", "Summon wasps to harry target, slowing by 20% and dealing damage for 6s.");
    }

    public void LearnLashvine()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("Lashvine"))
        {
            GameObject NewAbility = Instantiate(Lashvine);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<LashvineCast>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("Lashvine", true);
        }
    }

    public void ShowLashvineTooltipTT()
    {
        ShowLashvineTooltip();
    }

    public static void ShowLashvineTooltip()
    {
        tt.ShowTooltip("Summon Lashvine", "16% Mana", "Summon a stationary Lashvine at the target location, it attacks nearby enemies.");
    }

    public void LearnLightningStrike()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("LightningStrike"))
        {
            GameObject NewAbility = Instantiate(LightningStrike);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<LightningStrikeCast>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("LightningStrike", true);
        }
    }

    public void ShowLightningStrikeTooltipTT()
    {
        ShowLightningStrikeTooltip();
    }

    public static void ShowLightningStrikeTooltip()
    {
        tt.ShowTooltip("Call Lightning", "16% Mana", "Call a Lightning Strike on a target enemy, within 1 second - a second bolt will strike a nearby enemy.");
    }

    #endregion

    #region Trapper
    public void LearnTrapper()
    {
        if (talentPoints > 0 && NumofResources < 2 && !LearnedFlags.ContainsKey("Trapper"))
        {
            Player.GetComponent<AAHandler>().ChangeAutoAttack(1);
            WindowManager.GetComponent<WindowManager>().GainSpears();
            PlayerManager.LearnSpears();
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            NumofResources++;
            SpearThrowButton.GetComponent<Button>().interactable = true;
            BearTrapButton.GetComponent<Button>().interactable = true;
            LearnedFlags.Add("Trapper", true);
        }
    }

    public void ShowTrapperTooltip()
    {
        tt.ShowTooltip("Trapper", "Spears", "This subclass focuses on short to medium range abilities using spears. Specializing in crowd control, bleeding and mobility.");
    }

    public void LearnSpearThrow()
    {
        if(talentPoints>0 && !LearnedFlags.ContainsKey("SpearThrow"))
        {
            GameObject NewAbility = Instantiate(SpearThrow);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<SpearThrow>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("SpearThrow", true);
            LungeButton.GetComponent<Button>().interactable = true;
        }
    }

    public void ShowSpearThrowTooltipTT()
    {
        ShowSpearThrowTooltip();
    }

    public static void ShowSpearThrowTooltip()
    {
        tt.ShowTooltip("Spear Throw", "1 Spear", "Throws a spear to target location, deals heavy damage. Leaves behind a spear that can be picked up to replenish spear resource.");
    }


    public void LearnBearTrap()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("BearTrap"))
        {
            GameObject NewAbility = Instantiate(BearTrap);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<BearTrapCaster>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("BearTrap", true);
            LungeButton.GetComponent<Button>().interactable = true;
        }
    }

    public void ShowBearTrapTooltipTT()
    {
        ShowBearTrapTooltip();
    }


    public static void ShowBearTrapTooltip()
    {
        tt.ShowTooltip("Bear Trap", "", "Places a bear trap that roots enemies and causes bleed damage over time.");
    }

    public void LearnLunge()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("Lunge"))
        {
            GameObject NewAbility = Instantiate(Lunge);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<LungeCast>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("Lunge", true);
        }
    }

    public void ShowLungeTooltipTT()
    {
        ShowLungeTooltip();
    }


    public static void ShowLungeTooltip()
    {
        tt.ShowTooltip("Lunge", "", "The Trapper lunges at a target location dealing damage to enemies in its path. Cooldown is reset if a spear is picked up.");
    }
    #endregion

    #region Beastmaster
    public void LearnBeastMaster()
    {
        if (talentPoints > 0 && NumofResources < 2 && !LearnedFlags.ContainsKey("BeastMaster"))
        {
            Player.GetComponent<AAHandler>().ChangeAutoAttack(3);
            WindowManager.GetComponent<WindowManager>().GainRage();
            PlayerManager.LearnRage();
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            NumofResources++;
            BiteButton.GetComponent<Button>().interactable = true;
            LearnedFlags.Add("BeastMaster", true);
        }
    }

    public void ShowBeastmasterTooltip()
    {
        tt.ShowTooltip("Beastmaster", "Rage", "This subclass focuses on synergistic abilities with the allied companion. specializes in crowd control, and combo abilities with pet.");
    }

    public void LearnBite()
    {
        if (talentPoints > 0 && !LearnedFlags.ContainsKey("Bite"))
        {
            GameObject NewAbility = Instantiate(Bite);
            NewAbility.transform.SetParent(Player.transform.GetChild(0));
            NewAbility.transform.position = Player.transform.position;
            Player.GetComponent<SpellHandler>().Spells.Add(NewAbility.GetComponent<Bite>());
            PlayerManager.TalentPoints = PlayerManager.TalentPoints - 1;
            LearnedFlags.Add("Bite", true);
        }
    }

    public void ShowBiteTooltipTT()
    {
        ShowBiteTooltip();
    }

    public static void ShowBiteTooltip()
    {
        tt.ShowTooltip("Bite", "", "send companion to bite target, changing its current target. generates 12 Rage.");
    }
    #endregion
}
