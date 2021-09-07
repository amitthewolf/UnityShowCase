using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspSwarm : MonoBehaviour
{
    public GameObject WaspSwarmDot;

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
            bool Exists = other.GetComponent<DotManager>().AddDot(WaspSwarmDot.GetComponent<WaspSwarmDot>());
            if (!Exists)
            {
                GameObject Dot = Instantiate(WaspSwarmDot);
                Dot.transform.SetParent(other.GetComponent<DotManager>().transform);
                Dot.transform.position = other.transform.position;
                other.GetComponent<DotManager>().saveDot(Dot);
            }
            Destroy(gameObject);
        }
    }
}
