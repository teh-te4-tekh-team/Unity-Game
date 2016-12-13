using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 250f;
    
    //private Vector3 movement;
    //private Rigidbody playerRigidBody;

    public GameObject Bullet;
    public Transform BulletSpawn;

    //public NetworkStartPosition[] NetworkStartPositions;
    public int InitialHealth = 100;

    //[SyncVar(hook = "OnChangeHealth")]
    public int currentHealth;

    public RectTransform healthBar;

    public GameUser CurrentUser;

    void Start()
    {
        //if (this.isLocalPlayer)
        //{
        //    this.NetworkStartPositions = FindObjectsOfType<NetworkStartPosition>();
        //}
    }

    void Awake()
    {
        this.currentHealth = this.InitialHealth;
        //this.playerRigidBody = this.GetComponent<Rigidbody>();
        this.CurrentUser = new GameUser();
    }

    void FixedUpdate()
    {
        if (this.currentHealth <= 0)
        {
            this.Death();
        }
        float horizontal = Input.GetAxis("Horizontal") * this.rotationSpeed;
        float vertical = Input.GetAxis("Vertical") * this.speed;

        this.Move(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.CmdFire();
        }
    }

    //[Command]
    private void CmdFire()
    {

        GameObject bullet = (GameObject)Instantiate(
           this.Bullet, this.BulletSpawn.position, this.BulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 16;

        //NetworkServer.Spawn(bullet);

        Destroy(bullet, 5.0f);
    }

    //void OnChangeHealth(int health)
    //{
    //    this.healthBar.sizeDelta = new Vector2(health * 5, this.healthBar.sizeDelta.y);
    //    //this.healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    //    //HudManager.instance.UpdateHealth(this.currentHealth);
    //}

    public void TakeDamage(int amount)
    {
        this.currentHealth -= amount;

        this.healthBar.sizeDelta = new Vector2(this.currentHealth * 5, this.healthBar.sizeDelta.y);
        HudManager.instance.UpdateHealth(this.currentHealth);

        if (this.currentHealth <= 0)
        {
            this.Death();
        }
    }

    void Move(float horizontal, float vertical)
    {
        vertical *= Time.deltaTime;
        horizontal *= Time.deltaTime;
        this.transform.Translate(0, 0, vertical);
        this.transform.Rotate(0, horizontal, 0);

        //this.movement.Set(horizontal, 0, vertical);
        //this.movement = this.movement.normalized * this.speed * Time.deltaTime;
        //this.playerRigidBody.MovePosition(this.transform.position + this.movement);
    }

    void Death()
    {
        //this.RpcRespawn();
        //this.currentHealth = this.InitialHealth;
        this.gameObject.SetActive(false);
    }

    //[ClientRpc]
    //void RpcRespawn()
    //{
    //    if (this.isLocalPlayer)
    //    {
    //        // Set the spawn point to origin as a default value
    //        Vector3 spawnPoint = Vector3.zero;

    //        // If there is a spawn point array and the array is not empty, pick one at random
    //        if (this.NetworkStartPositions != null && this.NetworkStartPositions.Length > 0)
    //        {
    //            spawnPoint = this.NetworkStartPositions[Random.Range(0, this.NetworkStartPositions.Length)].transform.position;
    //        }

    //        // Set the player’s position to the chosen spawn point
    //        this.transform.position = spawnPoint;
    //    }
    //}

    //public override void OnStartLocalPlayer()
    //{
    //    this.gameObject.GetComponentsInChildren<MeshRenderer>().Single(renderer => renderer.name == "Visor").material.color = Color.red;
        
    //}
}