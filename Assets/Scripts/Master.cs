using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

using System.Threading;

public static class Master

{
    public static List<Vector3> vecList;

    private static Vector3 HipCenter;
    public static Vector3 getHipCenter() { return HipCenter; }
    public static void setHipCenter(Vector3 _HipCenter) { HipCenter = _HipCenter; }

    private static Vector3 DistalFemur;
    public static Vector3 getDistalFemur() { return DistalFemur; }
    public static void setDistalFemur(Vector3 _DistalFemur) { DistalFemur = _DistalFemur; }

    private static Vector3 MedialEpicondyl;
    public static Vector3 getMedialEpicondyl() { return DistalFemur; }
    public static void setMedialEpicondyl(Vector3 _DistalFemur) { DistalFemur = _DistalFemur; }

    private static Vector3 LateralEpicondyl;
    public static Vector3 getLateralEpicondyl() { return LateralEpicondyl; }
    public static void setLateralEpicondyl(Vector3 _LateralEpicondyl) { LateralEpicondyl = _LateralEpicondyl; }


    private static Vector3 MedialGroove;
    public static Vector3 getMedialGroove() { return MedialGroove; }
    public static void setMedialGroove(Vector3 _MedialGroove) { MedialGroove = _MedialGroove; }

    private static Vector3 LateralGroove;
    public static Vector3 getLateralGroove() { return LateralGroove; }
    public static void setLateralGroove(Vector3 _LateralGroove) { LateralGroove = _LateralGroove; }



}
