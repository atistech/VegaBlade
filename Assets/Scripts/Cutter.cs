using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Cutter : MonoBehaviour
{
    private GameController controller;
    private UIController ui;
    private EzySliceController ezySlice;

    private Collider lastCollider;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        ui = FindObjectOfType<UIController>();
        ezySlice = FindObjectOfType<EzySliceController>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //Collider is cuttable
        if (collider.gameObject.layer == 8)
        {
            //Collider is bomb
            if (collider.tag == "Bomb")
            {
                ezySlice.Cut(collider.gameObject, 0.25f);
                controller.BombExplosion(collider.transform.position);
                ui.AddCross();
            }
            //Collider is any cuttable object
            else
            {
                ui.AddPointText(collider.transform.position);
                
                if (lastCollider != collider)
                {
                    controller.score++;
                    lastCollider = collider;
                }
                else
                {
                    lastCollider = null;
                }

                ezySlice.Cut(collider.gameObject, 4);
            }
        }
    }
}
