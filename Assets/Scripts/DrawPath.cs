using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{

    List<Vector3> posList = new List<Vector3>();
    float l = 0.01f;
    float w = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start drawing");
        GameObject thePart = GameObject.Find("MasterClass");
        drawingTool playerScript = thePart.GetComponent<drawingTool>();
        posList = playerScript.playerPos;

        Color red = Color.red;
        LineRenderer lineRenderer = thePart.GetComponent<LineRenderer>();
        lineRenderer.SetWidth(l, w);
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(red, red);
        

        lineRenderer.SetVertexCount(posList.Count);

        for(int i = 0; i < posList.Count; i++)
        {
            lineRenderer.SetPosition(i, posList[i]);
            Debug.Log("UPDATING"); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
