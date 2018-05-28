using OpenTK;
using System;

class Camera
{
    public Vector3 Position = new Vector3(5, 5, 0.5f), Direction = new Vector3(0, 0, 1), ScreenCentre, NormDirection, P0, P1, P2;
    public Vector3 Right = new Vector3(1, 0, 0), Up = new Vector3(0, 1, 0);
    public float FOV = 180, ScreenWidth, DistanceToOrigin;
    static Camera C = new Camera();
    float x, y, z;
    public Matrix3 RotateX, RotateY, RotateZ;
    Vector3 V = new Vector3(0, 0, 1), upp = new Vector3(0, 1, 0), UnitR, UnitU, test, test1;
    public double B = 10;
    public Camera()
    {
        double a = Math.Asin((FOV / 180) * Math.PI);
        test = Vector3.Cross(V, upp);
        test1 = Vector3.Normalize(test);
        UnitR = Vector3.Divide(Vector3.Cross(V, upp), Vector3.Normalize(Vector3.Cross(V, upp)));
        UnitU = Vector3.Cross(test1, V);
        ScreenWidth = 2;
        DistanceToOrigin = (float)(1 / Math.Sin(((FOV / 2) / 180) * Math.PI));
        NormDirection = Vector3.Normalize(Direction);
        Position = Position;// * RotateX;
        ScreenCentre = Position + Direction * DistanceToOrigin;
        P0 = (ScreenCentre - test1 + UnitU);// * RotateX;
        P1 = (ScreenCentre + test1 + UnitU);// *  RotateX;
        P2 = (ScreenCentre - test1 - UnitU);// * RotateX;
    }

    public void Tick()
    {
        double x = B / 180 * Math.PI;
        RotateX = new Matrix3
         (1, 0, 0,
         0, (float)Math.Cos(x), -(float)Math.Sin(x),
         0, (float)Math.Sin(x), (float)Math.Cos(x));
        RotateY = new Matrix3
            ((float)Math.Cos(x), 0, (float)Math.Sin(x),
            0, 1, 0,
            -(float)Math.Sin(x), 0, (float)Math.Cos(x));
        RotateZ = new Matrix3(
            (float)Math.Cos(x), -(float)Math.Sin(x), 0,
            (float)Math.Sin(x), (float)Math.Cos(x), 0,
            0, 0, 1);

        Console.WriteLine(Direction);
    }

    //public void RotateX()
    //{
    //    x = (float)(Math.Cos(B) * Direction.X - Math.Sin(B) * Direction.Z);
    //    Direction.X = x;
    //}


    public static Camera Instance()
    {
        return C;
    }
}