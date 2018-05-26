using OpenTK;
using System;

class Camera
{
    public Vector3 Position = new Vector3(5, 5, 0.5f), Direction = new Vector3(0, 0, 1), ScreenCentre, NormDirection, P0, P1, P2;
    public float FOV = 90, ScreenWidth, DistanceToOrigin;
    static Camera C = new Camera();

    public Camera()
    {
        double a = Math.Asin((FOV / 180) * Math.PI);
        ScreenWidth = 2;
        DistanceToOrigin = (float)(1 / Math.Sin(((FOV / 2) / 180) * Math.PI));
        NormDirection = Vector3.Normalize(Direction);
    }

    public void Tick()
    {
        ScreenCentre = Position + Direction * DistanceToOrigin;
        P0 = ScreenCentre + new Vector3(-1, -1, 0);
        P1 = ScreenCentre + new Vector3(1, -1, 0);
        P2 = ScreenCentre + new Vector3(-1, 1, 0);
    }

    public static Camera Instance()
    {
        return C;
    }
}