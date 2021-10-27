using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COR : MonoBehaviour
{
    public Vector3 center;
    public GameObject prefab;

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
        
    }
}
