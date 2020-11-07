using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public void Move(GameObject obj)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < 8 && mousePosition.x > -8)
        {
            if (mousePosition.y < 4 && mousePosition.y > -4)
            {
                obj.transform.position = mousePosition + new Vector3(0, 0, 2);
            }
        }
    }
}
