using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawingTool : MonoBehaviour
{

    public List<Vector3> playerPos = new List<Vector3>();
    Vector3 pos;
    public GameObject obj;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = obj.transform.position;
        playerPos.Add(pos);

        Debug.Log("tracked");

    }
}
