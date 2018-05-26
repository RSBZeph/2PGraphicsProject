using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Template;

class Raytracer
{
    Camera C;
    public Surface Screen;
    Scene S;
    Ray r;
    Ray[] arRay;
    int CheckRayY;
    Vector3 RayColor;
    KeyboardState KBS;

    public Raytracer(Surface sur)
    {
        C = Camera.Instance();
        Screen = sur;
        S = new Scene(Screen);
        arRay = new Ray[Screen.width / 2];
        CheckRayY = Screen.height / 2;
    }

    public void Render()
    {
        KBS = Keyboard.GetState();
        if (KBS.IsKeyDown(Key.A))
            C.Position.X -= 0.25f;
        else if (KBS.IsKeyDown(Key.D))
            C.Position.X += 0.25f;
        if (KBS.IsKeyDown(Key.S))
            C.Position.Z -= 0.25f;
        else if (KBS.IsKeyDown(Key.W))
            C.Position.Z += 0.25f;
        if (KBS.IsKeyDown(Key.Q))
            C.Position.Y -= 0.25f;
        else if (KBS.IsKeyDown(Key.E))
            C.Position.Y += 0.25f;

        Draw3D();
        DrawDebug();
    }

    void Draw3D()
    {
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 0; y < Screen.height; y++)
            {
                r = new Ray(C.Position, CreateRayDirection(x, y));
                r.x = x;
                r.y = Screen.height - y;
                r.Distance = S.CheckIntersect(r);
                if (y == CheckRayY)
                    arRay[x] = r;
                Screen.pixels[x + y * Screen.width] = Colour(new Vector3(0.2f, 0, 0));
            }

        foreach (Intersection I in S.intersections)
        {
            //if (I.OnMirror)
            //{

            //}
            //else
            //{
                Screen.pixels[I.Ray.x + I.Ray.y * Screen.width] = S.ShadowRay(I);
            //}
        }
        S.intersections.Clear();
    }

    public void DrawDebug()
    {
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 0; y < Screen.height; y++)
            {
                Screen.pixels[x + Screen.width / 2 + y * Screen.width] = Colour(new Vector3(0, 0, 0));
            }
        
        RayColor = new Vector3(0.3f, 0.8f, 0.5f);
        Screen.Line(512, 0, 512, 512, Colour(new Vector3(1, 1, 1)));
        Vector2 Origin = VectorToScreenPos(C.Position);
        Screen.Line((int)(Origin.X - 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), Colour(new Vector3(1, 1, 1)));
        Screen.Line((int)(Origin.X + 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), Colour(new Vector3(1, 1, 1)));
        Screen.Line((int)(Origin.X - C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), (int)(Origin.X + C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.LeftScreen.DistanceToOrigin * Screen.height / 10), Colour(new Vector3(1, 1, 1)));
        Screen.Line(0, CheckRayY, Screen.width / 2, CheckRayY, Colour(RayColor));

        int counter = 0;
        float t;
        Vector2 end, srstart, srend;
        Vector3 shadowcolor = new Vector3(1, 1, 1);

        foreach (Ray r in arRay)
        {
            if (counter == 0)
            {
                t = 8;
                for (int o = 0; o < arRay.Length; o++)
                {
                    if (arRay[o].x == r.x)
                    {
                        t = arRay[o].Distance;
                        break;
                    }
                }
                end = VectorToScreenPos(C.Position + t * r.Direction);
                Screen.Line((int)(Origin.X), (int)(Origin.Y), (int)(end.X), (int)(end.Y), Colour(RayColor));
                counter = 30;

                foreach (Ray sr in S.shadowrays)
                {
                    if (r.y == sr.y)
                        if (r.x == sr.x)
                        {
                            srstart = VectorToScreenPos(sr.Start);
                            if (sr.Occluded)
                            {
                                srend = VectorToScreenPos(sr.Start + sr.Direction * sr.Distance);
                                shadowcolor = new Vector3(0.7f, 0.1f, 0);
                            }
                            else
                            {
                                srend = VectorToScreenPos(sr.Start + sr.Direction * sr.MaxDistance);
                                shadowcolor = new Vector3(1, 1, 1);
                            }
                            Screen.Line((int)(srstart.X), (int)(srstart.Y), (int)(srend.X), (int)(srend.Y), Colour(shadowcolor));
                            break;
                        }
                }
            }
            counter--;
        }
        S.DrawPrimitivesDebug();

        //foreach (Ray sr in S.shadowrays)
        //{
        //    if (sr.y == CheckRayY)
        //    {
        //        srstart = VectorToScreenPos(sr.Start);
        //        if (sr.Occluded)
        //        {
        //            srend = VectorToScreenPos(sr.Start + sr.Direction * sr.Distance);
        //            shadowcolor = new Vector3(0.7f, 0.1f, 0);
        //            Screen.Line((int)(srstart.X), (int)(srstart.Y), (int)(srend.X), (int)(srend.Y), Colour(shadowcolor));
        //        }
        //    }
        //}
        S.shadowrays.Clear();
    }

    Vector2 VectorToScreenPos(Vector3 v)
    {
        return new Vector2(v.X * Screen.width / 20 + Screen.width / 2, Screen.height - v.Z * Screen.height / 10);
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
        return ((int)(colorx * 255f) << 16) + ((int)(colory * 255f) << 8) + (int)(colorz * 255f);
    }

    public struct Ray
    {
        public Vector3 Start, Direction;
        public int x, y;
        public float Distance, MaxDistance;
        public bool Occluded;

        public Ray(Vector3 a, Vector3 b)
        {
            Start = a;
            Direction = b;
            x = -1;
            y = -1;
            Distance = 10;
            MaxDistance = 1;
            Occluded = false;
        }
    }
}