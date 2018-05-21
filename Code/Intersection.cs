using OpenTK;
using System;
using System.Collections.Generic;
using static Raytracer;

class Intersection
{
    public Vector3 Position = new Vector3(), Color;
    public Primitive Object = new Primitive();
    public float Distance, ColorFactor = 1;
    public int X = -1, Y = -1;
    public Ray Ray;

    public Intersection(Primitive prim, float dis, Ray r)
    {
        Object = prim;
        Distance = dis;
        Ray = r;
        Position = Ray.Start + Ray.Direction * dis;
        Color = prim.Color;
    }
}