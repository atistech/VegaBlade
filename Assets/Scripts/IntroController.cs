using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public GameObject lemon;
    public GameObject cucumber;
    public TextMesh cucumberText;
    public TextMesh lemonText;

    private EzySliceController ezySlice;

    // Start is called before the first frame update
    void Start()
    {
        ezySlice = FindObjectOfType<EzySliceController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameObject(lemon, 100);
        CheckGameObject(cucumber, 75);
        CheckTextMesh(cucumberText, 50);
        CheckTextMesh(lemonText, 50);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (mousePosition.x < 8 && mousePosition.x > -8)
        {
            if (mousePosition.y < 4 && mousePosition.y > -4)
            {
                transform.position = mousePosition + new Vector3(0, 0, 2);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            collider.gameObject.transform.rotation = Quaternion.identity;
            ezySlice.Cut(collider.gameObject, 2);
            if (collider.gameObject.tag == "Cucumber")
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            else if (collider.gameObject.tag == "Bomb")
                Application.Quit();
        }
    }

    private void CheckGameObject(GameObject obj, int time)
    {
        if (obj != null)
        {
            obj.transform.Rotate(Vector3.up * (time * Time.deltaTime));
        }
    }
    private void CheckTextMesh(TextMesh text, int time)
    {
        if (text != null)
        {
            text.transform.Rotate(Vector3.up * (time * Time.deltaTime));
        }
    }
}
