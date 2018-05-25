using OpenTK;
using System;

class Camera
{
    public Vector3 Position = new Vector3(5, 5, 0.5f), Direction = new Vector3(0, 0, 1), ScreenCentre, NormDirection;
    public float FOV = 90, ScreenWidth;
    public Plane LeftScreen;
    static Camera C = new Camera();

    public Camera()
    {
        double a = Math.Asin((FOV / 180) * Math.PI);
        LeftScreen = new Plane();
        ScreenWidth = 2;
        LeftScreen.DistanceToOrigin = (float)(1 / Math.Sin(((FOV / 2) / 180) * Math.PI));
        NormDirection = Vector3.Normalize(Direction);
        ScreenCentre = Position + Direction * LeftScreen.DistanceToOrigin;
        LeftScreen.P0 = ScreenCentre + new Vector3(-1, -1, 0);
        LeftScreen.P1 = ScreenCentre + new Vector3(1, -1, 0);
        LeftScreen.P2 = ScreenCentre + new Vector3(-1, 1, 0);
    }

    public static Camera Instance()
    {
        return C;
    }
}