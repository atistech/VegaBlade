using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutter : MonoBehaviour
{
    private GameScene gameScene;
    private IntroScene introScene;
    private InputController inputController;

    private Collider lastCollider;

    // Start is called before the first frame update
    void Start()
    {
        gameScene = FindObjectOfType<GameScene>();
        introScene = FindObjectOfType<IntroScene>();
        inputController = FindObjectOfType<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        inputController.Move(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //collider is cuttable
        if (collider.gameObject.layer == 8)
        {
            //collider is bomb
            if (collider.tag == "Bomb")
            {
                //active scene is "GameScene"
                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    gameScene.BombCut(collider.gameObject);
                }
                //active scene is "IntroScene"
                else
                {
                    introScene.BombCut(collider.gameObject);
                }
                
            }
            //collider is any cuttable object
            else
            {
                //active scene is "GameScene"
                if (SceneManager.GetActiveScene().name == "GameScene")
                {
                    Check(collider);
                    gameScene.NormalCut(collider.gameObject);
                }
                //active scene is "IntroScene"
                else
                {
                    introScene.NormalCut(collider.gameObject);
                }
            }
        }
    }

    private void Check(Collider collider)
    {
        if (lastCollider != collider)
        {
            gameScene.AddScore();
            lastCollider = collider;
        }
        else
        {
            lastCollider = null;
        }
    }
}
