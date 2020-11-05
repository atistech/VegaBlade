using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Cutter : MonoBehaviour
{
    public Material lemonMaterial;
    public Material cucumberMaterial;
    public Material tomatoMaterial;
    public Material potatoMaterial;
    public Material onionMaterial;

    private GameController controller;
    private UIController ui;

    private Collider lastCollider;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        ui = FindObjectOfType<UIController>();
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            ui.AddPointText(collision.transform.position);
            ui.UpdateCrossPanel(1);

            if (lastCollider != collision)
            {
                controller.score++;
                lastCollider = collision;
            }
            else
            {
                lastCollider = null;
            }

            Material material = GetMaterial(collision.gameObject);

            SlicedHull cuttedObject = Cut(collision.gameObject, material);
            if (cuttedObject != null)
            {
                GameObject upperHull = cuttedObject.CreateUpperHull(collision.gameObject, material);
                GameObject lowerHull = cuttedObject.CreateLowerHull(collision.gameObject, material);

                upperHull.tag = collision.gameObject.tag;
                lowerHull.tag = collision.gameObject.tag;

                upperHull.layer = 0;
                lowerHull.layer = 0;

                AddComponent(upperHull, true);
                AddComponent(lowerHull, false);

                Destroy(collision.gameObject);
                Destroy(upperHull.gameObject, 4);
                Destroy(lowerHull.gameObject, 4);
            }
        }
    }

    //Gameobject slice method
    SlicedHull Cut(GameObject obj, Material material)
    {
        //Return slice as vertical axis
        return obj.Slice(transform.position, transform.up, material);
    }

    //Add the wood object functionalities to GameObject method
    void AddComponent(GameObject obj, bool upperPiece)
    {
        if (obj.tag == "Cucumber")
        {
            obj.AddComponent<CapsuleCollider>();
        }
        else
        {
            obj.AddComponent<SphereCollider>();
        }

        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        obj.GetComponent<Rigidbody>().AddForce(transform.up * 5, ForceMode.VelocityChange);


        if (upperPiece)
        {
            obj.GetComponent<Rigidbody>().AddForce(transform.right * 5, ForceMode.VelocityChange);
            obj.transform.rotation = Quaternion.Euler(60, 45, 0);
        }
        else
        {
            obj.GetComponent<Rigidbody>().AddForce(transform.right * -5, ForceMode.VelocityChange);
            obj.transform.rotation = Quaternion.Euler(-60, -45, 0);
        }
    }

    Material GetMaterial(GameObject gameObject)
    {
        if(gameObject.tag == "Lemon")
        {
            return lemonMaterial;
        }
        else if(gameObject.tag == "Cucumber")
        {
            return cucumberMaterial;
        }
        else if (gameObject.tag == "Tomato")
        {
            return tomatoMaterial;
        }
        else if (gameObject.tag == "Potato")
        {
            return potatoMaterial;
        }
        else
        {
            return onionMaterial;
        }
    }
}
