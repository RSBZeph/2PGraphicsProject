using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Template;

class Raytracer
{
    Camera C;
    public Surface Screen;
    Scene S;
    Ray r;
    Ray[] arRay;
    int CheckRayY = 0;

    public Raytracer(Surface sur)
    {
        C = Camera.Instance();
        Screen = sur;
        S = new Scene(Screen);
        arRay = new Ray[Screen.width / 2];
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
            {
                r = new Ray(C.Position, CreateRayDirection(x, y), x, y);
                if (y == CheckRayY)
                    arRay[x] = r;
                S.CheckIntersect(r);
            }

        foreach (Intersection I in S.intersections)
        {
            Screen.pixels[(int)(I.Ray.x + I.Ray.y * Screen.width)] = S.ShadowRay(I);
        }
    }

    public void DrawDebug()
    {
        Screen.Line(512, 0, 512, 512, 0xff0000);
        Vector3 Origin = new Vector3(Screen.width / 2 + C.Position.X * Screen.width / 20, Screen.height - C.Position.Z * Screen.height / 10, 0);
        Screen.Line((int)(Origin.X - 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), 0xff0000);
        Screen.Line((int)(Origin.X + 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), 0xff0000);
        Screen.Line((int)(Origin.X - C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), (int)(Origin.X + C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), 0xff0000);
        int tijd = 32;
        foreach (Ray r in arRay)
        {
            float t = 1000;
            if(tijd == 32)
            {
                Screen.Line((int)(Origin.X), (int)(Origin.Y), (int)(Origin.X + t * r.Direction.X), (int)(Origin.Y + t * r.Direction.Y), 0xff0000);
                tijd = 0;
            }
            tijd++;
        }
        S.DrawPrimitivesDebug();
    }

    Vector3 CreateRayDirection(float x, float y)
    {
        Vector3 Direction;
        Vector3 ScreenPoint = C.LeftScreen.P0 + x * (C.LeftScreen.P1 - C.LeftScreen.P0) / (Screen.width / 2) + y * (C.LeftScreen.P2 - C.LeftScreen.P0) / Screen.height;
        Direction = ScreenPoint - C.Position;
        return Vector3.Normalize(Direction);
    }
    public static int Colour(Vector3 colorVec)
    {
        float colorx = MathHelper.Clamp(colorVec.X, 0, 1);
        float colory = MathHelper.Clamp(colorVec.Y, 0, 1);
        float colorz = MathHelper.Clamp(colorVec.Z, 0, 1);
        return ((int)(colorx * 255f) << 16) + ((int)(colory * 255f) << 8) + (int)(colorz * 255);
    }

    public struct Ray
    {
        public Vector3 Start, Direction;
        public int x, y;

        public Ray(Vector3 a, Vector3 b, int c, int d)
        {
            Start = a;
            Direction = b;
            x = c;
            y = d;
        }
    }
}