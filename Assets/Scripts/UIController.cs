using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image[] crosses = new Image[3];
    public TextMesh pointText;

    public void UpdateCrossPanel(int length)
    {
        for (int i = 0; i < length; i++)
        {
            crosses[i].gameObject.SetActive(true);
        }
    }
    
    public void AddPointText(Vector3 position)
    {
        pointText.gameObject.SetActive(true);
        pointText.transform.position = position;
        Invoke("DeletePointText", 0.5f);
    }

    private void DeletePointText()
    {
        pointText.gameObject.SetActive(false);
    }
}
