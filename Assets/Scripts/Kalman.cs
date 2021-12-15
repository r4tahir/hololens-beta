using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

using vf = MathNet.Numerics.LinearAlgebra.Vector<float>;
using mf = MathNet.Numerics.LinearAlgebra.Matrix<float>;

class Kalman
{
    // state
    private Vector<float> X;
    // state transition model
    private Matrix<float> F;
    // observation model
    private Matrix<float> H;
    // covariance matrix
    private Matrix<float> P;
    // process noise
    private Matrix<float> R; // n_x x n_x
    // observation noise
    private Matrix<float> Q; // n_z x n_z

    private int n_x;
    private int n_z;
    public int count;

    public Vector<float> GetState()
    {
        return X;
    }

    public Matrix<float> GetCovar()
    {
        return P;
    }

    public void Init(float[] _initx, int _initn_z, float[,] _initF, float[,] _initH,
                        float[,] _initP, float _initr, float _initq)
    {
        count = 0;
        X = DenseVector.OfArray(_initx);
        n_x = _initx.Length;
        n_z = _initn_z;
        F = DenseMatrix.OfArray(_initF);
        H = DenseMatrix.OfArray(_initH);
        P = DenseMatrix.OfArray(_initP);
        R = _initr * Matrix<float>.Build.DenseIdentity(n_x);
        Q = _initq * Matrix<float>.Build.DenseIdentity(n_z);
    }

    public void Predict()
    {
        X = F * X;
        P = F * P * F.Transpose() + Q;
    }

    public void Update(Vector<float> Z)
    {
        count++;
        Vector<float> y = Z - H * X;
        Matrix<float> S = H * P * H.Transpose() + R;
        Matrix<float> K = P * H.Transpose() * S.Inverse();
        X = X + K * y;
        P = (Matrix<float>.Build.DenseIdentity(n_z) - K * H) * P;
    }

    public void HololensSpecific()
    {
        float[] _initx = { 0, 0, 0 };
        int _initn_z = 3;
        float[,] _initF = { { 1, 0, 0 },
                            { 0, 1, 0 },
                            { 0, 0, 1 } };
        float[,] _initH = { { 1, 0, 0 },
                            { 0, 1, 0 },
                            { 0, 0, 1 } };
        float var = 10f;
        float[,] _initP = { { var, 0, 0 },
                            { 0, var, 0 },
                            { 0, 0, var } };
        float _initr = 0.5f;
        float _initq = 3f;
        Init(_initx, _initn_z, _initF, _initH, _initP, _initr, _initq);
    }

}
