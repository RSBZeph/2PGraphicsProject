using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using Template;
using static Raytracer;

class Scene
{
    List<Sphere> spheres = new List<Sphere>();
    List<Plane> planes = new List<Plane>();
    List<Light> lights = new List<Light>();
    public List<Intersection> intersections = new List<Intersection>();
    Intersection i1, i2;
    public Surface Screen;
    float x1, y1, z1, i, j, k, a, b, c, discriminant, result1, result2, shadowlength, lightscale;
    Vector3 difference, shadowray;

    public Scene(Surface sur)
    {
        Screen = sur;
        FillLists();
    }

    void FillLists()
    {
        Sphere s1 = new Sphere(new Vector3(4, 5, 4), 1f, new Vector3(0.5f, 0.3f, 0));
        spheres.Add(s1);

        Sphere s2 = new Sphere(new Vector3(7, 5, 7), 2f, new Vector3(0, 0.6f, 0.5f));
        spheres.Add(s2);

        Light l1 = new Light(new Vector3(0, 10, 0), 1f);
        lights.Add(l1);
    }

    public void DrawPrimitivesDebug()
    {
        int width = Screen.width / 2, height = Screen.height;
        int width1 = width / 10, height1 = height / 10;
        foreach (Sphere sphere in spheres)
        {
            for (double rad = 0.0; rad < 360; rad++)
            {
                double angle = rad * Math.PI / 180;
                int x = (int)(width + width1 * sphere.Position.X + width1 * sphere.Radius * Math.Cos(angle));
                int y = (int)(height - height1 * sphere.Position.Z + height1 * sphere.Radius * Math.Sin(angle));
                int Location = x + y * Screen.width;
                Screen.pixels[Location] = Colour(sphere.Color);
            }
        }
    }

    public void CheckIntersect(Ray ray)
    {
        foreach (Sphere sphere in spheres)
        {
            x1 = ray.Start.X;
            y1 = ray.Start.Y;
            z1 = ray.Start.Z;
            i = ray.Direction.X;
            j = ray.Direction.Y;
            k = ray.Direction.Z;
            difference = ray.Start - sphere.Position;            
             a = Vector3.Dot(ray.Direction, ray.Direction);
             b = 2 * Vector3.Dot(difference, ray.Direction);
             c = Vector3.Dot(difference, difference) - (sphere.Radius * sphere.Radius);
             discriminant = (b * b) - (4 * a * c);
            if (discriminant > 0)
            {
                 result1 = (float)((-b + Math.Sqrt(discriminant)) / (2 * a));
                 result2 = (float)((-b - Math.Sqrt(discriminant)) / (2 * a));
                i1 = new Intersection(sphere, result1, ray);
                i2 = new Intersection(sphere, result2, ray);
                intersections.Add(i1);
                intersections.Add(i2);
                break;
            }
        }

        foreach(Plane plane in planes)
        {
            for (float t = 0; t < 100; t += 0.5f)
            {
             //if (ray raakt plane)
             //{
             //ditto van sphere
             //}
            }
        }
    }

    public int ShadowRay(Intersection intersection)
    {
        foreach (Light light in lights)
        {
            shadowray = light.Position - intersection.Position;
            shadowlength = Length(shadowray);
            intersection.Color = intersection.Color * (light.Intensity / shadowlength);
        }
        return Colour(intersection.Color);
    }

    public float Distance(Vector3 first, Vector3 second)
    {
        Vector3 Difference = second - first;
        return (float)Math.Sqrt(Math.Pow(Difference.X, 2) + Math.Pow(Difference.Y, 2) + Math.Pow(Difference.Z, 2));
    }
    public float Length(Vector3 L)
    {
        return (float)Math.Sqrt(L.X * L.X + L.Y * L.Y + L.Z * L.Z);
    }
}