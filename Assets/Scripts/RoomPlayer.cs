using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomPlayer : NetworkRoomPlayer
{
    private LobbyUIManager lobbyUIManager = null;

    public override void OnStartClient()
    {
        if (LogFilter.Debug) Debug.LogFormat("OnStartClient {0}", SceneManager.GetActiveScene().name);

        base.OnStartClient();
    }

    public override void OnClientEnterRoom()
    {
        Debug.Log("OnClientEnterRoom");

        //Invoke("Abc", 1f);

    }

    public override void OnClientExitRoom()
    {
        if (LogFilter.Debug) Debug.LogFormat("OnClientExitRoom {0}", SceneManager.GetActiveScene().name);
    }

    public void SelectColor(Color color)
    {
        // This is executed only on this client
        Debug.Log("Color Selected");
        CmdSelectColor(color);
    }

    [Command]
    void CmdSelectColor(Color color)
    {
        Debug.Log("Executed on server :" + color);
        lobbyUIManager.AddLog("Executed on server :");
        // This is executed on the server
        if (lobbyUIManager.IsColorAvailable(color))
        {
            lobbyUIManager.AddColor(color);
            RpcSelectColor(color);
        }
    }

    [ClientRpc]
    void RpcSelectColor(Color color)
    {
        Debug.Log("Executed on client :" + color);
        // This is executed on all clients
        if (lobbyUIManager.IsColorAvailable(color))
        {
            lobbyUIManager.AddColor(color);
            lobbyUIManager.UpdateColor(color, index);
        }
    }

    void Abc()
    {
        var canvas = GameObject.FindWithTag("Canvas");
        Debug.Log("Canvas is " + canvas);
        /*lobbyUIManager = canvas.GetComponent<LobbyUIManager>();

        lobbyUIManager.AddLog("OnClientEnterRoom");

        if (isLocalPlayer)
        {
            lobbyUIManager.Init(this);
            var buttons = lobbyUIManager.slots;
            for (int i = 0; i < buttons.Length; ++i)
            {
                var _index = i;
                buttons[i].onClick.AddListener(() =>
                {
                    var color = buttons[_index].transform.GetChild(0).GetComponent<Image>().color;
                    SelectColor(color);
                });
            }
        }*/
    }

}
