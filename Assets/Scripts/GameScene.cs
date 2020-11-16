using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class GameScene : MonoBehaviour
{
    public GameObject lemon;
    public GameObject cucumber;
    public GameObject tomato;
    public GameObject potato;
    public GameObject onion;
    public GameObject bomb;
    public GameObject explosion;

    private UIController uiController;
    private SoundController soundController;

    private int score = 0;
    private int crossCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        soundController = FindObjectOfType<SoundController>();
        GenerateNew();
    }

    // Update is called once per frame
    void Update()
    {
        //update game score
        uiController.UpdateScore(score);

        //destroy uncutted game objects
        RemoveUncuttedObjects();


        //check how many bomb cutted
        if (crossCounter == 3)
        {
            //show game over panel
            uiController.GameOver(score);
        }
    }

    public void BombCut(GameObject obj)
    {
        //play bomb sound effect
        soundController.BombEffect();
        //cut the game object with destroy in 4 seconds
        Cut(obj, 0.25f);
        //add the explosion object
        GameObject exp = Instantiate(explosion, 
            obj.transform.position, Quaternion.identity);
        //destroy explosion object in 1/4 of a second
        Destroy(exp, 0.75f);
        //show the cross image
        uiController.AddCross();
        //cross counter increase
        crossCounter++;
    }

    public void NormalCut(GameObject obj)
    {
        //play vega cut sound effect
        soundController.VegaCutEffect();
        //show point text on the cutting position
        uiController.AddPointText(obj.transform.position);
        //cut the game object with destroy in 4 seconds
        Cut(obj, 4);
    }

    public void AddScore()
    {
        //score increase by one
        score+=10;
    }

    private void GenerateNew()
    {
        if (crossCounter < 3)
        {
            int random = Random.Range(1, 4);

            StartCoroutine(RandomCreateRigidbody(random)); 

            Invoke("GenerateNew", 2f);
        }
    }

    private GameObject RandomGameObject()
    {
        int random = Random.Range(1, 7);

        if (random == 1)
            return lemon;
        else if (random == 2)
            return cucumber;
        else if (random == 3)
            return tomato;
        else if (random == 4)
            return potato;
        else if (random == 5)
            return onion;
        else if (random == 6)
            return bomb;
        else
            return null;
    }

    private IEnumerator RandomCreateRigidbody(int param)
    {
        float delay = 0.3f;

        int[] array = { -2, 0, 1 };
        if (param == 1)
        {
            int random = Random.Range(0, 3);
            CreateRigidbody(RandomGameObject(), array[random]);
        }
        if (param == 2)
        {
            int random = Random.Range(0, 3);
            CreateRigidbody(RandomGameObject(), array[random]);
            int random2 = Random.Range(0, 3);
            while (random == random2)
            {
                random2 = Random.Range(0, 3);
            }
            yield return new WaitForSeconds(delay);
            CreateRigidbody(RandomGameObject(), array[random2]);
        }
        if (param == 3)
        {
            CreateRigidbody(RandomGameObject(), array[0]);
            yield return new WaitForSeconds(delay);
            CreateRigidbody(RandomGameObject(), array[1]);
            yield return new WaitForSeconds(delay);
            CreateRigidbody(RandomGameObject(), array[2]);
        }
    }
    
    private void CreateRigidbody(GameObject obj, int random)
    {
        //add new game object
        GameObject gameObject = Instantiate(obj, new Vector3(random, -7, 3), Quaternion.identity);
        //set name as "CuttableObject"
        gameObject.name = "CuttableObject";
        //add up force with 13x
        gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 14, ForceMode.VelocityChange);

        int random2 = Random.Range(1, 3);
        
        if (random2 == 1)
        {
            //add right force with 7x
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 3, ForceMode.VelocityChange);
        }
        else if (random2 == 2)
        {
            //add right force with 7x
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * -3, ForceMode.VelocityChange);
        }
    }

    private void RemoveUncuttedObjects()
    {
        //find all game objects in foreach
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            //game object has "CuttableObject" name
            if (obj.name == "CuttableObject")
            {
                //destroy game object in 4 seconds
                Destroy(obj, 4);
            }
        }
    }

    private void Cut(GameObject obj, float time)
    {
        CuttableObject cut = obj.GetComponent<CuttableObject>();

        SlicedHull cuttedObject = obj.Slice(obj.transform.position + new Vector3(0.01f, 0.01f, 0.01f), transform.up);

        if (cuttedObject != null)
        {
            GameObject upperHull = cuttedObject.CreateUpperHull(obj, cut.innerMaterial);
            GameObject lowerHull = cuttedObject.CreateLowerHull(obj, cut.innerMaterial);

            cut.RunRigidbodyAnimation(upperHull, lowerHull, time);
        }
    }
}
