using System;
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

    public Raytracer(Surface sur)
    {
        C = new Camera();
        Screen = sur;
        S = new Scene(Screen);
    }

    public void Render()
    {
        Draw3D();
        DrawDebug();
    }

    void Draw3D()
    {
        for (int x = 0; x < Screen.width / 2; x++)        
            for (int y = 0; y < Screen.height / 2; y++)
                S.CheckIntersect(new Ray(C.Position, CreateRayDirection(x, y), x, y));

        foreach (Intersection I in S.intersections)
        {
            Screen.pixels[(int)(I.Ray.x + I.Ray.y * Screen.width)] = CreateColor(100, 255, 0);
        }
    }

    int CreateColor(int red, int green, int blue)
    {
        return (red << 16) + (green << 8) + blue;
    }

    public void DrawDebug()
    {
        Screen.Line(512, 0, 512, 512, 0xff0000);

        GL.Color3(1f, 0f, 0f);
        GL.Begin(PrimitiveType.Triangles);
        GL.Vertex3(((Screen.width / 2) - 10f) / Screen.width, (-(Screen.height / 2) - 10f) / Screen.height, 0);
        GL.Vertex3(((Screen.width / 2) + 10f) / Screen.width, (-(Screen.height / 2) - 10f) / Screen.height, 0);
        GL.Vertex3(((Screen.width / 2) - 0f) / Screen.width, (-(Screen.height / 2) + 10f) / Screen.height, 0);
        GL.End();

        DebugOrigin = new Vector3(0.5f, -0.5f, 0f);

        Screen.Line((int)((DebugOrigin.X * 512 * 3) + C.LeftScreen.P0.X * scale), (int)((DebugOrigin.Y * -256 + 256) - C.LeftScreen.DistanceToOrigin * scale), (int)((DebugOrigin.X * 512 * 3) + C.LeftScreen.P1.X * scale), (int)((DebugOrigin.Y * -256 + 256) - C.LeftScreen.DistanceToOrigin * scale), 0xff0000);

        S.DrawPrimitivesDebug();
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
        public float x, y;

        public Ray(Vector3 a, Vector3 b, float c, float d)
        {
            Start = a;
            Direction = b;
            x = c;
            y = d;
        }
    }
}