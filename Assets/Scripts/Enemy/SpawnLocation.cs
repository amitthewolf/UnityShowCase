using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public List<GameObject> PossibleSpawn;
    public float SpawnTimer;
    private float SpawnDeath;
    private GameObject CurrSpawn;
    private bool Died;

    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Died && CurrSpawn == null && Time.time > SpawnTimer + SpawnDeath)
        {
            Debug.Log("Time - " + Time.time);
            Debug.Log("SpawnTimer - " + SpawnTimer);
            Debug.Log("SpawnDeath - " + SpawnDeath);
            Respawn();
        }
        else if (CurrSpawn == null && SpawnDeath == 0)
        {
            Died = true;
            SpawnDeath = Time.time;
            Debug.Log(SpawnDeath);
        }
    }

    private void Respawn()
    {
        SpawnDeath = 0;
        Died = false;
        int index = Random.Range(0, PossibleSpawn.Count);
        CurrSpawn = Instantiate(PossibleSpawn[index]);
        CurrSpawn.transform.SetParent(this.transform);
        CurrSpawn.transform.localPosition = new Vector3(0, 0, 0);
        CurrSpawn.GetComponent<NewEnemyAI>().SetOGPosition();
    }
}
