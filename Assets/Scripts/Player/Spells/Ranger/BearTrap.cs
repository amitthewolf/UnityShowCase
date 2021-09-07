using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public GameObject BleedingEffect;
    public float SnareTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<NewEnemyAI>().Snare(SnareTime);
            bool Exists = other.GetComponent<DotManager>().AddDot(BleedingEffect.GetComponent<TrapBleed>());
            if (!Exists)
            {
                GameObject Dot = Instantiate(BleedingEffect);
                Dot.transform.SetParent(other.GetComponent<DotManager>().transform);
                Dot.transform.position = other.transform.position;
                other.GetComponent<DotManager>().saveDot(Dot);
            }
            Destroy(gameObject);
        }
    }
}
