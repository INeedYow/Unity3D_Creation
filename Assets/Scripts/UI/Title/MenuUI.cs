using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene("DungeonScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
