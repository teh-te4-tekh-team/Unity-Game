using UnityEngine;
using System.Collections;

public class EnemyDetector : MonoBehaviour {

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Enemy")
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Enemy")
        {
            other.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
