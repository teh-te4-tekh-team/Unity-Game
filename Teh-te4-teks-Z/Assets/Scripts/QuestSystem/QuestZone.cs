using UnityEngine;
using System.Collections;

public class QuestZone : MonoBehaviour
{

    public Transform player;

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "PlayerModel") return;

        if (other.transform.root.CompareTag("Player"))
        {
            // 
        }
    }
}
