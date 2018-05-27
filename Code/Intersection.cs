using OpenTK;
using System;
using System.Collections.Generic;
using static Raytracer;

class Intersection
{
    public Vector3 Position, Color, Normal;
    public Primitive Object;
    public float Distance;
    public Ray Ray;
    public bool OnMirror;
    public Intersection(Primitive prim, float dis, Ray r, bool onmirror)
    {
        Object = prim;
        Distance = dis;
        Ray = r;
        Position = Ray.Start + Ray.Direction * dis;
        Color = prim.Color;
        if (Object is Sphere)
            Normal = Vector3.Normalize(Position - Object.Position);

        OnMirror = onmirror;
    }
}