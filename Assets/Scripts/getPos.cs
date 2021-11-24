using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPos : MonoBehaviour
{
    /* float i = (float)-0.2;
     float j = (float)-0.1;
     float k = (float)1.0; */

    public GameObject obj;

    Vector3 pos;
    Vector3 center = new Vector3((float)-0.2, (float)-0.1, (float)1.0);
    Vector3 size = new Vector3((float)1, (float)1, (float)1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        pos = gameObject.transform.position;
        
        if(pos == center)
        {
            Debug.Log("MATCH PAIR AUTHENTICATION");
            obj.GetComponent<MeshRenderer>().material.color = Color.green;
            obj.transform.localScale = size;
        }

    }
}
