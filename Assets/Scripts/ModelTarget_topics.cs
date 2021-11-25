using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

using vf = MathNet.Numerics.LinearAlgebra.Vector<float>;
using mf = MathNet.Numerics.LinearAlgebra.Matrix<float>;

public class ModelTarget_topics : MonoBehaviour
{
    public int timer;
    public int counter;
    public bool filtered;
    List<Vector3> newlist;
    public Vector3 asdf;

    public GameObject arCam;
    public GameObject boneMT;
    public GameObject boneMTModel;
    public GameObject pointerMT;
    public GameObject pointerMTModel;

    public Vector3 arcPos;
    public Vector3 boneMTPos;
    public Vector3 boneDX;
    public Vector3 pointerMTPos;
    public Vector3 pointerDX;
    
    public Quaternion boneMTRot;
    public Quaternion pointerMTRot;

    public float testvar;
    public List<Vector3> vecList;
    public int n_data;
    public Vector3 CenterOfRotation;

    public ModelTargetBehaviour[] modelTargetBehaviours;

    public float getTestVar()
    {
        return testvar;
    }

    void Awake()
    {
        arCam = GameObject.Find("/ARCamera");

        boneMT = GameObject.Find("/boneModelTarget");
        boneMTModel = GameObject.Find("/boneMTModel");

        pointerMT = GameObject.Find("/pointerModelTarget");
        pointerMTModel = GameObject.Find("/pointerMTModel");
        //modelTargetBehaviours = FindObjectsOfType<ModelTargetBehaviour>();

        vecList = new List<Vector3>();
        testvar = 1;
        n_data = 0;
        timer = 0;

    }

    // Start is called before the first frame update
    void Start()
    {
        // generateSphericalPoints();
        // CenterOfRotation = collectCOR(vecList);
        counter = 0;
        filtered = false;
    }



    // Update is called once per frame
    void Update()
    {
        {
            /*
            boneMT.SetActive(false);
            boneMTPos =  boneMT.transform.localPosition;
            boneMTRot = boneMT.transform.localRotation;

            pointerMTPos = pointerMT.transform.localPosition;
            pointerMTRot = pointerMT.transform.localRotation;

            arcPos =  arCam.transform.localPosition;

            Vector3 diff = boneMTPos-arcPos;
            float flength = getL2(boneMTPos, arcPos);
            string slength = string.Format("{0:N5}", flength);

            mf pointerRM = Resources.RotationMatrixFromQuaternion(pointerMTRot);
            Vector3 pointerEnd = Numerics.Vector3FrmVector3x(pointerRM * Geometry.GetPointerPos());


            Status boneStatus = boneMT.GetComponent<ObserverBehaviour>().TargetStatus.Status;
            Status pointerStatus = pointerMT.GetComponent<ObserverBehaviour>().TargetStatus.Status;

            if (boneStatus == Status.NO_POSE)
            {
                boneMTModel.SetActive(false);
            }
            else if (boneStatus == Status.TRACKED)
            {
                boneMTModel.SetActive(true);
            }

            if (pointerStatus == Status.NO_POSE)
            {
                pointerMTModel.SetActive(false);
            }
            else if (pointerStatus == Status.TRACKED)
            {
                pointerMTModel.SetActive(true);
            }

            Debug.Log("number in container: " + modelTargetBehaviours.Length);
            foreach (var modelTargetBehaviour in modelTargetBehaviours)
            {
                //foreach (var i in modelTargetBehaviour.GetAvailableStateNames())
                //{
                //    Debug.Log("i: " + i);
                //}
                Debug.Log("motionhint: " + modelTargetBehaviour.GetMotionHint());
            }
            */
            MBIret ret = ModelBehaviorInterface.getPose(arCam, pointerMT);
            //Debug.Log("current: " + pointerMT.GetComponent<ObserverBehaviour>().TargetStatus.Status);
            if (ret.status == Status.TRACKED)
            {
                Debug.Log("returned: " + ret.status);
                Debug.Log(ret.pos);
                Debug.Log(ret.rot);
                pointerMTModel.SetActive(true);
                pointerMTModel.transform.localRotation = pointerMTRot;
                pointerMTModel.transform.localPosition = pointerMTPos;
            }
            else
            {
                pointerMTModel.SetActive(false);
            }


            //boneMTModel.transform.localRotation = boneMTRot;
            //boneMTModel.transform.localPosition = boneMTPos;
            //
            //pointerMTModel.transform.localRotation = pointerMTRot;
            //pointerMTModel.transform.localPosition = pointerMTPos;



            /*
            if ((++timer % 200 == 0) && (counter < 40))
            {
                Debug.Log(counter);
                vecList.Add(diff);
                counter++;
            } 
            else if (!filtered)
            {
                Debug.Log("filtering list");
                vecList = filterList(vecList);
                asdf = collectCOR(vecList);
                Debug.Log("filtering list");
                filtered = true;
            }
            */
        }
    }

    List<Vector3> filterList(List<Vector3> vecList)
    {
        var newPosList = new List<Vector3>();
        var set = new HashSet<Vector3>();

        for(int i = 0; i < vecList.Count; i++)
        {
            if (!set.Contains(vecList[i]))
            {
                newPosList.Add(vecList[i]);
                //record as future duplicate
                set.Add(vecList[i]);
            }
        }

        List<Vector3> posListTwo = vecList.Distinct().ToList();
        return newPosList;
    }

    public float getL2(Vector3 vec1, Vector3 vec2)
    {
        Vector3 v3temp = vec1-vec2;
        float x0 = v3temp.x;
        float y0 = v3temp.y;
        float z0 = v3temp.z;
        float ftemp = x0*x0 + y0*y0 + z0*z0;
        return Mathf.Sqrt(ftemp);
    }


    public Vector3 collectCOR(List<Vector3> vecList)
    {
            int n_data = vecList.Count;
            int count = (int)Math.Floor((double)n_data / 4.0d);
            n_data = count * 4;
            Debug.Log(n_data);
            int index = 0;
            List<Vector3> corList = new List<Vector3>(new Vector3[count]);

            for (int i = 0; i < count * 4; i+= 4)
            {
                Vector3 point = Resources.CenterOfRotation(
                    vecList[i],
                    vecList[i + 1],
                    vecList[i + 2],
                    vecList[i + 3]);
                corList[index++] = point;
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
        Vector3 ground_truth = new Vector3(0,0,0);
        float radius = 20;
        for (double theta = 0; theta < Math.PI; theta += Math.PI/10) {
            for (double r = 0; r < radius; r += radius/10) {
                float x = (float) (radius * Math.Cos(theta) + ground_truth.x);
                float y = (float) (radius * Math.Sin(theta) + ground_truth.y);
                float z = getSphereZ(ground_truth, radius, x, y);
                vecList.Add(new Vector3(x,y,z));
                n_data++;
                Debug.Log("temp" + new Vector3(x,y,z));
            }
        }
        Debug.Log("generated sphereical points: " + vecList.Count);
    }
    private float getSphereZ(Vector3 gt, float r, float x, float y)
    {   
        float x0 = gt.x;
        float y0 = gt.y;
        float z0 = gt.z;
        return Mathf.Sqrt(r*r - (x-x0)*(x-x0) - (y-y0)*(y-y0)) + z0;
    }

}
