using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour
{
    [Header("Materials")]
    public Material outerMaterial;
    public Material innerMaterial;

    [Header("Effect")]
    public GameObject effect;
    public Color effectColor;

    public void RunRigidbodyAnimation(GameObject upper, GameObject lower, float time)
    {
        upper.tag = tag;
        lower.tag = tag;

        //set the layer as 0 to not cuttable
        upper.layer = 0;
        lower.layer = 0;

        AddComponent(upper, 1);
        AddComponent(lower, -1);

        AddEffect();

        Destroy(gameObject);
        Destroy(upper.gameObject, time);
        Destroy(lower.gameObject, time);
    }

    private void AddComponent(GameObject obj, int sign)
    {
        obj.transform.position = obj.transform.position + new Vector3(0, 0, 2);
        
        //game object is cucumber
        if (obj.tag == "Cucumber")
        {
            //cucumber has capsule collider
            obj.AddComponent<CapsuleCollider>();
            obj.GetComponent<CapsuleCollider>().isTrigger = true;
        }
        //game object is others
        else
        {
            //others have sphere collider
            obj.AddComponent<SphereCollider>();
            obj.GetComponent<SphereCollider>().isTrigger = true;
        }

        //rotate game object to see inside
        obj.transform.rotation = Quaternion.Euler(sign * 60, sign * 45, 0);

        //add rigidbody component with props to game object
        AddRigidbodyProperties(obj, sign);
    }

    private void AddRigidbodyProperties(GameObject obj, int sign)
    {
        //add a rigidbody component to game object
        Rigidbody temp = obj.AddComponent<Rigidbody>();
        //set this rigidbody component as interpolate
        temp.interpolation = RigidbodyInterpolation.Interpolate;
        //add up force to this rigidbody component
        temp.AddForce(transform.up * 5, ForceMode.Impulse);
        //add right or left force to this rigidbody component
        temp.AddForce(transform.right * sign * 5, ForceMode.Impulse);
    }

    private void AddEffect()
    {
        // this game object is not bomb
        if (tag != "Bomb")
        {
            //create new effectObject
            GameObject effectObject = Instantiate(effect, transform.position + new Vector3(0, 0, 4), Quaternion.identity);
            //the color of effectObject set according to related effectColor
            effectObject.GetComponent<SpriteRenderer>().color = effectColor;
            //remove this effectObject in half of a second
            Destroy(effectObject, 2);
        }
    }
}
