using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    void OnCollisionEnter(Collision coll)
    {
        GameObject hit = coll.gameObject;
        EnemyController enemyController = hit.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.TakeDamage(10);
        }

        Destroy(this.gameObject);
    }
}