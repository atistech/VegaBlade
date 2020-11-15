using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public void StartButtonClick()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
