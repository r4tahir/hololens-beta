using System;
using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;
using UnityEngine.UI;

public class EventObjectScript : MonoBehaviour
{
    public List<Vector3> vecList;
    private int index;
    public List<string> ModelTargetNameList;
    public List<GameObject> ModelTargetList;
    public GameObject tmp;

    void Start()
    {
        tmp = GameObject.Find("Button-ChangeCurrent");

        index = 1;
        ModelTargetNameList = new List<string> { "boneMT" };
        foreach (string s in ModelTargetNameList)
        {
            ModelTargetList.Add(GameObject.Find(s));
        }
        //ModelTargetList.Add(GameObject.Find("pointerMT"));
        foreach (GameObject obj in ModelTargetList)
        {
            Debug.Log(obj.name);
        }
    }

    public void ChangeActiveModelTarget()
    {
        ModelTargetList[index-1].SetActive(false);
        index = index % ModelTargetList.Count +1;
        ModelTargetList[index-1].SetActive(true);

        Debug.Log("i am sad");
        Debug.Log(ModelTargetList[index - 1].name);
        // Text textfield = tmp.GetComponent<Text>();
        // textfield.text = ModelTargetNameList[index - 1];
        
    }

    public void AddPointToVectorList(GameObject MasterObj)
    {
        Debug.Log("adding point");
        GameObject ModelTarget = ModelTargetList[index - 1];

        Status objStatus = ModelTarget.GetComponent<ObserverBehaviour>().TargetStatus.Status;
        //Text textfield = GameObject.Find("/DebugCanvas").GetComponent<Text>();

        if (objStatus == Status.NO_POSE)
        {
            Debug.Log("no pose try again");
            //textfield.text = "no pose try again";
        }
        else
        {
            Debug.Log("pose found");
            //textfield.text = "pose found";

            Vector3 pos = ModelTarget.transform.localPosition;

            //Debug.Log("about to find aarcam");
            GameObject ARCam = GameObject.Find("MixedRealityPlayspace/ARCamera");
            Vector3 ARCPos = ARCam.transform.localPosition;
            Vector3 diff = pos - ARCPos;
            Debug.Log(diff);
            
            //Debug.Log("about to find master veclist")
            // GameObject MasterObj = GameObject.Find("MasterClass");
            Master MasterScript = MasterObj.GetComponent<Master>();
            MasterScript.text1 = diff.ToString("F6");
            MasterScript.vecList.Add(diff);
        }
    }

    public void PrintMasterVectorList(GameObject MasterObj)
    {
        // GameObject MasterObj = GameObject.Find("MasterClass");
        Master MasterScript = MasterObj.GetComponent<Master>();
        List<Vector3> vecList = MasterScript.vecList;
        Debug.Log("printing master list");
        foreach (Vector3 vec in vecList)
        {
            Debug.Log(vec);
        }
        return;
    }

    public void doAssimilateCOR(GameObject MasterObj)
    {
        Vector3 cor = Resources.AssimilateCoR(MasterObj);
        GameObject spherical = GameObject.Find("spherical");
        spherical.transform.localPosition = cor;
        return;
    }

    public void testCOR(GameObject MasterObj)
    {
        Master MasterScript = MasterObj.GetComponent<Master>();
        List<Vector3> vecList = MasterScript.vecList;

        double R = 10.0;

        float x0 = 1;
        float y0 = -5;
        float z0 = -10;
        Vector3 ground_truth = new Vector3(x0, y0, z0);

        for (double theta = Math.PI/10; theta < Math.PI*0.9; theta+= Math.PI/10)
        {
            for (double r= R/10; r < R*0.9; r+= R/10)
            {
                float x = (float)(r * Math.Cos(theta) + x0);
                float y = (float)(r * Math.Sin(theta) + y0);
                Debug.Log(R * R - (x - x0) * (x - x0) - (y - y0) * (y - y0));
                float z = (float)(Math.Sqrt(R*R - (x - x0) * (x - x0) - (y - y0) * (y - y0)) + z0);
                vecList.Add(new Vector3(x, y, z));
            }
        }

        Debug.Log(vecList.Count);
        foreach (Vector3 point in vecList)
        {
            Debug.Log(point.ToString("F6"));
        }

        doAssimilateCOR(MasterObj);
        return;
    }
}
