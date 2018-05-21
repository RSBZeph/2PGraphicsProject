using OpenTK;
using System;

class Camera
{
    public Vector3 Position = new Vector3(5, 5, 0.5f), Direction = new Vector3(0, 0, 1), ScreenCentre, NormDirection;
    Vector3 sw;
    public float FOV, ScreenWidth;
    public Plane LeftScreen;
    static Camera C = new Camera();

    public Camera()
    {
        NormDirection = Vector3.Normalize(Direction);
        LeftScreen = new Plane();
        LeftScreen.DistanceToOrigin = 1.5f;
        ScreenCentre = Position + Direction * LeftScreen.DistanceToOrigin;
        LeftScreen.P0 = ScreenCentre + new Vector3(-1, -1, 0);
        LeftScreen.P1 = ScreenCentre + new Vector3(1, -1, 0);
        LeftScreen.P2 = ScreenCentre + new Vector3(-1, 1, 0);
        sw = LeftScreen.P1 - LeftScreen.P0;
        ScreenWidth = (float)Math.Sqrt(sw.X * sw.X + sw.Y * sw.Y + sw.Z * sw.Z);
    }

    public static Camera Instance()
    {
        return C;
    }
}