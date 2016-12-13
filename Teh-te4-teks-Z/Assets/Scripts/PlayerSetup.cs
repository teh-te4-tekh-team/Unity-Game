using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : MonoBehaviour {
    void Start()
    {
       
            CameraController.target = this.transform;
       
    }
}
