using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    public GameObject Origin;
    public GameObject AttackRoot;
    public GameObject Target;
    private float AttackTime;
    private bool Attacked = false;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindWithTag("Player");
        AttackRoot = transform.parent.parent.gameObject;
        Origin = AttackRoot.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Wake()
    {
        Attacked = false;
        AttackTime = Time.time;
        Invoke("TouchCheck", 0.5f);
        Invoke("DisableRoot", 0.8f);
    }

    private void DisableRoot()
    {
        Destroy(this.gameObject);
        AttackRoot.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            Target = Other.gameObject;
        }
    }

    private void TouchCheck()
    {
        if (Target.GetComponent<Collider2D>().IsTouching(this.GetComponent<Collider2D>()))
        {
            DealDmg();
        }
    }

    private void DealDmg()
    {
        PlayerManager.instance.TakeDmg(Origin.GetComponent<NewEnemyAI>().AttackDmg);
        Attacked = true;
        AttackRoot.GetComponent<AudioSource>().Play();
    }

}
