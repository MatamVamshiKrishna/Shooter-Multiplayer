using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    private RoomPlayer roomPlayer = null;
    private List<Color> colorsSelected = new List<Color>();

    public Button[] slots;
    public Image[] images;
    public Text Log;

    private void Start()
    {
        Debug.Log("Lobby UI Manager");
    }

    public void Init(RoomPlayer roomPlayer)
    {
        Debug.Log(roomPlayer);
        this.roomPlayer = roomPlayer;
    }

    public void OnSlotClicked(Image image)
    {
        roomPlayer.SelectColor(image.color);
    }

    public bool IsColorAvailable(Color color)
    {
        return !colorsSelected.Contains(color);
    }

    public void AddColor(Color color)
    {
        colorsSelected.Add(color);
    }

    public void RemoveColor(Color color)
    {
        colorsSelected.Remove(color);
    }

    public void UpdateColor(Color color, int index)
    {
        Debug.Log("Update color :" + color + "Index " + index);
        images[index].color = color;
    }

    public void AddLog(string str)
    {
        Log.text = str;
    }
}
