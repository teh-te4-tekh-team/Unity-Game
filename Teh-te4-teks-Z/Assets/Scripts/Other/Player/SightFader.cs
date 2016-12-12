using UnityEngine;
using System.Collections;

public class SightFader : MonoBehaviour
{

    public SphereCollider SightTrigger;
    public float DefaultRadius;
    
    public void Start()
    {
        this.BeginFade();
    }

    private void BeginFade()
    {
        this.SightTrigger.radius = this.DefaultRadius;
        this.StartCoroutine(this.FadeOut());
    }

    public void TakeElexir()
    {
        this.StopAllCoroutines();

        this.BeginFade();
    }

    private IEnumerator FadeOut()
    {
        while (this.SightTrigger.radius > 5)
        {
            this.SightTrigger.radius -= 1f;
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}
