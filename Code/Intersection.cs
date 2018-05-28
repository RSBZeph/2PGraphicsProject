﻿using OpenTK;
using System;
using System.Collections.Generic;
using static Raytracer;

class Intersection
{
    public Vector3 Position, Color, Normal;
    public Primitive Object;
    public float Distance;
    public Ray Ray;
    public Intersection(Primitive prim, float dis, Ray r)
    {
        Object = prim;
        Distance = dis;
        Ray = r;
        Position = Ray.Start + Ray.Direction * dis;
        Color = prim.Color;
        if (Object is Sphere)
            Normal = Vector3.Normalize(Position - Object.Position);
        else
        {
            Plane plane = (Plane)Object; 
            Normal = plane.Normal;
        }        
    }
}