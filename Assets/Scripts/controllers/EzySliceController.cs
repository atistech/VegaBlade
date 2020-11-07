using UnityEngine;
using EzySlice;

public class EzySliceController : MonoBehaviour
{
    public Material[] materials = new Material[6];
    private Collider lastCollider;

    public void Cut(GameObject obj, float time)
    {
        Material material = GetMaterial(obj);

        SlicedHull cuttedObject = obj.Slice(transform.position, transform.up, material);

        if (cuttedObject != null)
        {
            GameObject upperHull = cuttedObject.CreateUpperHull(obj, material);
            GameObject lowerHull = cuttedObject.CreateLowerHull(obj, material);

            upperHull.tag = obj.tag;
            lowerHull.tag = obj.tag;

            upperHull.layer = 0;
            lowerHull.layer = 0;

            AddComponent(upperHull, true);
            AddComponent(lowerHull, false);

            Destroy(obj);
            Destroy(upperHull.gameObject, time);
            Destroy(lowerHull.gameObject, time);
        }
    }

    private void AddComponent(GameObject obj, bool upperPiece)
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

    private Material GetMaterial(GameObject gameObject)
    {
        if (gameObject.tag == "Lemon")
            return materials[0];
        else if (gameObject.tag == "Cucumber")
            return materials[1];
        else if (gameObject.tag == "Tomato")
            return materials[2];
        else if (gameObject.tag == "Potato")
            return materials[3];
        else if (gameObject.tag == "Onion")
            return materials[4];
        else
            return materials[5];
    }
}
