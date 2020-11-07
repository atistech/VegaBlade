using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public GameObject bomb;
    public GameObject cucumber;

    private EzySliceController ezySlice;
    private SoundController soundController;

    // Start is called before the first frame update
    void Start()
    {
        ezySlice = FindObjectOfType<EzySliceController>();
        soundController = FindObjectOfType<SoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameObject(bomb, 100);
        CheckGameObject(cucumber, 75);
    }

    public void BombCut(GameObject obj)
    {
        obj.transform.rotation = Quaternion.identity;
        ezySlice.Cut(obj, 2);
        soundController.BombEffect();
        Invoke("ApplicationQuit", 0.5f);
    }

    public void NormalCut(GameObject obj)
    {
        obj.transform.rotation = Quaternion.identity;
        ezySlice.Cut(obj, 2);
        soundController.VegaCutEffect();
        Invoke("NextScene", 0.5f);
    }

    private void NextScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private void ApplicationQuit()
    {
        Application.Quit();
    }

    private void CheckGameObject(GameObject obj, int time)
    {
        if (obj != null)
        {
            obj.transform.Rotate(Vector3.up * (time * Time.deltaTime));
        }
    }
}
