using OpenTK;
using System;

class Camera
{
    public Vector3 Position = new Vector3(5, 5, 0.5f), Direction, ScreenCentre, P0, P1, P2, originaldirection = new Vector3(0, 0, 1);
    public Vector3 Right = new Vector3(1, 0, 0), Up = new Vector3(0, 1, 0);
    public float FOV = 180, ScreenWidth, DistanceToOrigin, DistanceToOrigin2D;
    static Camera C = new Camera();

    public Camera()
    {
        //change the originaldirection to start out looking in a different direction
        Direction = Vector3.Normalize(originaldirection);
        ScreenWidth = 2;
        Matrix4.CreateRotationX(4f);
    }

    public void Tick()
    {
        DistanceToOrigin = (float)(1 / Math.Sin(((FOV / 2) / 180) * Math.PI));
        ScreenCentre = Position + Direction * DistanceToOrigin;
        Console.WriteLine(ScreenCentre - Position);
        DistanceToOrigin2D = (float)Math.Sqrt((ScreenCentre - Position).X * (ScreenCentre - Position).X + (ScreenCentre - Position).Z * (ScreenCentre - Position).Z);
        P0 = ScreenCentre + Right + Up;
        P1 = ScreenCentre - Right + Up;
        P2 = ScreenCentre + Right - Up;
    }

    public static Camera Instance()
    {
        return C;
    }
}