using System.Collections.Generic;
using UnityEngine;

public class ObjectsBetweenCameraAndObject : MonoBehaviour
{
    public GameObject player;
    public static Dictionary<string, GameObject> objectsHit = new Dictionary<string, GameObject> { };

    void FixedUpdate()
    {
        //float dist = Vector3.Distance(transform.position, player.transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 100f);
        Debug.DrawRay(transform.position, transform.forward, Color.yellow, 1f);

        foreach (KeyValuePair<string, GameObject> kvpGo in objectsHit)
        {
            var r = kvpGo.Value.GetComponent<Renderer>();
            //r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 1f);
            r.enabled = true;
        }

        foreach (RaycastHit h in hits)
        {
            var objectHit = h.collider.gameObject;

            if (objectHit.tag != "AutoHide") continue;

            var ohr = objectHit?.GetComponent<Renderer>();

            if (!objectsHit.ContainsKey(objectHit.name)) objectsHit.Add(objectHit.name, objectHit);

            Debug.Log(objectHit.name);
            Debug.DrawRay(objectHit.transform.position, objectHit.transform.forward, Color.red, 1f);

            //ohr.material.color = new Color(ohr.material.color.r, ohr.material.color.g, ohr.material.color.b, .2f);
            ohr.enabled = false;
        }
    }
}
