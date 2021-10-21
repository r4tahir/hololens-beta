using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Vuforia;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading.Tasks;


public class FindPos : MonoBehaviour
{
    public int timer;
    public int counter;
    public bool filtered;
    List<Vector3> newlist;
    public Vector3 asdf;

    public GameObject gameObject;

    public Vector3 pos;
    public List<Vector3> vecList;
    public int n_data;
    public Vector3 CenterOfRotation;

    void Awake()
    {
        vecList = new List<Vector3>();
        n_data = 0;
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start logging");

      
        float offset = 0.5f;
        vecList.Add(new Vector3(0.2f + offset, 0.2f + offset, 0.959166305f + offset));
        vecList.Add(new Vector3(0.2f + offset, 0.3f + offset, 0.932737905f + offset));
        vecList.Add(new Vector3(0.3f + offset, 0.2f + offset, 0.932737905f + offset));
        vecList.Add(new Vector3(0.3f + offset, 0.3f + offset, 0.905538514f + offset));

        counter = 0;
        filtered = false;
        int i = 0;
        Point point = evaluateCOR(
            new Point(vecList[i]),
            new Point(vecList[i + 1]),
            new Point(vecList[i + 2]),
            new Point(vecList[i + 3]));

        CenterOfRotation = new Vector3(point.x, point.y, point.z);

    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.transform.position;

        if ((++timer % 100 == 0) && (counter < 20))
        {
            Debug.Log(counter);
            vecList.Add(pos);
            Debug.Log(pos);
            //Debug.Log(string.Format(string.Format("{0:N4}", moTar.transform.position)));
            counter++;
        }
        else //if (!filtered)
        {
            Debug.Log("filtering list");
            vecList = filterList(vecList);
            asdf = collectCOR(vecList);
            filtered = true;
        }
    }

    List<Vector3> filterList(List<Vector3> vecList)
    {
        var newPosList = new List<Vector3>();
        var set = new HashSet<Vector3>();

        for (int i = 0; i < vecList.Count; i++)
        {
            if (!set.Contains(vecList[i]))
            {
                newPosList.Add(vecList[i]);
                //record as future duplicate
                set.Add(vecList[i]);
            }
        }

        //Debug.Log(newPosList.Count);
        List<Vector3> posListTwo = vecList.Distinct().ToList();
        //Debug.Log(posListTwo.Count);
        return newPosList;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Point
    {
        public float x;
        public float y;
        public float z;
        public Point(Vector3 vec)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
        }
        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [DllImport("Resources", CallingConvention = CallingConvention.Cdecl)]
    public static extern Point evaluateCOR(Point p1, Point p2, Point p3, Point p4);

    public Vector3 collectCOR(List<Vector3> vecList)
    {
        int n_data = vecList.Count;
        int count = (int)Math.Floor((double)n_data / 4.0d);
        n_data = count * 4;
        Debug.Log(n_data);
        int index = 0;
        List<Vector3> corList = new List<Vector3>(new Vector3[count]);

        for (int i = 0; i < count * 4;)
        {
            Point point = evaluateCOR(
                new Point(vecList[i]),
                new Point(vecList[i + 1]),
                new Point(vecList[i + 2]),
                new Point(vecList[i + 3]));
            corList[index++] = new Vector3(point.x, point.y, point.z);
            i += 4;
        }

        foreach (Vector3 vec in corList)
        {
            Debug.Log(string.Format("{0:N4}", vec));
        }

        float x_ave = 0;
        float y_ave = 0;
        float z_ave = 0;

        // foreach (var p in corList)
        // {
        //     x_ave += p.x;
        //     y_ave += p.y;
        //     z_ave += p.z;
        // }

        x_ave /= corList.Count;
        y_ave /= corList.Count;
        z_ave /= corList.Count;

        return new Vector3(x_ave, y_ave, z_ave);
    }

    private void generateSphericalPoints()
    {
        Vector3 ground_truth = new Vector3(0, 0, 0);
        float radius = 20;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float x = i / radius + ground_truth.x;
                float y = j / radius + ground_truth.y;
                float z = getSphereZ(ground_truth, radius, x, y);
                vecList.Add(new Vector3(x, y, z));
                n_data++;
                Debug.Log("temp" + new Vector3(x, y, z));
            }
        }
        Debug.Log("generated sphereical points: " + vecList.Count);
    }
    private float getSphereZ(Vector3 gt, float r, float x, float y)
    {
        r = r / r;
        float x0 = gt.x;
        float y0 = gt.y;
        float z0 = gt.z;
        return Mathf.Sqrt(r * r - (x - x0) * (x - x0) - (y - y0) * (y - y0)) + z0;
    }

}
