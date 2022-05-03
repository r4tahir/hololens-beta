using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COR : MonoBehaviour
{
    public Vector3 center;
    public GameObject prefab;

    public Vector3 startPos;
    public Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        GameObject thePart = GameObject.Find("MasterClass");
        FindPos playerScript = thePart.GetComponent<FindPos>();
        center = playerScript.asdf;

        GameObject obj = Instantiate(prefab, center, Quaternion.Euler(0f, 0f, 0f));
        obj.name = "COR Sphere";
    }

    // Update is called once per frame
    void Update()
    {
        var go = GameObject.Find("EmptyBoss");

        var startPoint = GameObject.Find("COR Sphere");
        var endPoint = GameObject.Find("EndSphere");

        var lineRend = go.AddComponent<LineRenderer>();

        go.GetComponent<LineRenderer>().SetWidth(0.05f, 0.05f);

        startPos = startPoint.transform.position;
        endPos = endPoint.transform.position;

        lineRend.SetPosition(0, startPos);
        lineRend.SetPosition(1, endPos);
    }
}
