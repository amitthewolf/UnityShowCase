using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttackRoot : MonoBehaviour, EnemyRoot
{
    public GameObject AttackProjectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void EnableAttack()
    {
        GameObject Projectile = Instantiate(AttackProjectile, gameObject.transform.position, Quaternion.identity);
        Vector3 Aim = (PlayerManager.instance.transform.position - transform.position).normalized;
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(6*Aim.x, 6*Aim.y);
        Projectile.transform.Rotate(0f, 0f, Mathf.Atan2(Aim.y, Aim.x) * Mathf.Rad2Deg);
        Destroy(Projectile,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
