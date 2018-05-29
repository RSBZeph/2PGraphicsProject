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
    Vector3 RayColor = new Vector3(0.3f, 0.8f, 0.5f);
    KeyboardState KBS;
    public float angle = 0;
    public float K;
    int RotateAngleRight = 90;

    public Raytracer(Surface sur)
    {
        C = Camera.Instance();
        Screen = sur;
        S = new Scene(Screen);
        arRay = new Ray[Screen.width / 2];
        CheckRayY = Screen.height / 2;
        //K = (float)((10 / 180) * Math.PI);
    }

    public void Render()
    {
        KBS = Keyboard.GetState();       

        if (KBS.IsKeyDown(Key.U))
        {
            C.Direction = C.RotateX * C.Direction;
            //RotateAngleRight++;
            //if(RotateAngleRight >= 450)
            //{
            //    RotateAngleRight = 90;
            //}
            //C.RotateRight(RotateAngleRight);
           

            //if (C.B < 0)
            //    C.B = -C.B;

            //float x = (float)(Math.Cos(0.17) * C.Direction.X - Math.Sin(0.17) * C.Direction.Z);
            //C.Direction = C.Direction * Matrix3.CreateRotationY(0.1f); 
            //C.Direction = Vector3.Normalize(C.Direction + Vector3.UnitX);
            //C.Direction =  C.Direction * new Vector3((float)(C.Direction.X * Math.Cos(0.17) + C.Direction.Z * Math.Sin(0.17)), C.Direction.Y, (float)(-C.Direction.X * Math.Sin(0.17) + C.Direction.Z * Math.Cos(0.17)));
            //ScreenCentre = Position + Direction * DistanceToOrigin;
            //C.P0 = C.P0 * C.RotateX;
            //C.P1 = C.P1 * C.RotateX;
            //C.P2 = C.P2 * C.RotateX;
        }

        else if (KBS.IsKeyDown(Key.J))
        {
            C.Direction = C.Direction + new Vector3(0.1f, 0, 0);
            //C.Direction = C.Direction * Matrix3.CreateRotationZ(1f);
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

        C.Tick();
        Draw3D();
    }

    //draws the pixels on the left screen (the 3d world)
    void Draw3D()
    {
        //here it shoots a ray foreach pixel
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 1; y < Screen.height - 1; y++)
            {
                //making a ray of the pixel on x,y
                Ray r = new Ray(C.Position, CreateRayDirection(x, y));
                r.x = Screen.width / 2 - x;
                r.y = y;
                r.MinDistance = CreateMinDistance(x, y);
                r.Distance = S.CheckIntersect(r);
                //if r.Distance = -1 there is no intersection
                if (r.Distance == -1)
                {
                    r.Distance = 10;
                }
                if (y == CheckRayY)
                    arRay[x] = r;
            }
        //calls the debug function
        DrawDebug();
        //here we give every pixel the backgroundcolor
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 0; y < Screen.height; y++)
                Screen.pixels[x + y * Screen.width] = Colour(new Vector3(0.2f, 0, 0));

        //here we give every pixel with intersections a color
        foreach (Intersection I in S.intersections)
        {
            //if it is a mirror it looks for reflection rays
            if (I.Object.Mirror)
            {
                S.recursions = 0;
                Screen.pixels[I.Ray.x + I.Ray.y * Screen.width] = Colour(S.CheckReflectIntersect(I));
            }
            else
            {
                Screen.pixels[I.Ray.x + I.Ray.y * Screen.width] = Colour(S.ShadowRay(I));
            }
        }
        S.intersections.Clear();
        //draws the horizontal line in the middle of the left screen
        Screen.Line(0, CheckRayY, Screen.width / 2, CheckRayY, Colour(RayColor));
        //draw the vertical line between the debug and the 3d world
        Screen.Line(512, 0, 512, 512, Colour(new Vector3(1, 1, 1)));
    }

    //draws the pixels on the right screen (debug)
    public void DrawDebug()
    {
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 0; y < Screen.height; y++)
            {
                Screen.pixels[x + Screen.width / 2 + y * Screen.width] = Colour(new Vector3(0, 0, 0));
            }
        //draws the debug camera and sreen
        Vector2 Origin = VectorToScreenPos(C.Position);

        int counter = 0;
        float t;
        Vector2 end, srstart, srend, rrstart, rrend;
        Vector3 shadowcolor = new Vector3(1, 1, 1);

        //draws the rays in the debug
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

                //draws the shadowrays in the debug
                foreach (Ray sr in S.shadowrays)
                {
                    if (sr.y == CheckRayY)
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
                
                //draws the reflection rays in the debug
                foreach (Ray rr in S.reflectrays)
                {
                    if (rr.y == CheckRayY)
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
        //calling the function in Scene to draw the primitives in the debug
        S.DrawPrimitivesDebug();
        //clearing the ray lists
        S.reflectrays.Clear();
        
        //here the camera and the virtual screen are drawn, both dependant on the horizontal angle of the camera
        //the changing of the vertical angle is simulated by changing the distance from camera to screen for the debug window
        float angleoffset = (float)(Math.Atan((C.ScreenWidth / 2) / C.DistanceToOrigin2D) * 180 / Math.PI);
        float screencirclediameter = (float)Math.Sqrt(C.DistanceToOrigin2D * C.DistanceToOrigin2D + (C.ScreenWidth) * (C.ScreenWidth) / 4);
        Vector2 leftcorner = pointoncircle(angle - angleoffset - 90, screencirclediameter), rightcorner = pointoncircle(angle + angleoffset - 90, screencirclediameter);
        Screen.Line((int)leftcorner.X, (int)leftcorner.Y, (int)rightcorner.X, (int)rightcorner.Y, Colour(new Vector3(1, 1, 1)));
        Screen.Line((int)(pointoncircle(angle -90).X), (int)(pointoncircle(angle -90).Y), (int)(pointoncircle(angle - 220).X), (int)(pointoncircle(angle -220).Y), Colour(new Vector3(1, 1, 1)));
        Screen.Line((int)(pointoncircle(angle -90).X), (int)(pointoncircle(angle -90).Y), (int)(pointoncircle(angle + 40).X), (int)(pointoncircle(angle + 40).Y), Colour(new Vector3(1, 1, 1)));
        S.shadowrays.Clear();
       
    }


    //used to draw the camera on a invisible circle on the debug screen
    Vector2 pointoncircle(double angle, float size = 0.2f)
    {
        int width = Screen.width / 2, height = Screen.height;
        int width1 = width / 10, height1 = height / 10;
        angle = angle * Math.PI / 180;
        int x = (int)(width + width1 * C.Position.X + width1 * size * Math.Cos(angle));
        int y = (int)(height - height1 * C.Position.Z + height1 * size * Math.Sin(angle));
        return new Vector2(x, y);
    }

    //simple function to translate a 3d point to a point on the debug
    Vector2 VectorToScreenPos(Vector3 v)
    {
        return new Vector2(v.X * Screen.width / 20 + Screen.width / 2, Screen.height - v.Z * Screen.height / 10);
    }

    //creates the direction from camera to screen
    Vector3 CreateRayDirection(float x, float y)
    {
        Vector3 ScreenPoint = C.P0 + x * (C.P1 - C.P0) / (Screen.width / 2) + y * (C.P2 - C.P0) / Screen.height;
        return Vector3.Normalize(ScreenPoint - C.Position);
    }

    //gets the distance from camera to a point on the screen, any intersection of rays that's smaller than this value is on the wrong end of the screen, thus not visible
    float CreateMinDistance(float x, float y)
    {
        Vector3 ScreenPoint = C.P0 + x * (C.P1 - C.P0) / (Screen.width / 2) + y * (C.P2 - C.P0) / Screen.height;
        return S.Length(ScreenPoint - C.Position);
    }

    //translates vecor3 colors to int color for GL
    public static int Colour(Vector3 colorVec)
    {
        float colorx = MathHelper.Clamp(colorVec.X, 0, 1);
        float colory = MathHelper.Clamp(colorVec.Y, 0, 1);
        float colorz = MathHelper.Clamp(colorVec.Z, 0, 1);
        return ((int)(colorx * 255f) << 16) + ((int)(colory * 255f) << 8) + (int)(colorz * 255f);
    }

    //the ray struct which stores all the values for a ray
    public struct Ray
    {
        public Vector3 Start, Direction;
        public int x, y;
        public float Distance, MaxDistance, MinDistance;
        public bool Occluded;

        public Ray(Vector3 start, Vector3 direction)
        {
            Start = start;
            Direction = direction;
            x = -1;
            y = -1;
            Distance = 10;
            MaxDistance = 1;
            MinDistance = 1;
            Occluded = false;
        }
    }
}