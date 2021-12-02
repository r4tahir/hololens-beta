using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;


using vf = MathNet.Numerics.LinearAlgebra.Vector<float>;
using mf = MathNet.Numerics.LinearAlgebra.Matrix<float>;


public static class Resources
{

    public static Vector3 CenterOfRotation(Vector3 pt1, Vector3 pt2, Vector3 pt3, Vector3 pt4)
    {
        float x1 = pt1.x; float y1 = pt1.y; float z1 = pt1.z;
        float x2 = pt2.x; float y2 = pt2.y; float z2 = pt2.z;
        float x3 = pt3.x; float y3 = pt3.y; float z3 = pt3.z;
        float x4 = pt4.x; float y4 = pt4.y; float z4 = pt4.z;

        float t1 = -(x1 * x1 + y1 * y1 + z1 * z1);
        float t2 = -(x2 * x2 + y2 * y2 + z2 * z2);
        float t3 = -(x3 * x3 + y3 * y3 + z3 * z3);
        float t4 = -(x4 * x4 + y4 * y4 + z4 * z4);

        vf t_ = Numerics.Vector4xFrm4f(t1, t2, t3, t4);
        vf x_ = Numerics.Vector4xFrm4f(x1, x2, x3, x4);
        vf y_ = Numerics.Vector4xFrm4f(y1, y2, y3, y4);
        vf z_ = Numerics.Vector4xFrm4f(z1, z2, z3, z4);
        vf ones_ = vf.Build.Dense(4, 1.0f);

        mf T = mf.Build.DenseOfColumnVectors(x_, y_, z_, ones_);
        float dT = T.Determinant();

        mf D = mf.Build.DenseOfColumnVectors(t_, y_, z_, ones_);
        float dD = 1 / dT * D.Determinant();

        mf E = mf.Build.DenseOfColumnVectors(x_, t_, z_, ones_);
        float dE = 1 / dT * E.Determinant();

        mf F = mf.Build.DenseOfColumnVectors(x_, y_, t_, ones_);
        float dF = 1 / dT * F.Determinant();

        mf G = mf.Build.DenseOfColumnVectors(x_, y_, z_, t_);
        float dG = 1 / dT * G.Determinant();

        return new Vector3(-dD / 2, -dE / 2, -dF / 2);
    }

    public static mf RotationMatrixFromQuaternion(Quaternion q)
    {
        // https://automaticaddison.com/how-to-convert-a-quaternion-to-a-rotation-matrix/

        float q0 = q.w;
        float q1 = q.x;
        float q2 = q.y;
        float q3 = q.z;

        // First row of the rotation matrix
        float r00 = 2 * (q0 * q0 + q1 * q1) - 1;
        float r01 = 2 * (q1 * q2 - q0 * q3);
        float r02 = 2 * (q1 * q3 + q0 * q2);

        // Second row of the rotation matrix;
        float r10 = 2 * (q1 * q2 + q0 * q3);
        float r11 = 2 * (q0 * q0 + q2 * q2) - 1;
        float r12 = 2 * (q2 * q3 - q0 * q1);

        // Third row of the rotation matrix
        float r20 = 2 * (q1 * q3 - q0 * q2);
        float r21 = 2 * (q2 * q3 + q0 * q1);
        float r22 = 2 * (q0 * q0 + q3 * q3) - 1;

        return DenseMatrix.OfArray(new float[,] { { r00, r01, r02 },
                                                  { r10, r11, r12 },
                                                  { r20, r21, r22 } });
    }

    public static Vector3 AssimilateCoR(GameObject MasterObj)
    {
        Vector3 COR;
        Master MasterScript = MasterObj.GetComponent<Master>();
        List<Vector3> vecList = MasterScript.vecList;
        List<Vector3> listCOR = new List<Vector3> {};

        int counter = 0;
        int i = 0;

        while (i < vecList.Count)
        {
            if (i+4 <= vecList.Count)
            {
                listCOR[i] = Resources.CenterOfRotation(vecList[i], vecList[i + 1], vecList[i + 2], vecList[i + 3]);
            }
            i += 4;
            counter++;
        }

        Vector3 sum = new Vector3(0, 0, 0);
        foreach (Vector3 cor in listCOR)
        {
            sum += cor;
        }

        COR = sum / listCOR.Count;

        Debug.Log(COR);
        return COR;
    }
}
