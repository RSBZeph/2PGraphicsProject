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
    public List<Ray> shadowrays = new List<Ray>();
    Intersection i1;
    public Surface Screen;
    float a, b, c, discriminant, result1, result2, finalresult, shadowlength, precalc1;
    Vector3 difference, shadowray;
    Ray SR;

    public Scene(Surface sur)
    {
        Screen = sur;
        FillLists();
    }

    void FillLists()
    {
        Sphere s1 = new Sphere(new Vector3(5, 5, 7), 3f, new Vector3(0.5f, 0.3f, 0));
        spheres.Add(s1);

        //Sphere s2 = new Sphere(new Vector3(7, 5, 6), 1f, new Vector3(0, 0.6f, 0.5f));
        //spheres.Add(s2);

        Light l1 = new Light(new Vector3(0, 5, 2), 2f);
        lights.Add(l1);

        //Light l2 = new Light(new Vector3(10, 5, 5), 3f);
        //lights.Add(l2);
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

        foreach (Light light in lights)
        {
            for (double rad = 0.0; rad < 360; rad++)
            {
                double angle = rad * Math.PI / 180;
                int x = (int)(width + width1 * light.Position.X + width1 * 0.1f * Math.Cos(angle));
                int y = (int)(height - height1 * light.Position.Z + height1 * 0.1f * Math.Sin(angle));
                int Location = x + y * Screen.width;
                if (x > Screen.width / 2 && x < Screen.width && y > -1)
                    Screen.pixels[Location] = Colour(new Vector3 (200, 200, 200));
            }
        }
    }

    public float CheckIntersect(Ray ray)
    {
        foreach (Sphere sphere in spheres)
        {
            difference = ray.Start - sphere.Position;
            a = Vector3.Dot(ray.Direction, ray.Direction);
            b = 2 * Vector3.Dot(difference, ray.Direction);
            c = Vector3.Dot(difference, difference) - (sphere.Radius * sphere.Radius);
            discriminant = (b * b) - (4 * a * c);
            if (discriminant > 0)
            {
                result1 = (float)((-b + Math.Sqrt(discriminant)) / (2 * a));
                result2 = (float)((-b - Math.Sqrt(discriminant)) / (2 * a));
                finalresult = Math.Min(result1, result2);
                i1 = new Intersection(sphere, finalresult, ray);
                intersections.Add(i1);
                return finalresult;
            }
        }
        return 8;
    }

    public int ShadowRay(Intersection inter)
    {
        float attenuation = 0;
        foreach (Light light in lights)
        {
            difference = light.Position - inter.Position;
            shadowray = Vector3.Normalize(difference);
            shadowlength = Math.Abs(Length(difference));
            SR = new Ray(inter.Position, shadowray)
            {
                x = inter.Ray.x,
                y = inter.Ray.y,
                MaxDistance = shadowlength,
            };
            ShadowRayIntersect(inter, shadowlength - float.Epsilon);
            if (!SR.Occluded)
            {
                //attenuation += Vector3.Dot(inter.Normal, difference) / (shadowlength * shadowlength) * light.Intensity;
                //if (attenuation > 1)
                //    attenuation = 1;
                attenuation = 1;
            }
            shadowrays.Add(SR);
            if (Vector3.Dot(inter.Normal, difference) < 0)
            {

            }
        }
        return Colour(inter.Color * attenuation);
    }

    void ShadowRayIntersect(Intersection inter, float maxdis)
    {
        SR.Start += SR.Direction * float.Epsilon;
        foreach (Sphere sphere in spheres)
        {
            difference = SR.Start - sphere.Position;
            a = Vector3.Dot(SR.Direction, SR.Direction);
            b = 2 * Vector3.Dot(difference, SR.Direction);
            c = Vector3.Dot(difference, difference) - (sphere.Radius * sphere.Radius);
            discriminant = (b * b) - (4 * a * c);
            if (discriminant > 0)
            {
                precalc1 = (float)(Math.Sqrt(discriminant));
                result1 = ((-b + precalc1) / (2 * a));
                result2 = ((-b - precalc1) / (2 * a));
                if (result1 < maxdis && result2 < maxdis)
                {
                    if (result1 > 0 && result2 > 0)
                    {
                        finalresult = Math.Min(result1, result2) - 2 * float.Epsilon;
                        SR.Distance = finalresult;
                        SR.Occluded = true;
                        return;
                    }
                }
            }
        }
        foreach (Sphere s in spheres)
        {

        }
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