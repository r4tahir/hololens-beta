using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{

    List<Vector3> posList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start drawing");
        GameObject thePart = GameObject.Find("MasterClass");
        FindPos playerScript = thePart.GetComponent<FindPos>();
        posList = playerScript.vecList;

        Color red = Color.red;
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(red, red);
        lineRenderer.SetWidth(0.2F, 0.2F);

        lineRenderer.SetVertexCount(posList.Count);

        for(int i = 0; i < posList.Count; i++)
        {
            lineRenderer.SetPosition(i, posList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("UPDATING");
    }
}
