using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpear : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

   

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            PlayerManager.AddSpear();
            LungeCast LungeCast = Player.GetComponentInChildren<LungeCast>();
            if (LungeCast != null && LungeCast.IsActive())
            {
                LungeCast.ResetCD();
            }
            Destroy(gameObject);
        }
    }


    /*
    void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        Vector3 PlayerPos = Player.transform.position;
        if (Vector3.Distance(transform.position, PlayerPos)<=1f)
        {
            PlayerManager.AddSpear();
            if (Player.GetComponentInChildren<LungeCast>() != null)
                Player.GetComponentInChildren<LungeCast>().ResetCD();
            Destroy(gameObject);
        }
    }
    */
}
