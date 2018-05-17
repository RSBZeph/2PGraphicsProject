using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Template;

class Raytracer
{
    Camera C;
    public Surface Screen;
    Scene S;

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
            for (int y = 0; y < Screen.height; y++)
                S.CheckIntersect(new Ray(C.Position, CreateRayDirection(x, y), x, y));

        foreach (Intersection I in S.intersections)
        {
            Screen.pixels[(int)(I.Ray.x + I.Ray.y * Screen.width)] = I.Object.Color;
        }
    }

    int CreateColor(int red, int green, int blue)
    {
        return (red << 16) + (green << 8) + blue;
    }

    public void DrawDebug()
    {
        Screen.Line(512, 0, 512, 512, 0xff0000);
        Vector3 Origin = new Vector3(Screen.width / 2 + C.Position.X * Screen.width / 20, Screen.height - C.Position.Z * Screen.height / 10, 0);
        Screen.Line((int)(Origin.X - 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), 0xff0000);
        Screen.Line((int)(Origin.X + 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), 0xff0000);

        Screen.Line((int)(Origin.X - C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), (int)(Origin.X + C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), 0xff0000);

        S.DrawPrimitivesDebug();
    }

    Vector3 CreateRayDirection(float x, float y)
    {
        //= unit vector
        Vector3 Direction;
        Vector3 ScreenPoint = C.LeftScreen.P0 + x * (C.LeftScreen.P1 - C.LeftScreen.P0) + y * (C.LeftScreen.P2 - C.LeftScreen.P0);
        Direction = ScreenPoint - C.Position;
        return Vector3.Normalize(Direction);
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