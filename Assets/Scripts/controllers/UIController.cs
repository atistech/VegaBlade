using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image[] crosses = new Image[3];
    public Text pointText;
    public Text scoreText;
    public GameObject gameOverPanel;

    private int crossCounter = 0;

    public void AddCross()
    {
        if (crossCounter < 3)
        {
            crossCounter++;
            for (int i = 0; i < crossCounter; i++)
            {
                crosses[i].gameObject.SetActive(true);
            }
        }
    }
    
    public void UpdateScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void AddPointText(Vector3 position)
    {
        pointText.gameObject.SetActive(true);
        pointText.transform.position = Camera.main.WorldToScreenPoint(position);
        Invoke("DeletePointText", 0.5f);
    }

    private void DeletePointText()
    {
        pointText.gameObject.SetActive(false);
    }

    public void GameOver(int score)
    {
        gameOverPanel.SetActive(true);
        foreach(Text txt in FindObjectsOfType<Text>())
        {
            if (txt.name == "ScoreResultText")
            {
                txt.text = ""+score;
            }
        }
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
