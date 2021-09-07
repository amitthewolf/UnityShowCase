using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    public List<IAutoAtt> Autoattacks;
    private IAutoAtt ChosenAA;
    private IAutoAtt SecondaryAA;
    private IAutoAtt TempAA;
    private float LastAttack;
    public float AttackSpeed;
    private bool Continued;
    public static bool AAEnabled;

    private void Start()
    {
        Autoattacks = new List<IAutoAtt>();
        Autoattacks.Add(Player.GetComponent<Shooting>());
        Autoattacks.Add(Player.GetComponent<SpearThrust>());
        Autoattacks.Add(Player.GetComponent<DruidAA>());
        Autoattacks.Add(Player.GetComponent<BMAA>());
        AAEnabled = true;
    }

    public void ChangeAutoAttack(int ChosenIndex)
    {
        if(ChosenAA != null)
        {
            SecondaryAA = ChosenAA;
        }
        for( int i = 0; i < Autoattacks.Count;i++)
        {
            IAutoAtt AA = Autoattacks[i];
            if (i == ChosenIndex)
            {
                AA.Activate();
                ChosenAA = AA;
            }
            else
                AA.Deactivate();
        }
    }

    private void SwitchAuto()
    {
        if (SecondaryAA != null)
        {
            ChosenAA.Deactivate();
            TempAA = ChosenAA;
            ChosenAA = SecondaryAA;
            ChosenAA.Activate();
            SecondaryAA = TempAA;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= LastAttack + AttackSpeed)
        {
            if(ChosenAA!=null && AAEnabled)
            {
                PlayerMovement.MovementAbility = true;
                PlayerMovement.Stop();
                ChosenAA.AutoAttack(PlayerManager.Power);
                LastAttack = Time.time;
                Continued = false;
            }
        }
        else if (Time.time >= LastAttack + 0.5f && !Continued)
        {
            PlayerMovement.MovementAbility = false;
            Continued = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchAuto();
        }
    }


}
