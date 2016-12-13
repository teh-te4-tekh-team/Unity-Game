using UnityEngine;
using UnityEngine.Networking;

public class PlayerController2 : NetworkBehaviour
{

    public GameObject Bullet;
    public Transform BulletSpawn;

    void Update()
    {
        if (!this.isLocalPlayer)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 250.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;

        this.transform.Rotate(0, x, 0);
        this.transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.CmdFire();
        }

    }

    [Command]
    private void CmdFire()
    {

        GameObject bullet = (GameObject)Instantiate(
           this.Bullet, this.BulletSpawn.position, this.BulletSpawn.rotation);
        
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 16;
        
        NetworkServer.Spawn(bullet);

        Destroy(bullet, 5.0f);
    }

    //public override void OnStartLocalPlayer()
    //{
    //    //this.GetComponent<MeshRenderer>().material.color = Color.blue;
    //}
}