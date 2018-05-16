using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using static Raytracer;

class Scene
{
    List<Sphere> spheres = new List<Sphere>();
    List<Plane> planes = new List<Plane>();
    List<Light> lights = new List<Light>();
    List<Intersection> intersections = new List<Intersection>();
    
    public Scene()
    {

    }

    void FillLists()
    {
        Sphere s = new Sphere();
        s.Position = new Vector3(3, 4, 7);
        s.Radius = 3f;
        spheres.Add(s);
    }

    public void CheckIntersect(Ray ray)
    {
        foreach (Sphere sphere in spheres)
        {
            for (float t = 0; t < 100; t += 0.5f)
                if (Distance((ray.Start + ray.Direction * t), sphere.Position) <= sphere.Radius + 1)
                {
                    //if(sphere - intersect som die faking lang is > 0)
                    //maakt nieuwe intersect instantie en vul met data
                    //add aan intersections list
                }
        }

        foreach(Plane plane in planes)
        {
            for (float t = 0; t < 100; t += 0.5f)
                //if (ray raakt plane)
                //{
                //ditto van sphere
                //}
        }
    }

    float Distance(Vector3 first, Vector3 second)
    {
        Vector3 Difference = second - first;
        return (float)Math.Sqrt(Math.Pow(Difference.X, 2) + Math.Pow(Difference.Y, 2) + Math.Pow(Difference.Z, 2));
    }
}