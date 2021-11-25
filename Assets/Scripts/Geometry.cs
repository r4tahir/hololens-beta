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



public static class Geometry
{
    // in meters
    private static readonly float[] PointerPos = { 0.12f, 0, 0 };

    public static vf GetPointerPos()
    {
        return DenseVector.OfArray(PointerPos);
    }
}
