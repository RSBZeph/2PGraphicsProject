﻿using OpenTK;
using System;
using System.Collections.Generic;
using Template;
using static Raytracer;

class Scene
{
    List<Sphere> spheres = new List<Sphere>();
    List<Plane> planes = new List<Plane>();
    List<Light> lights = new List<Light>();
    Intersection reflectintersect;
    Camera C;
    public List<Intersection> intersections = new List<Intersection>();
    public List<Ray> shadowrays = new List<Ray>(), reflectrays = new List<Ray>();
    Intersection i1;
    public Surface Screen;
    float a, b, c, discriminant, result1, result2, finalresult, shadowlength, precalc1;
    Vector3 difference1, difference2, difference3, shadowray, MirrorColor,test = Vector3.Zero;
    bool FromMirror = false;
    int recursioncap = 5;
    public int recursions;

    public Scene(Surface sur)
    {
        Screen = sur;
        FillLists();
        C = Camera.Instance();
        test = C.Position;
    }

    void FillLists()
    {
        Sphere s1 = new Sphere(new Vector3(2, 5, 8), 2f, new Vector3(0.7f, 0.7f, 0.7f), false);
        s1.Mirror = true;
        s1.ReflectFactor = 0.5f;
        spheres.Add(s1);

        s1 = new Sphere(new Vector3(8, 5, 7), 1f, new Vector3(0, 0.6f, 0.9f), false);
        spheres.Add(s1);

        Sphere s2 = new Sphere(new Vector3(5, 5, 7), 0.5f, new Vector3(0, 0.8f, 0.3f), false);
        spheres.Add(s2);

        Plane p1 = new Plane(new Vector3(0, -1, 0), test, new Vector3(1f, 1f, 1f));
        planes.Add(p1);

        Light l1 = new Light(new Vector3(1, 5, 4), 5f);
        lights.Add(l1);

        //Light l2 = new Light(new Vector3(10, 5, 8), 5f);
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
                if (x > Screen.width / 2 && x < Screen.width && y > -1 && y < Screen.height)
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
                if (x > Screen.width / 2 && x < Screen.width && y > -1 && y < Screen.height)
                    Screen.pixels[Location] = Colour(new Vector3 (200, 200, 200));
            }
        }
    }

    public float CheckIntersect(Ray ray)
    {
        bool replaced = false , sphereFound = false;
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

                if (((!FromMirror && result1 > ray.MinDistance) || (FromMirror && result1 > 0.001f)) && (finalresult == -1 || result1 < finalresult))
                {
                    if (!FromMirror && result2 > ray.MinDistance || FromMirror && result2 > 0.001f)
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

                if (((!FromMirror && result2 > ray.MinDistance) || (FromMirror && result2 > 0.001f)) && (finalresult == -1 || result1 < finalresult))
                {
                    if (!FromMirror && result1 > ray.MinDistance || FromMirror && result1 > 0.001f)
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
        if(!sphereFound)
        {
            foreach (Plane plane in planes)
            {
                //float vx = ray.Direction.X, vy = ray.Direction.Y, vz = ray.Direction.Z;
                //float x0 = ray.Start.X, y0 = ray.Start.Y, z0 = ray.Start.Z;
                //float pa = plane.Position.X, pb = plane.Position.Y, pc = plane.Position.Z, d = plane.Distance;
                //float denominator = pa * vx + pb * vy + pc * vz;
                //if (denominator >= 0)
                //{
                //    float t = -(pa * x0 + pb * y0 + pc * z0 + d) / denominator;
                //    if (t < finalresult)
                //    {
                //        finalresult = t;
                //        replaced = true;
                //        Object = plane;
                //    }
                //}
                /*float t = -((Vector3.Dot(ray.Start, plane.NPlane) + plane.Distance) / (Vector3.Dot(ray.Direction, plane.NPlane)));
                if (t <= 0 && t < finalresult)
                {
                    finalresult = t;
                    replaced = true;
                    Object = plane;
                }*/
                float denominator = Vector3.Dot(plane.NPlane, ray.Direction);
                if (denominator > Math.Pow(10, -6))
                {
                    Vector3 p0l0 = plane.Distance - ray.Start;
                    finalresult = Vector3.Dot(p0l0, plane.NPlane) / denominator;
                    Object = plane;
                    //finalresult = ray.Start + ray.Direction * t;
                }
            }
        }
        if (replaced)
        {
            i1 = new Intersection(Object, finalresult, ray);
            if (!FromMirror)
                intersections.Add(i1);
            else
            {
                reflectintersect = i1;
                ray.Distance = finalresult;
                reflectrays.Add(ray);
                FromMirror = false;
            }
        }        
        return finalresult;
    }

    public Vector3 CheckReflectIntersect(Intersection inter)
    {
        if (recursions == 0)
        {
            MirrorColor = inter.Color;
        }

        reflectintersect = null;
        double angle = Math.Acos(Vector3.Dot(inter.Ray.Direction, inter.Normal)) * 180 / Math.PI;
        Vector3 Reflect = -2 * Vector3.Dot(inter.Ray.Direction, inter.Normal) * inter.Normal + inter.Ray.Direction;
        FromMirror = true;
        Ray RRay = new Ray(inter.Position + 0.0001f * inter.Ray.Direction, Reflect);
        RRay.x = inter.Ray.x;
        RRay.y = inter.Ray.y;
        RRay.Distance = CheckIntersect(RRay);
        if (RRay.Distance == -1)
        {
            RRay.Distance = 10;
            reflectrays.Add(RRay);
            recursions--;
            return new Vector3(0.2f, 0, 0) * inter.Object.ReflectFactor + ShadowRay(inter) * (1 - inter.Object.ReflectFactor);
        }
        else //if(reflectintersect != null)
        {
            if (reflectintersect != null && reflectintersect.Object.Mirror && recursions < recursioncap)
            {
                recursions++;
                return CheckReflectIntersect(reflectintersect);
            }
            else
            {
                return ShadowRay(reflectintersect) * inter.Object.ReflectFactor + ShadowRay(inter) * (1 - inter.Object.ReflectFactor);
            }
        }
    }

    public Vector3 ShadowRay(Intersection inter)
    {
        float attenuation = 0;
        foreach (Light light in lights)
        {
            difference2 = light.Position - inter.Position;
            shadowray = Vector3.Normalize(difference2);
            shadowlength = Math.Abs(Length(difference2));
            Ray SR = new Ray(inter.Position - shadowray * 0.0001f, shadowray)
            {
                x = inter.Ray.x,
                y = inter.Ray.y,
                MaxDistance = shadowlength,
            };
            SR = ShadowRayIntersect(SR);
            if (!SR.Occluded)
            {
                attenuation += Vector3.Dot(inter.Normal, difference2) / (shadowlength * shadowlength) * light.Intensity;
                if (attenuation > 1)
                    attenuation = 1;
            }
            shadowrays.Add(SR);
        }
        return inter.Color * attenuation;
    }

    Ray ShadowRayIntersect(Ray ray)
    {
        foreach (Sphere sphere in spheres)
        {
            difference3 = ray.Start - sphere.Position;
            a = Vector3.Dot(ray.Direction, ray.Direction);
            b = 2 * Vector3.Dot(difference3, ray.Direction);
            c = Vector3.Dot(difference3, difference3) - (sphere.Radius * sphere.Radius);
            discriminant = (b * b) - (4 * a * c);
            if (discriminant > 0)
            {
                precalc1 = (float)(Math.Sqrt(discriminant));
                result1 = ((-b + precalc1) / (2 * a));
                result2 = ((-b - precalc1) / (2 * a));
                if(result1 > 0 && result2 > 0)
                { 
                    ray.Distance = Math.Min(result1, result2);
                    ray.Occluded = true;
                    return ray;
                }                
            }
        }
        //foreach (Plane plane in planes)
        //{
        //    //    float vx = ray.Direction.X, vy = ray.Direction.Y, vz = ray.Direction.Z;
        //    //    float x0 = ray.Start.X, y0 = ray.Start.Y, z0 = ray.Start.Z;
        //    //    float pa = plane.Position.X, pb = plane.Position.Y, pc = plane.Position.Z, d = plane.Distance;
        //    //    float denominator = pa * vx + pb * vy + pc * vz;
        //    //    if (denominator >= 0)
        //    //    {
        //    //        float t = -(pa * x0 + pb * y0 + pc * z0 + d) / denominator;
        //    //        if (t < finalresult)
        //    //        {
        //    //            finalresult = t;
        //    //            replaced = true;
        //    //            Object = plane;
        //    //        }
        //    //    }
        //    float t = -((Vector3.Dot(SR.Start, plane.NPlane) + plane.Distance) / (Vector3.Dot(SR.Direction, plane.NPlane)));
        //    if (t <= 0 && t < finalresult)
        //    {
        //        SR.Distance = t;
        //        SR.Occluded = true;

        //    }
        //}
        return ray;
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