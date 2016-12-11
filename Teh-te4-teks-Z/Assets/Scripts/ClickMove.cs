using UnityEngine;

public class ClickMove : MonoBehaviour
{
    public GameObject player;

    public void OnClick(RaycastHit hit)
    {
        if (this.player.GetComponent<PlayerHealth>().currentHealth < 0)
        {
            PlayerMovement navPos = this.player.GetComponent<PlayerMovement>();
            navPos.Move(hit.point);
        }
    }
}
