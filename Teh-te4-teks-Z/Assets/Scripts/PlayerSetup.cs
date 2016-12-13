using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    void Start()
    {
        if (this.isLocalPlayer)
        {
            this.GetComponent<PlayerController>().enabled = true;
            CameraController.target = this.transform;
        }
    }
}
