using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

using System.Threading;

public class Master : MonoBehaviour
{
    public Master instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public List<Vector3> vecList;

    private Vector3 HipCenter;
    public Vector3 getHipCenter() { return HipCenter; }
    public void setHipCenter(Vector3 _HipCenter) { HipCenter = _HipCenter; }

    private Vector3 DistalFemur;
    public Vector3 getDistalFemur() { return DistalFemur; }
    public void setDistalFemur(Vector3 _DistalFemur) { DistalFemur = _DistalFemur; }

    private Vector3 MedialEpicondyl;
    public Vector3 getMedialEpicondyl() { return DistalFemur; }
    public void setMedialEpicondyl(Vector3 _DistalFemur) { DistalFemur = _DistalFemur; }

    private Vector3 LateralEpicondyl;
    public Vector3 getLateralEpicondyl() { return LateralEpicondyl; }
    public void setLateralEpicondyl(Vector3 _LateralEpicondyl) { LateralEpicondyl = _LateralEpicondyl; }


    private Vector3 MedialGroove;
    public Vector3 getMedialGroove() { return MedialGroove; }
    public void setMedialGroove(Vector3 _MedialGroove) { MedialGroove = _MedialGroove; }

    private Vector3 LateralGroove;
    public Vector3 getLateralGroove() { return LateralGroove; }
    public void setLateralGroove(Vector3 _LateralGroove) { LateralGroove = _LateralGroove; }



}
