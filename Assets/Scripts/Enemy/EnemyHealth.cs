using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float Health;
    public float MaxHealth;
    public Image HealthBar;
    public GameObject FloatingDamage;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = Health / MaxHealth;
    }

    public void TakeDmg(float Damage)
    {
        if(GetComponent<NewEnemyAI>() != null)
            GetComponent<NewEnemyAI>().StartChasing();
        else if(GetComponent<Solomon>() != null)
            GetComponent<Solomon>().StartChasing();
        GameObject DmgText = Instantiate(FloatingDamage, transform.position, Quaternion.identity);
        DmgText.transform.GetChild(0).GetComponent<TextMesh>().text = Damage.ToString();
        Health = Health - Damage;
        if (Health <= 0)
            GetComponent<EnemyGeneral>().Die();
    }

    public void TakeDotDmg(float Damage,int DotType)
    {
        if (GetComponent<NewEnemyAI>()!= false)
            GetComponent<NewEnemyAI>().StartChasing();
        else if (GetComponent<Solomon>() != null)
            GetComponent<Solomon>().StartChasing();
        GameObject DmgText = Instantiate(FloatingDamage, transform.position, Quaternion.identity);
        DmgText.transform.GetChild(0).GetComponent<TextMesh>().text = Damage.ToString();
        if(DotType==1)
            DmgText.transform.GetChild(0).GetComponent<TextMesh>().color = new Color32(255, 0, 0, 255);
        else
            DmgText.transform.GetChild(0).GetComponent<TextMesh>().color = new Color32(255, 255, 0, 255);
        Health = Health - Damage;
        if (Health <= 0)
            GetComponent<EnemyGeneral>().Die();
    }
}
