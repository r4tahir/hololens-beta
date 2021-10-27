using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPos : MonoBehaviour
{
    GameObject obj;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
        Debug.Log(pos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
