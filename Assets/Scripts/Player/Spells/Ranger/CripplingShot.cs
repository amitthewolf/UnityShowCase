using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CripplingShot : MonoBehaviour
{
    public GameObject CripplingShotDebuff;

    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerManager.SetTarget(other);
            other.GetComponent<EnemyHealth>().TakeDmg(PlayerManager.Power*1.2f);
            bool Exists = other.GetComponent<DotManager>().AddDot(CripplingShotDebuff.GetComponent<CripplingShotDebuff>());
            if (!Exists)
            {
                GameObject Dot = Instantiate(CripplingShotDebuff);
                Dot.transform.SetParent(other.GetComponent<DotManager>().transform);
                Dot.transform.position = other.transform.position;
            }
            Destroy(gameObject);
        }
    }
}
