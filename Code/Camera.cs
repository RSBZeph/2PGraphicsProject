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
        LeftScreen = new Plane(new Vector3(3, 2, 7.5f), 4f, new Vector3(0.2f, 0.6f, 0.4f));
        ScreenWidth = 2;
        LeftScreen.Distance = (float)(1 / Math.Sin(((FOV / 2) / 180) * Math.PI));
        NormDirection = Vector3.Normalize(Direction);
    }

    public void Tick()
    {
        ScreenCentre = Position + Direction * LeftScreen.Distance;
        LeftScreen.P0 = ScreenCentre + new Vector3(-1, -1, 0);
        LeftScreen.P1 = ScreenCentre + new Vector3(1, -1, 0);
        LeftScreen.P2 = ScreenCentre + new Vector3(-1, 1, 0);
    }

    public static Camera Instance()
    {
        return C;
    }
}