using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // To check authority
        if(isLocalPlayer)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                CmdPunch();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                CmdKick();
            }
        }
    }

    // This will be executed on the server copy of the client
    [Command]
    void CmdPunch()
    {
        // Uncomment this if you want server also to trigger the animation
        //Punch();
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
