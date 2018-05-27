using OpenTK;
using System;
using System.Collections.Generic;
using Template;
using static Raytracer;

class Scene
{
    List<Sphere> spheres = new List<Sphere>();
    List<Plane> planes = new List<Plane>();
    List<Light> lights = new List<Light>();
    Camera C;
    public List<Intersection> intersections = new List<Intersection>();
    public List<Ray> shadowrays = new List<Ray>();
    Intersection i1;
    public Surface Screen;
    float a, b, c, discriminant, result1, result2, finalresult, shadowlength, precalc1;
    Vector3 difference1, difference2, difference3, shadowray;
    Ray SR;
    float dot = 100;

    public Scene(Surface sur)
    {
        Screen = sur;
        FillLists();
        C = Camera.Instance();
    }

    void FillLists()
    {
        Sphere s1 = new Sphere(new Vector3(4, 5, 5), 2f, new Vector3(0, 0.6f, 0.9f), false);
        spheres.Add(s1);

        Sphere s2 = new Sphere(new Vector3(6, 5, 4), 0.2f, new Vector3(0, 0.8f, 0.3f), false);
        spheres.Add(s2);

        Plane p1 = new Plane(new Vector3(0, -1, 0), 6f, new Vector3(1f, 1f, 1f));
        planes.Add(p1);

        Light l1 = new Light(new Vector3(0, 5, 2), 1f);
        lights.Add(l1);

        Light l2 = new Light(new Vector3(10, 5, 8), 5f);
        lights.Add(l2);
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
        bool replaced = false;
        Primitive Object = null;
        finalresult = -1;
        foreach (Sphere sphere in spheres)
        {
            difference1 = ray.Start - sphere.Position;
            a = Vector3.Dot(ray.Direction, ray.Direction);
            b = 2 * Vector3.Dot(difference1, ray.Direction);
            c = Vector3.Dot(difference1, difference1) - (sphere.Radius * sphere.Radius);
            discriminant = (b * b) - (4 * a * c);
            if (discriminant > 0)
            {
                result1 = (float)((-b + Math.Sqrt(discriminant)) / (2 * a));
                result2 = (float)((-b - Math.Sqrt(discriminant)) / (2 * a));

                if (result1 > ray.MinDistance && (finalresult == -1 || result1 < finalresult))
                {
                    if (result2 > ray.MinDistance)
                    {
                        finalresult = Math.Min(result1, result2);
                        replaced = true;
                        Object = sphere;
                    }
                    else
                    {
                        finalresult = result1;
                        replaced = true;
                        Object = sphere;
                    }
                }

                if (result2 > ray.MinDistance && (finalresult == -1 || result2 < finalresult))
                {
                    if (result1 > ray.MinDistance)
                    {
                        finalresult = Math.Min(result1, result2);
                        replaced = true;
                        Object = sphere;
                    }
                    else
                    {
                        finalresult = result2;
                        replaced = true;
                        Object = sphere;
                    }
                }
            }
        }
        //foreach (Plane plane in planes)
        //{
        //    float vx = ray.Direction.X, vy = ray.Direction.Y, vz = ray.Direction.Z;
        //    float x0 = ray.Start.X, y0 = ray.Start.Y, z0 = ray.Start.Z;
        //    float pa = plane.Position.X, pb = plane.Position.Y, pc = plane.Position.Z, d = plane.Distance;
        //    float denominator = pa * vx + pb * vy + pc * vz;
        //    if (denominator >= 0)
        //    {
        //        float t = -(pa * x0 + pb * y0 + pc * z0 + d) / denominator;
        //        if (t < finalresult)
        //        {
        //            finalresult = t;
        //            Object = plane;
        //        }
        //    }
        //    //float t = -((Vector3.Dot(ray.Start, plane.NPlane) + plane.DistanceToOrigin) / (Vector3.Dot(ray.Direction, plane.NPlane)));
        //    //if (t <= 0 && t < finalresult)
        //    //{
        //    //    finalresult = t;
        //    //    prim = plane;
        //    //}
        //}
        if (replaced)
        {
            i1 = new Intersection(Object, finalresult, ray, false);
            intersections.Add(i1);
        }
        if (finalresult == -1)
        {
            finalresult = 8;
        }
        return finalresult;
    }

    //public void ReflectionRayIntersect(Intersection Inter, Ray ray)//, Vector3 SphereNormal, Vector3 Incoming, Ray ray)
    //{
    //    foreach (Sphere S in spheres)
    //    {

    //        Vector3 ReflectionDirection = difference - 2 * (difference * Inter.Normal) * Inter.Normal;
    //        Vector3 Reflectdifference = i1.Position + 0.0001f * ReflectionDirection - S.Position;
    //        a = Vector3.Dot(ReflectionDirection, ReflectionDirection);
    //        b = 2 * Vector3.Dot(Reflectdifference, ReflectionDirection);
    //        c = Vector3.Dot(Reflectdifference, Reflectdifference) - (S.Radius * S.Radius);
    //        discriminant = (b * b) - (4 * a * c);
    //        if (discriminant > 0)
    //        {
    //            result1 = (float)((-b + Math.Sqrt(discriminant)) / (2 * a));
    //            result2 = (float)((-b - Math.Sqrt(discriminant)) / (2 * a));
    //            finalresult = Math.Min(result1, result2);
    //            i1 = new Intersection(S, finalresult, ray, S.Mirror);
    //            intersections.Add(i1);
    //            // return finalresult;
    //        }
    //    }
    //    // return 8;            
    //    return 0;
    //    if (sphere.Mirror == true)
    //    {
    //        Vector3 ReflectionRayDirection = ReflectionRay(i1, i1.Normal, difference);

    //        float ReflectionRayDiscriminant = (b * b) - (4 * a * c);

    //        if (ReflectionRayDiscriminant > 0)
    //        {
    //            result1 = (float)((-b + Math.Sqrt(discriminant)) / (2 * a));
    //            result2 = (float)((-b - Math.Sqrt(discriminant)) / (2 * a));
    //            finalresult = Math.Min(result1, result2);
    //            i1 = new Intersection(sphere, finalresult, ray, sphere.Mirror);
    //            intersections.Add(i1);
    //        }
    //    }
    //}

    public int ShadowRay(Intersection inter)
    {
        float attenuation = 0;
        bool nothing = false;

        if (inter.Ray.x == 60 && inter.Ray.y == 252)
        {

        }
        foreach (Light light in lights)
        {
            difference2 = light.Position - inter.Position;
            if (Vector3.Dot(inter.Position, C.Direction) <= 0)
            {
                nothing = true;
                continue;
            }
            shadowray = Vector3.Normalize(difference2);
            shadowlength = Math.Abs(Length(difference2));
            SR = new Ray(inter.Position - difference2 * 0.0001f, shadowray)
            {
                x = inter.Ray.x,
                y = inter.Ray.y,
                MaxDistance = shadowlength,
            };
            ShadowRayIntersect();
            if (!SR.Occluded)
            {
                attenuation += Vector3.Dot(inter.Normal, difference2) / (shadowlength * shadowlength) * light.Intensity;
                if (attenuation > 1)
                    attenuation = 1;
                else if (attenuation < 0)
                    attenuation = 0;
            }
            shadowrays.Add(SR);
            nothing = false;
        }
        if (!nothing)
            return Colour(inter.Color * attenuation);
        else
            return Colour(new Vector3(0.2f, 0, 0));
    }

    void ShadowRayIntersect()
    {
        foreach (Sphere sphere in spheres)
        {
            difference3 = SR.Start - sphere.Position;
            a = Vector3.Dot(SR.Direction, SR.Direction);
            b = 2 * Vector3.Dot(difference3, SR.Direction);
            c = Vector3.Dot(difference3, difference3) - (sphere.Radius * sphere.Radius);
            discriminant = (b * b) - (4 * a * c);
            if (discriminant > 0)
            {
                precalc1 = (float)(Math.Sqrt(discriminant));
                result1 = ((-b + precalc1) / (2 * a));
                result2 = ((-b - precalc1) / (2 * a));
                if(result1 > 0 && result2 > 0)
                { 
                    SR.Distance = Math.Min(result1, result2);
                    SR.Occluded = true;
                    return;
                }                
            }
        }
        return;
    }

    public float Distance(Vector3 first, Vector3 second)
    {
        Vector3 offset = second - first;
        return (float)Math.Sqrt((offset.X * offset.X) + (offset.Y * offset.Y) + (offset.Z * offset.Z));
    }
    public float Length(Vector3 L)
    {
        return (float)Math.Sqrt(L.X * L.X + L.Y * L.Y + L.Z * L.Z);
    }
}