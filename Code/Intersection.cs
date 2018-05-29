using OpenTK;
using System;
using System.Collections.Generic;
using static Raytracer;

class Intersection
{
    // here we store our value for our intersections (position, color, normal, ect)
    public Vector3 Position, Color, Normal;
    public Primitive Object;
    public double Distance;
    public Ray Ray;
    public Intersection(Primitive prim, float dis, Ray r)
    {
        //if we make a instance we fill in the values
        Object = prim;
        Distance = dis;
        Ray = r;
        Position = Ray.Start + Ray.Direction * dis;
        Color = prim.Color;
        //the normal for if it is a plane or a sphere
        if (Object is Sphere)
            Normal = Vector3.Normalize(Position - Object.Position);
        else
        {
            Plane plane = (Plane)Object; 
            Normal = plane.Normal;
        }        
    }
}