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

// api reference: https://numerics.mathdotnet.com/

public static class Numerics
{
    // Numerics from floats (3x)
    public static vf Vector3xFrm3f(float a, float b, float c)
    {
        return DenseVector.OfArray(new float[] { a, b, c });
    }

    // Numerics from floats (4x)
    public static vf Vector4xFrm4f(float a, float b, float c, float d)
    {
        return DenseVector.OfArray(new float[] { a, b, c, d });
    }

    // Numerics from unity (3x)
    public static vf Vector3xFrmVector3(Vector3 v)
    {
        return DenseVector.OfArray(new float[] { v.x, v.y, v.z });
    }

    // Numerics from unity (quaternion)
    public static vf Vector4xFrmQuaternion(Quaternion q)
    {
        return DenseVector.OfArray(new float[] { q.w, q.x, q.y, q.z });
    }

    // Unity from numerics (3x)
    public static Vector3 Vector3FrmVector3x(vf v)
    {
        return new Vector3(v[0], v[1], v[2]);
    }


    public static mf Matrix3x3Frm9f(float a, float b, float c, float d, float e, float f, float g, float h, float i)
    {
        return DenseMatrix.OfArray(new float[,] { { a, b, c }, 
                                                  { d, e, f }, 
                                                  { g, h, i } });
    }
    public static mf Matrix3x3Frm3Vector3(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        return DenseMatrix.OfArray(new float[,] { {v1.x, v2.x, v3.x}, 
                                                  {v1.y, v2.y, v3.y}, 
                                                  {v1.z, v2.z, v3.z} });
    }
}
