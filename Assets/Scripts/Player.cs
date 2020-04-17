using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{

    private Animator animator = null;
    private CharacterController controller = null;

    [SyncVar]
    private int health = 100;

    private Text PlayerId;
    private Text Health;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        if(isLocalPlayer)
        {
            PlayerId = GameObject.FindGameObjectWithTag("PlayerId").GetComponent<Text>();
            Health = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
            PlayerId.text = "Player Id: " + netId.ToString();
            Health.text = "Health: " + health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // To check authority
        if(isLocalPlayer)
        {
            //if(controller.isGrounded)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                Vector3 delta = new Vector3(h, 0.0f, v) * Time.deltaTime;
                controller.Move(delta);
                //CmdMove(delta);
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                CmdPunch();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                CmdKick();
            }

            Health.text = "Health: " + health.ToString();
            animator.SetFloat("speed", controller.velocity.magnitude);
        }

        Debug.Log("Player " + netId + " Health " + health);
    }

    [Command]
    private void CmdMove(Vector3 delta)
    {
        transform.position += delta;
        //controller.Move(delta);
    }

    // This will be executed on the server copy of the client
    [Command]
    void CmdPunch()
    {
        //health--;
        // Uncomment this if you want server also to trigger the animation
        Punch();
        RpcPunch();
    }

    // This will be executed on the server copy of the client
    [Command]
    void CmdKick()
    {
        Kick();
        RpcKick();
    }

    // This will be executed on the all the clients except on the server copy of the client
    [ClientRpc]
    void RpcPunch()
    {
        Punch();
    }

    void Punch()
    {
        animator.SetTrigger("punch");
    }

    // This will be executed on the all the clients except on the server copy of the client
    [ClientRpc]
    void RpcKick()
    {
        Kick();
    }

    void Kick()
    {
        animator.SetTrigger("kick");
    }
}
