using System.Linq;
using UnityEngine;

public class ElexirDrink : MonoBehaviour
{
    public void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.root.tag != "Elexir")
        {
            return;
        }

        SightFader sightFader = this.transform.root.GetComponentInChildren<SightFader>();
        sightFader.TakeElexir();
        Destroy(coll.gameObject);
    }
}
