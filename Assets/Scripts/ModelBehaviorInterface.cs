using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct MBIret
{
    public Status status;
    public Vector3 pos;
    public Quaternion rot;

    public MBIret(
        Status status_,
        Vector3 pos_,
        Quaternion rot_)
    {
        status = status_;
        pos = pos_;
        rot = rot_;
    }
}

public static class ModelBehaviorInterface
{
    public static MBIret getPose(GameObject arCam, GameObject obj)
    {
        obj.SetActive(true);
        System.Threading.Thread.Sleep(10);
        Status objStatus = obj.GetComponent<ObserverBehaviour>().TargetStatus.Status;
        if (objStatus == Status.NO_POSE)
        {
            return new MBIret(
                objStatus, 
                new Vector3(), 
                new Quaternion());
        }
        Vector3 pos = obj.transform.localPosition;
        Quaternion rot = obj.transform.localRotation;

        Vector3 acPos = arCam.transform.localPosition;
        Vector3 diff = pos - acPos;

        obj.SetActive(false);

        return new MBIret(
            objStatus,
            diff,
            rot);
    }
}
