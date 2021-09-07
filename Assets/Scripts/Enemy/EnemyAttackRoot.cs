using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRoot : MonoBehaviour, EnemyRoot
{
    private float AttackTime;
    public GameObject Collider;
    public GameObject ColliderLocation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableAttack()
    {
        this.gameObject.SetActive(true);
        GameObject collider = Instantiate(Collider);
        collider.transform.SetParent(ColliderLocation.transform);
        collider.transform.localPosition = new Vector3(0, 0, 0);
        collider.GetComponent<EnemyAttackCollider>().Wake();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 dir = PlayerManager.instance.transform.position - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
