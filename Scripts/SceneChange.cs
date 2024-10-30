using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void onMenuClick()
    {
        SceneManager.LoadScene("Start");
    }

    public void onRestartClick()
    {
        SceneManager.LoadScene("Game");
    }
}
