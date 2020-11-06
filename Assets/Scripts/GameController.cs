using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject lemon;
    public GameObject cucumber;
    public GameObject tomato;
    public GameObject potato;
    public GameObject onion;
    public GameObject bomb;
    public GameObject explosion;
    public Text scoreText;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        GenerateNew();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
    }

    public void BombExplosion(Vector3 position)
    {
        GameObject exp = Instantiate(explosion, position, Quaternion.identity);
        Destroy(exp, 0.75f);
    }

    public void GenerateNew()
    {
        int random = Random.Range(1, 7);

        if (random == 1)
            CreateRigidbody(lemon);
        else if (random == 2)
            CreateRigidbody(cucumber);
        else if (random == 3)
            CreateRigidbody(tomato);
        else if (random == 4)
            CreateRigidbody(potato);
        else if (random == 5)
            CreateRigidbody(onion);
        else if (random == 6)
            CreateRigidbody(bomb);

        Invoke("GenerateNew", 3);
    }
    
    void CreateRigidbody(GameObject param)
    {
        GameObject gameObject = Instantiate(param, new Vector3(-7, -7, 3), Quaternion.identity);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 13, ForceMode.VelocityChange);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 7, ForceMode.VelocityChange);
    } 
}
