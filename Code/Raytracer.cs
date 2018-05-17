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
    bool A = true;

    public Raytracer(Surface sur)
    {
        C = new Camera();
        Screen = sur;
        S = new Scene(Screen);
       

    }

    public void Render()
    {
        Draw3D();
        if (A)
        {
            DrawDebug();
            A = false;
        }
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

        Screen.Line((int)(Screen.width / 2 + C.Position.X * Screen.width / 20 - 10), (int)(Screen.height - C.Position.Z * Screen.height / 10 + 5), (int)(Screen.width / 2 + C.Position.X * Screen.width / 20), (int)(Screen.height - C.Position.Z * Screen.height / 10 - 20), 0xff0000);
        Screen.Line((int)(Screen.width / 2 + C.Position.X * Screen.width / 20 + 10), (int)(Screen.height - C.Position.Z * Screen.height / 10 + 5), (int)(Screen.width / 2 + C.Position.X * Screen.width / 20), (int)(Screen.height - C.Position.Z * Screen.height / 10 - 20), 0xff0000);
        Screen.pixels[(int)(Screen.width / 2 + C.Position.X * Screen.width / 20 + Screen.height - C.Position.Z * Screen.height / 10 * Screen.width)] = 0xff0000;
        Vector3 Origin = new Vector3(Screen.width / 4 * 3, Screen.height / 10 * 9, 0);

        Screen.Line((int)(Origin.X - C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), (int)(Origin.X + C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), 0xff0000);

        foreach (Intersection I in S.intersections)
        {
            Screen.Line((int)(Screen.width * 0.75), (int)(Screen.height * 0.9), (int)I.Position.X, (int)I.Position.Y, CreateColor(0, 0, 255));
            //Console.WriteLine("aaaffaafaffadadffa");
        }

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