using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public static Color color;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonClick(Image image)
    {
        color = image.color;
        /*PlayerPrefs.SetFloat("r", image.color.r);
        PlayerPrefs.SetFloat("g", image.color.g);
        PlayerPrefs.SetFloat("b", image.color.b);
        PlayerPrefs.SetFloat("a", image.color.a);*/
    }

    public void OpenOffline()
    {
        SceneManager.LoadScene(1);
    }
}
