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

    public void TestCOR(GameObject MasterObj)
    {
        //PopulateVecListWithRandom(ref MasterObj);

        Master MasterScript = MasterObj.GetComponent<Master>();
        List<Vector3> vecList = MasterScript.vecList;
        float upper = 0.1f;
        float lower = -0.1f;

        double R = 1.0;
        float RR = (float)R;

        float x0 = 1f;
        float y0 = 1f;
        float z0 = 1f;
        System.Random rnd = new System.Random();
        for (int i = 0; i < 100; i++)
        {
            float r = GetRandomFloat(rnd, 0.1f*RR, 0.99f*RR);
            float theta = GetRandomFloat(rnd, 0,  0.99f * 2 * (float)Math.PI);

            float x = (float)(r * Math.Cos(theta) + x0);
            float y = (float)(r * Math.Sin(theta) + y0);
            float z = (float)(Math.Sqrt(R * R - (x - x0) * (x - x0) - (y - y0) * (y - y0)) + z0);
            vecList.Add(new Vector3(x + GetRandomFloat(rnd, lower, upper),
                                    y + GetRandomFloat(rnd, lower, upper),
                                    z + GetRandomFloat(rnd, lower, upper)));
        }
        if (vecList.Count == 0)
        {
            Debug.Log("veclist empty");
        }

        doAssimilateCOR(MasterObj);
        return;
    }

    public void PopulateVecListWithRandom(ref GameObject MasterObj)
    {
        Master MasterScript = MasterObj.GetComponent<Master>();
        List<Vector3> vecList = MasterScript.vecList;
        float upper = 0; //1.0f;
        float lower = 0; //-1.0f;

        double R = 10.0;

        float x0 = 1f;
        float y0 = 1f;
        float z0 = 1f;
        System.Random rnd = new System.Random();
        for (double theta = Math.PI / 10; theta < Math.PI * 0.45; theta += Math.PI / 10)
        {
            for (double r = R / 10; r < R * 0.9; r += R / 10)
            {
                float x = (float)(r * Math.Cos(theta) + x0) + GetRandomFloat(rnd, lower, upper);
                float y = (float)(r * Math.Sin(theta) + y0) + GetRandomFloat(rnd, lower, upper);
                float z = (float)(Math.Sqrt(R * R - (x - x0) * (x - x0) - (y - y0) * (y - y0)) + z0) + GetRandomFloat(rnd, lower, upper);
                vecList.Add(new Vector3(x, y, z));
            }
        }

        return;
    }

    public float GetRandomFloat(System.Random rnd, float lower, float upper)
    {
        return (float)((upper-lower) * rnd.NextDouble() + lower);
    }
}

