using OpenTK;
using System;

class Camera
{
    public Vector3 Position = new Vector3(5, 5, 0.5f), Direction = new Vector3(0, 0, 1), ScreenCentre, NormDirection, P0, P1, P2;
    public Vector3 Right = new Vector3(1, 0, 0), Up = new Vector3(0, 1, 0);
    public float FOV = 180, ScreenWidth, DistanceToOrigin, DistanceToOrigin2D;
    static Camera C = new Camera();
    float x, y, z;
    public Matrix3 RotateX, RotateY, RotateZ;
    public Vector3 V = new Vector3(0, 0, 1), upp = new Vector3(0, 1, 0), UnitR, UnitU, test, test1;
    public double B = 1;
    float angle, hypotenuse, Oppositeside;
    public Camera()
    {
        double a = Math.Asin((FOV / 180) * Math.PI);

        ScreenWidth = 2;
        DistanceToOrigin = (float)(1 / Math.Sin(((FOV / 2) / 180) * Math.PI));
        NormDirection = Vector3.Normalize(Direction);
        Matrix4.CreateRotationX(4f);
    }

    public void Tick()
    {
        DistanceToOrigin2D = (float)Math.Sqrt((ScreenCentre - Position).X * (ScreenCentre - Position).X + (ScreenCentre - Position).Z * (ScreenCentre - Position).Z);
        ScreenCentre = Position + Direction * DistanceToOrigin;
        P0 = ScreenCentre + Right + Up;
        P1 = ScreenCentre - Right + Up;
        P2 = ScreenCentre + Right - Up;
    }

    public void RotateRight(float A)
    {
        double RotateAngleR = A / 180 * Math.PI;
        Oppositeside = ScreenCentre.Z - Position.Z;
        angle = (float)Math.Sin(1 / (Oppositeside));
        hypotenuse = (float)(Math.Sqrt(1 + (Oppositeside * Oppositeside)));

        P0.X = (Position.X + hypotenuse * (float)Math.Cos(RotateAngleR + angle));
        P0.Z = (Position.Z + hypotenuse * (float)Math.Sin(RotateAngleR + angle));
        P1.X = (Position.X + hypotenuse * (float)Math.Cos(RotateAngleR + angle));
        P1.Z = (Position.Z + hypotenuse * (float)Math.Sin(RotateAngleR + angle));
        P2.X = (Position.X + hypotenuse * (float)Math.Cos(RotateAngleR - angle));
        P2.Z = (Position.Z + hypotenuse * (float)Math.Sin(RotateAngleR - angle));
    }

    public static Camera Instance()
    {
        return C;
    }
}