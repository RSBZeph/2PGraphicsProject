using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

class Camera
{
    public Vector3 Position = new Vector3(5, 5, 1), Direction = new Vector3(0, 0, 1), ScreenCentre, NormDirection;
    Vector3 sw;
    public float FOV, ScreenWidth;
    public Plane LeftScreen;

    public Camera()
    {
        NormDirection = Vector3.Divide(Direction, (float)Math.Sqrt(Math.Pow(Direction.X, 2) + Math.Pow(Direction.Y, 2) + Math.Pow(Direction.Z, 2)));
        LeftScreen = new Plane();
        LeftScreen.DistanceToOrigin = 1;
        ScreenCentre = Position + Direction * LeftScreen.DistanceToOrigin;
        LeftScreen.P0 = ScreenCentre + new Vector3(-1, -1, 0);
        LeftScreen.P1 = ScreenCentre + new Vector3(1, -1, 0);
        LeftScreen.P2 = ScreenCentre + new Vector3(-1, 1, 0);
        sw = LeftScreen.P1 - LeftScreen.P0;
        ScreenWidth = (float)Math.Sqrt(Math.Pow(sw.X, 2) + Math.Pow(sw.Y, 2) + Math.Pow(sw.Z, 2));
    }
}