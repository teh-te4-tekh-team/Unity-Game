using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject currentPlayer;

    private Vector3 offset;

    private void Start()
    {
        this.offset = this.transform.position - this.currentPlayer.transform.position;
    }

    private void LateUpdate()
    {
        this.transform.position = this.currentPlayer.transform.position + this.offset;
        this.transform.LookAt(this.currentPlayer.transform);
    }
}
