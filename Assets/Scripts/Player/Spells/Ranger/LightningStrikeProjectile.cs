using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeProjectile : MonoBehaviour
{
    private List<GameObject> OptionalTargets;
    // Start is called before the first frame update
    private bool Chained = false;
    public float Modifier;
    public GameObject ChainLightning;
    private float Spritetimer;
    void Start()
    {
        OptionalTargets = new List<GameObject>();
        Spritetimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Spritetimer < Time.time - 0.2f)
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (!Chained && OptionalTargets.Count>0 && Time.time > Spritetimer + 1f)
        {
            OptionalTargets[0].GetComponent<EnemyHealth>().TakeDmg(PlayerManager.Power * Modifier);
            GameObject ChainedLightning = Instantiate(ChainLightning, OptionalTargets[0].transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
            Destroy(ChainedLightning, 0.1f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Enemy"))
        {
            OptionalTargets.Add(Other.gameObject);
        }
    }
}
