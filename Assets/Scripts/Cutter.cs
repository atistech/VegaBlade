using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutter : MonoBehaviour
{
    private GameScene gameScene;
    private InputController inputController;

    private Collider lastCollider;
    public Collider lastBombCollider;

    // Start is called before the first frame update
    void Start()
    {
        gameScene = FindObjectOfType<GameScene>();
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
                if (lastBombCollider != collider)
                {
                    lastBombCollider = collider;
                    gameScene.BombCut(collider.gameObject);
                }
            }
            //collider is any cuttable object
            else
            {
                Check(collider);
                gameScene.NormalCut(collider.gameObject);
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
