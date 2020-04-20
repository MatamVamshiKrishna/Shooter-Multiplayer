using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Selection : NetworkBehaviour
{
    public NetworkRoomPlayer RoomPlayer;
    public Sprite[] sprites;
    private Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };

    void Start()
    {
        if(isLocalPlayer)
        {
            var panel = GameObject.FindWithTag("Panel");
            var buttons = panel.GetComponentsInChildren<Button>();
            for(int i=0;i<buttons.Length;++i)
            {
                var index = i;
                buttons[i].onClick.AddListener(() =>
                {
                    SelectCharacter(index);
                });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectCharacter(int index)
    {
        Debug.Log(index);
        CmdOnCharacterSelected(index);
    }

    [Command]
    void CmdOnCharacterSelected(int index)
    {
        var tempObj = GameObject.FindWithTag("Temp");
        var selected = tempObj.GetComponent<Temp>().selected;
        if(!selected.Contains(index))
        {
            selected.Add(index);

            // storing index
            RpcOnCharacterSelected(index);
        }

    }

    [ClientRpc]
    void RpcOnCharacterSelected(int index)
    {
        var tempObj = GameObject.FindWithTag("Temp");
        tempObj.GetComponent<Temp>().selected.Add(index);

        var panel = GameObject.FindWithTag("Panel");
        var buttons = panel.GetComponentsInChildren<Button>();
        buttons[index].gameObject.SetActive(false);
        /*var temp = GameObject.FindWithTag("Temp").GetComponent<Temp>();
        foreach (var ind in temp.selected)
        {
            Debug.Log(ind);
        }*/
        UpdateCharacter(index);
    }

    void UpdateCharacter(int index)
    {
        var panel = GameObject.FindWithTag("Panel");
        var images = panel.GetComponentsInChildren<ImageT>();
        Debug.Log(images.Length);
        var roomPlayer = GetComponent<NetworkRoomPlayer>();
        images[roomPlayer.index].image.color = colors[index];
        /*if(isLocalPlayer)
        {
            images[0].image.color = colors[index];
        }
        else
        {
            images[1].image.sprite = sprites[index];
        }*/
    }

}
