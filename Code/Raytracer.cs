using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Template;

class Raytracer
{
    Camera C;
    public Surface Screen;
    Scene S;
    Vector3 DebugOrigin;
    int scale = 50;

    public Raytracer()
    {
        C = new Camera();
        S = new Scene();
    }

    public void Render()
    {
        DrawDebug();
    }

    void Draw3D()
    {
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 0; y < Screen.height / 2; y++)
                for (int t = 0; t < 10; t++)
                {
                   S.CheckIntersect(new Ray(C.Position, CreateRayDirection(x, y)));
                }
    }

    public void DrawDebug()
    {
        Screen.Line(512, 0, 512, 512, 0xff0000);

        DebugOrigin = new Vector3(0.5f, -0.9f, 0f);

        GL.Color3(1f, 0f, 0f);
        GL.Begin(PrimitiveType.Triangles);
        GL.Vertex3(((-10f) / Screen.width) + DebugOrigin.X, (- 10f / Screen.height) + DebugOrigin.Y, 0);
        GL.Vertex3(((10f / Screen.width)) + DebugOrigin.X, (- 10f / Screen.height) + DebugOrigin.Y, 0);
        GL.Vertex3(DebugOrigin.X, (10f / Screen.height) + DebugOrigin.Y, 0);
        GL.End();

        Screen.Line((int)((DebugOrigin.X * 512 * 3) + C.LeftScreen.P0.X * scale), (int)((DebugOrigin.Y * -256 + 256) - C.LeftScreen.DistanceToOrigin * scale), (int)((DebugOrigin.X * 512 * 3) + C.LeftScreen.P1.X * scale), (int)((DebugOrigin.Y * -256 + 256) - C.LeftScreen.DistanceToOrigin * scale), 0xff0000);
    }

    Vector3 CreateRayDirection(float x, float y)
    {
        //= unit vector
        Vector3 Direction, NDirection;
        Vector3 ScreenPoint = C.LeftScreen.P0 + x * (C.LeftScreen.P1 - C.LeftScreen.P0) + y * (C.LeftScreen.P2 - C.LeftScreen.P0);
        Direction = ScreenPoint - C.Position;
        NDirection = Vector3.Divide(Direction, (float)Math.Sqrt(Math.Pow(Direction.X, 2) + Math.Pow(Direction.Y, 2) + Math.Pow(Direction.Z, 2)));
        return Vector3.Divide(Direction, NDirection);
    }

    public struct Ray
    {
        public Vector3 Start, Direction;

        public Ray(Vector3 a, Vector3 b)
        {
            Start = a;
            Direction = b;
        }
    }
}