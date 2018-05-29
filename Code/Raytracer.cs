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
    Ray[] arRay;
    int CheckRayY;
    Vector3 RayColor;
    KeyboardState KBS;
    public float K;

    public Raytracer(Surface sur)
    {
        C = Camera.Instance();
        Screen = sur;
        S = new Scene(Screen);
        arRay = new Ray[Screen.width / 2];
        CheckRayY = Screen.height / 2;
        K = (float)((10 / 180) * Math.PI);
    }

    public void Render()
    {
        KBS = Keyboard.GetState();
        if (KBS.IsKeyDown(Key.A))
        {
            C.Position -= 0.25f * C.Right;
            C.P0 -= 0.25f * C.Right;
            C.P1 -= 0.25f * C.Right;
            C.P2 -= 0.25f * C.Right;
        }
        else if (KBS.IsKeyDown(Key.D))
        {
            C.Position += 0.25f * C.Right;
            C.P0 += 0.25f * C.Right;
            C.P1 += 0.25f * C.Right;
            C.P2 += 0.25f * C.Right;
        }
        if (KBS.IsKeyDown(Key.S))
        {
            C.Position -= 0.25f * C.Direction;
            C.P0 -= 0.25f * C.Direction;
            C.P1 -= 0.25f * C.Direction;
            C.P2 -= 0.25f * C.Direction;
        }
        else if (KBS.IsKeyDown(Key.W))
        {
            C.Position += 0.25f * C.Direction;
            C.P0 += 0.25f * C.Direction;
            C.P1 += 0.25f * C.Direction;
            C.P2 += 0.25f * C.Direction;
        }
        if (KBS.IsKeyDown(Key.Q))
        {
            C.Position -= 0.25f * C.Up;
            C.P0 -= 0.25f * C.Up;
            C.P1 -= 0.25f * C.Up;
            C.P2 -= 0.25f * C.Up;
        }
        else if (KBS.IsKeyDown(Key.E))
        {
            C.Position += 0.25f * C.Up;
            C.P0 += 0.25f * C.Up;
            C.P1 += 0.25f * C.Up;
            C.P2 += 0.25f * C.Up;
        }

        if (KBS.IsKeyDown(Key.U))
        {
            if (C.B < 0)
                C.B = -C.B;

            //float x = (float)(Math.Cos(0.17) * C.Direction.X - Math.Sin(0.17) * C.Direction.Z);
            //C.Direction = C.Direction * Matrix4.CreateRotationY(0.1f); 
            //C.Direction = Vector3.Normalize(C.Direction + Vector3.UnitX);
            //C.Direction =  C.Direction * new Vector3((float)(C.Direction.X * Math.Cos(0.17) + C.Direction.Z * Math.Sin(0.17)), C.Direction.Y, (float)(-C.Direction.X * Math.Sin(0.17) + C.Direction.Z * Math.Cos(0.17)));
            //ScreenCentre = Position + Direction * DistanceToOrigin;
            //C.P0 = C.P0 * C.RotateX;
            //C.P1 = C.P1 * C.RotateX;
            //C.P2 = C.P2 * C.RotateX;
        }

        else if (KBS.IsKeyDown(Key.J))
        {
            if (C.B > 0)
                C.B = -C.B;
            C.Direction = C.Direction * Matrix3.CreateRotationZ(1f);
            //if (C.Direction != new Vector3(0, 0, 1))
            //{
            //   C.Direction = C.Direction * new Vector3((float)(C.Direction.X * Math.Cos(-0.17) - C.Direction.Y * Math.Sin(-0.17)), (float)(C.Direction.X * Math.Sin(-0.17) + C.Direction.Y * Math.Cos(-0.17)), C.Direction.Z);

            //}
            //if (C.Direction == new Vector3(0, 0, 1))
            //    C.Direction = new Vector3(0, -1f, 1);
            //C.Direction = C.RotateX * C.Direction;
            //ScreenCentre = Position + Direction * DistanceToOrigin;
            //C.P0 =  C.RotateX * C.P0;
            //C.P1 =  C.RotateX * C.P1;
            //C.P2 = C.RotateX * C.P2;
        }

        //if(KBS.IsKeyDown(Key.I))
        //{
        //    if(C.B < 0)
        //        C.B = -C.B;
        //    //C.Position = C.Position * C.RotateY;
        //    //ScreenCentre = Position + Direction * DistanceToOrigin;
        //    C.P0 = C.P0 * C.RotateY;
        //    C.P1 = C.P1 * C.RotateY;
        //    C.P2 = C.P2 * C.RotateY;
        //}

        //else if (KBS.IsKeyDown(Key.K))
        //{
        //    if (C.B > 0)
        //        C.B = -C.B;
        //    C.Direction = C.Direction * C.RotateY;
        //    //ScreenCentre = Position + Direction * DistanceToOrigin;
        //    C.P0 = C.P0 * C.RotateY;
        //    C.P1 = C.P1 * C.RotateY;
        //    C.P2 = C.P2 * C.RotateY;
        //}

        //if (KBS.IsKeyDown(Key.O))
        //{
        //    if (C.B < 0)
        //        C.B = -C.B;
        //    C.Direction = C.Direction * C.RotateZ;
        //    //ScreenCentre = Position + Direction * DistanceToOrigin;
        //    C.P0 = C.P0 * C.RotateZ;
        //    C.P1 = C.P1 * C.RotateZ;
        //    C.P2 = C.P2 * C.RotateZ;
        //}

        //else if (KBS.IsKeyDown(Key.L))
        //{
        //    if (C.B > 0)
        //        C.B = -C.B;
        //    C.Direction = C.Direction * C.RotateZ;
        //    //ScreenCentre = Position + Direction * DistanceToOrigin;
        //    C.P0 = C.P0 * C.RotateZ;
        //    C.P1 = C.P1 * C.RotateZ;
        //    C.P2 = C.P2 * C.RotateZ;
        //}

        C.Tick();
        Draw3D();
        //if (KBS.IsKeyDown(Key.U))
        //{
        //    C.Direction += Vector3.Normalize(C.Direction + Vector3.UnitX);            
        //    //C.B++;
        //}
        //else if (KBS.IsKeyDown(Key.J))
        //{
        //    //C.B = -0.1f;
        //    C.RotateX();
        //    //C.B--;
        //}

        //    if(KBS.IsKeyDown())


        //if (KBS.IsKeyDown(Key.I))
        //{
        //    C.Position = C.Position * C.RotateY;
        //    //ScreenCentre = Position + Direction * DistanceToOrigin;
        //    C.P0 = (C.ScreenCentre + new Vector3(-1, -1, 0)) * C.RotateY;
        //    C.P1 = (C.ScreenCentre + new Vector3(1, -1, 0)) * C.RotateY;
        //    C.P2 = (C.ScreenCentre + new Vector3(-1, 1, 0)) * C.RotateY;
        //}
        //else if (KBS.IsKeyDown(Key.L))
        //    C.RotateThatShit += new Vector3(0, 0.1f, 0);
        //if(KBS.IsKeyDown(Key.O))
        //    C.RotateThatShit -= new Vector3(0, 0, 0.1f);
        //else if (KBS.IsKeyDown(Key.K))
        //    C.RotateThatShit += new Vector3(0, 0, 0.1f);

        
        //C.RotateThatShit = new Vector3(1, 1, 1);
        
    }

    void Draw3D()
    {
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 1; y < Screen.height - 1; y++)
            {
                Ray r = new Ray(C.Position, CreateRayDirection(x, y));
                r.x = Screen.width / 2 - x;
                r.y = y;
                r.MinDistance = CreateMinDistance(x, y);
                r.Distance = S.CheckIntersect(r);
                if (r.Distance == -1)
                {
                    r.Distance = 10;
                }
                if (y == CheckRayY)
                    arRay[x] = r;
            }

        DrawDebug();

        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 0; y < Screen.height; y++)
                Screen.pixels[x + y * Screen.width] = Colour(new Vector3(0.2f, 0, 0));

        foreach (Intersection I in S.intersections)
        {
            if (I.Object.Mirror)
            {
                S.recursions = 0;
                Screen.pixels[I.Ray.x + I.Ray.y * Screen.width] = Colour(S.CheckReflectIntersect(I));
            }
            else
            {
                if (I.Object is Plane)
                {

                }
                Screen.pixels[I.Ray.x + I.Ray.y * Screen.width] = Colour(S.ShadowRay(I));
            }
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
        Vector2 Origin = VectorToScreenPos(C.Position);
        Screen.Line((int)(Origin.X - 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), Colour(new Vector3(1, 1, 1)));
        Screen.Line((int)(Origin.X + 5), (int)(Origin.Y + 5), (int)(Origin.X), (int)(Origin.Y - 10), Colour(new Vector3(1, 1, 1)));
        Screen.Line((int)(Origin.X - C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.DistanceToOrigin * Screen.height / 10), (int)(Origin.X + C.ScreenWidth / 2 * Screen.width / 20), (int)(Origin.Y - C.DistanceToOrigin * Screen.height / 10), Colour(new Vector3(1, 1, 1)));
        Screen.Line(0, CheckRayY, Screen.width / 2, CheckRayY, Colour(RayColor));

        int counter = 0;
        float t;
        Vector2 end, srstart, srend, rrstart, rrend;
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
                        }
                }

                foreach (Ray rr in S.reflectrays)
                {
                    if (r.y == rr.y)
                        if (r.x == rr.x)
                        {
                            rrstart = VectorToScreenPos(rr.Start);
                            rrend = VectorToScreenPos(rr.Start + rr.Direction * rr.Distance);
                            Screen.Line((int)(rrstart.X), (int)(rrstart.Y), (int)(rrend.X), (int)(rrend.Y), Colour(new Vector3(0, 0, 0.8f)));
                        }
                }
            }
            counter--;
        }
        S.DrawPrimitivesDebug();

        S.reflectrays.Clear();
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
        Screen.Line(512, 0, 512, 512, Colour(new Vector3(1, 1, 1)));
    }

    Vector2 VectorToScreenPos(Vector3 v)
    {
        return new Vector2(v.X * Screen.width / 20 + Screen.width / 2, Screen.height - v.Z * Screen.height / 10);
    }

    Vector3 CreateRayDirection(float x, float y)
    {
        Vector3 ScreenPoint = C.P0 + x * (C.P1 - C.P0) / (Screen.width / 2) + y * (C.P2 - C.P0) / Screen.height;
        return Vector3.Normalize(ScreenPoint - C.Position);
    }

    float CreateMinDistance(float x, float y)
    {
        Vector3 ScreenPoint = C.P0 + x * (C.P1 - C.P0) / (Screen.width / 2) + y * (C.P2 - C.P0) / Screen.height;
        return S.Length(ScreenPoint - C.Position);
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
        public float Distance, MaxDistance, MinDistance;
        public bool Occluded;

        public Ray(Vector3 a, Vector3 b)
        {
            Start = a;
            Direction = b;
            x = -1;
            y = -1;
            Distance = 10;
            MaxDistance = 1;
            MinDistance = 1;
            Occluded = false;
        }
    }
}