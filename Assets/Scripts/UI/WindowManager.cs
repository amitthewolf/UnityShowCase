using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WindowManager : MonoBehaviour
{
    private bool CharacterPanelOpen;
    private bool TalentPanelOpen;
    private bool QuestLogOpen;
    public Canvas CharacterPanel;
    public Canvas TalentPanel;
    public Canvas TutorialPanel1;
    public Canvas TutorialPanel2;
    public Canvas ExitPanel;
    public Quest StartQuest;
    public GameObject QuestLog;
    public GameObject QuestOffer;
    public GameObject Interact;
    public GameObject FocusGlobe;
    public GameObject ManaGlobe;
    public GameObject RageGlobe;
    public GameObject Spears;
    public GameObject Spear;
    public GameObject Resources;
    private GameObject focus;
    private GameObject mana;
    private GameObject rage;
    private GameObject spears;
    private bool TutorialOver;

    // Start is called before the first frame update
    void Start()
    {
        CharacterPanelOpen = false;
        TalentPanelOpen = false;
        QuestLogOpen = false;
    }


    public void GainFocus()
    {
        focus = Instantiate(FocusGlobe);
        focus.transform.SetParent(Resources.transform);
    }

    public void GainMana()
    {
        mana = Instantiate(ManaGlobe);
        mana.transform.SetParent(Resources.transform);
    }

    public void GainSpears()
    {
        spears = Instantiate(Spears);
        spears.transform.SetParent(Resources.transform);
    }


    public void GainRage()
    {
        rage = Instantiate(RageGlobe);
        rage.transform.SetParent(Resources.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleExitPanelOn();
        }
        if (Input.GetKeyDown(KeyCode.C) && !CharacterPanelOpen)
        {
            CharacterPanelOpen = true;
            ToggleCharPanel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            CharacterPanelOpen = false;
            ToggleCharPanel();
        }
        if (Input.GetKeyDown(KeyCode.N) && !TalentPanelOpen)
        {
            
            ToggleTalPanel();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
           
            TalentTree.HideTooltip();
            ToggleTalPanel();
        }
        if (Input.GetKeyDown(KeyCode.L) && !QuestLogOpen)
        {
            ToggleQuestPanel();
            HideTutorial2();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleQuestPanel();
        }
        if (PlayerManager.GetFocusOn() && focus != null)
            focus.transform.GetChild(1).GetComponent<Image>().fillAmount = PlayerManager.GetFocusState();
        if (PlayerManager.GetManaOn() && mana != null)
            mana.transform.GetChild(1).GetComponent<Image>().fillAmount = PlayerManager.GetManaState();
        if (PlayerManager.GetRageOn() && rage != null)
            rage.transform.GetChild(1).GetComponent<Image>().fillAmount = PlayerManager.GetRageState();
    }

    private void ToggleExitPanelOn()
    {
        ExitPanel.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void ToggleExitPanelOff()
    {
        ExitPanel.gameObject.SetActive(false);
    }

    public void ShowTutorial2()
    {
        TutorialPanel2.gameObject.SetActive(true);
    }

    public void HideTutorial2()
    {
        TutorialPanel2.gameObject.SetActive(false);
    }

    public void ToggleOffTutorial()
    {
        TutorialPanel1.gameObject.SetActive(false);
    }

    public void RemoveSpear()
    {
        Destroy(spears.transform.GetChild(0).gameObject);
    }

    public void AddSpear()
    {
        GameObject NewSpear = Instantiate(Spear);
        NewSpear.transform.SetParent(spears.transform);
    }

    public void ToggleQuestPanel()
    {
        if (QuestLogOpen)
        {
            QuestLogOpen = false;
            AAHandler.AAEnabled = true;
        }
        else
        {
            QuestLogOpen = true;
            AAHandler.AAEnabled = false;

        }
        QuestLog.SetActive(QuestLogOpen);
        
    }

    public void ToggleCharPanel()
    {
        CharacterPanel.gameObject.SetActive(CharacterPanelOpen);
    }

    public void ToggleTalPanel()
    {
        if (TalentPanelOpen)
        {
            TalentPanelOpen = false;
            if (!TutorialOver)
            {
                QuestManager.instance.NewQuest(StartQuest);
                ShowTutorial2();
                TutorialOver = true;
            }
            AAHandler.AAEnabled = true;

        }
        else
        {
            TalentPanelOpen = true;
            ToggleOffTutorial();
            AAHandler.AAEnabled = false;
        }
        TalentPanel.gameObject.SetActive(TalentPanelOpen);

    }

    public void ToggleInteract(bool state)
    {
        Interact.gameObject.SetActive(state);
    }

    public void ToggleQuestOffer(bool state)
    {
        if (state)
        {
            ToggleInteract(false);
            AAHandler.AAEnabled = false;
        }
        else
        {
            AAHandler.AAEnabled = true;
        }
        QuestOffer.gameObject.SetActive(state);
    }
}
