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
    Intersection reflectintersect;
    Camera C;
    public List<Intersection> intersections = new List<Intersection>();
    public List<Ray> shadowrays = new List<Ray>(), reflectrays = new List<Ray>();
    Intersection i1;
    public Surface Screen;
    float b, c, discriminant, result1, result2, finalresult, shadowlength, precalc1;
    Vector3 shadowray;
    bool FromMirror = false;
    int recursioncap = 5;
    public int recursions;

    public Scene(Surface sur)
    {
        Screen = sur;
        FillLists();
        C = Camera.Instance();
    }

    //filling the the scene with instances of objects (spheres, planes and lights have their own lists)
    void FillLists()
    {
        Sphere s1 = new Sphere(new Vector3(5, 5, 9), 2f, new Vector3(0, 0.8f, 0.2f), false);
        s1.Mirror = true;
        s1.ReflectFactor = 0.5f;
        spheres.Add(s1);

        s1 = new Sphere(new Vector3(9.5f, 5, 6), 1.5f, new Vector3(0, 0.6f, 0.9f), false);
        spheres.Add(s1);

        s1 = new Sphere(new Vector3(2, 5, 6), 0.8f, new Vector3(0.8f, 0.5f, 0.3f), false);
        spheres.Add(s1);

        Plane p1 = new Plane(new Vector3(5, 3, 11), new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0.7f, 0.6f, 0));
        p1.finite = false;
        p1.checkerboard = true;
        planes.Add(p1);
        
        p1 = new Plane(new Vector3(5, 9, 15), new Vector3(1, 0.3f, 0), new Vector3(0.3f, 1, 0), new Vector3(0.7f, 0.6f, 0));
        p1.height = 3;
        p1.width = 6;
        planes.Add(p1);

        Light l1 = new Light(new Vector3(0, 7, 3), 3f);
        lights.Add(l1);

        l1 = new Light(new Vector3(10, 7, 3), 3f);
        lights.Add(l1);
    }

    //here we draw our primitives (sphere and light) for the debug
    public void DrawPrimitivesDebug()
    {
        int width = Screen.width / 2, height = Screen.height;
        int width1 = width / 10, height1 = height / 10;
        //drawing the spheres
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
        //drawing the lights
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

    //here we check for intersections with rays and our primitives(sphere and planes)
    //it returns the distance from the start of the ray to the intersection
    //and adds the intersection to the intersection list (with the object, the ray and the distance)
    public float CheckIntersect(Ray ray)
    {
        bool replaced = false;
        //the object and distance that we return
        Primitive Object = null;
        finalresult = -1;
        //re-write the the formula for sphere intersection with lines to one which consists of an a * t^2, b * t and c, allowing us to use the ABC formula to solve t
        foreach (Sphere sphere in spheres)
        {
            Vector3 difference1 = ray.Start - sphere.Position;
            b = 2 * Vector3.Dot(difference1, ray.Direction);
            c = Vector3.Dot(difference1, difference1) - (sphere.Radius * sphere.Radius);
            discriminant = (b * b) - (4 * c);
            if (discriminant >= 0)
            {
                result1 = (float)((-b + Math.Sqrt(discriminant)) / 2);
                result2 = (float)((-b - Math.Sqrt(discriminant)) / 2);

                if (((!FromMirror && result1 > ray.MinDistance) || (FromMirror && result1 > 0.001f)) && (!replaced || result1 < finalresult))
                {
                    if (!FromMirror && result2 > ray.MinDistance || FromMirror && result2 > 0.001f)
                        finalresult = Math.Min(result1, result2);
                    else
                        finalresult = result1;
                    replaced = true;
                    Object = sphere;                    
                }

                if (((!FromMirror && result2 > ray.MinDistance) || (FromMirror && result2 > 0.001f)) && (!replaced || result1 < finalresult))
                {
                    if (!FromMirror && result1 > ray.MinDistance || FromMirror && result1 > 0.001f)                    
                        finalresult = Math.Min(result1, result2);                    
                    else                    
                        finalresult = result2;
                    replaced = true;
                    Object = sphere;                   
                }
            }
        }
        //calculate intersections between rays and planes
        //if the plane is finite, check if the intersection position is within the boundries of the plane
        //else, acknowledge the intersection if it is not too ridiculously far away (<100)
        foreach (Plane plane in planes)
        {
            if (Vector3.Dot(plane.Normal, ray.Direction) == 0)
                continue;
            result1 = -Vector3.Dot((ray.Start - plane.Position), plane.Normal) / Vector3.Dot(ray.Direction, plane.Normal);
            if (result1 <= 0)
                continue;

            if (plane.finite)
            {
                Vector3 intersectpos = ray.Start + ray.Direction * result1;
                Vector3 middletointer = intersectpos - plane.Position;
                float dota = 0, dotb = 0;

                if (result1 < finalresult && result1 > 0 || !replaced)
                {
                    dota = Vector3.Dot(middletointer, plane.Dimension1);
                    if (dota < 0)
                        dota = Vector3.Dot(middletointer, -plane.Dimension1);
                    if (dota < plane.width)
                    {
                        dotb = Vector3.Dot(middletointer, plane.Dimension2);
                        if (dotb < 0)
                            dotb = Vector3.Dot(middletointer, -plane.Dimension2);
                        if (dotb < plane.height)
                        {
                            finalresult = result1;
                            Object = plane;
                            replaced = true;
                        }
                        else
                            continue;
                    }
                    else
                        continue;
                }
            }
            else
            {
                if (result1 < 100 && (result1 < finalresult || !replaced))
                {
                    finalresult = result1;
                    Object = plane;
                    replaced = true;
                }
            }
        }
        //when there is a intersection we add it to the list 
        //we also check if it is from a mirror; in that case we add it to the reflection intersection list
        //if no intersections were made, a distance of -1 will be returned, which then gets converted in a stardard ray length of 10 for the debug screen
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

    //use the checkintersect to find a intersection of the refleced ray
    public Vector3 CheckReflectIntersect(Intersection inter)
    {
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
        else 
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

    //creates shadowrays for each light, calculates attentuation
    public Vector3 ShadowRay(Intersection inter)
    {
        float attenuation = 0.1f;
        foreach (Light light in lights)
        {
            Vector3 difference2 = light.Position - inter.Position;
            shadowray = Vector3.Normalize(difference2);
            shadowlength = Length(difference2);
            Ray SR = new Ray(inter.Position - shadowray * 0.0001f, shadowray)
            {
                x = inter.Ray.x,
                y = inter.Ray.y,
                MaxDistance = shadowlength,
                Distance = shadowlength,
            };
            SR = ShadowRayIntersect(SR);
            if (!SR.Occluded)
            {
                attenuation += Vector3.Dot(inter.Normal, difference2) / (shadowlength * shadowlength) * light.Intensity;
                attenuation = MathHelper.Clamp(attenuation, 0, 1);
            }
            shadowrays.Add(SR);
        }
        //determines the color of the plane, if it is assigned to have a checkerboard
        if (inter.Object is Plane)
        {
            Plane plane = (Plane)inter.Object;
            if (plane.checkerboard)
            {
                Vector3 planedistance = inter.Position - inter.Object.Position;
                if (Math.Sin(Vector3.Dot(planedistance, plane.Dimension1)) <= 0 && Math.Sin(Vector3.Dot(planedistance, plane.Dimension2)) <= 0)
                    inter.Color = new Vector3(1, 1, 1);
                else if (Math.Sin(Vector3.Dot(planedistance, plane.Dimension1)) <= 0 || Math.Sin(Vector3.Dot(planedistance, plane.Dimension2)) <= 0)
                    inter.Color = new Vector3(0, 0, 0);
                else
                    inter.Color = new Vector3(1, 1, 1);
            }
        }
        return inter.Color * attenuation;
    }

    //this finds our shadow ray intersections
    //it uses the same way to find intersections like in CheckIntersect
    Ray ShadowRayIntersect(Ray ray)
    {
        if (ray.x == 332)
            if (ray.y == 256)
            {
                //ray.Occluded = false;
                //return ray;
            }
        foreach (Sphere sphere in spheres)
        {
            Vector3 difference3 = ray.Start - sphere.Position;
            b = 2 * Vector3.Dot(difference3, ray.Direction);
            c = Vector3.Dot(difference3, difference3) - (sphere.Radius * sphere.Radius);
            discriminant = (b * b) - (4 * c);
            if (discriminant >= 0)
            {
                precalc1 = (float)(Math.Sqrt(discriminant));
                result1 = ((-b + precalc1) / 2);
                result2 = ((-b - precalc1) / 2);
                if (result1 > 0 && result2 > 0)
                {
                    if (Math.Min(result1, result2) < ray.Distance)
                    {
                        ray.Distance = Math.Min(result1, result2);
                        ray.Occluded = true;
                    }
                }
            }
        }
        foreach (Plane plane in planes)
        {
            if (Vector3.Dot(plane.Normal, ray.Direction) == 0)
                continue;
            result1 = -Vector3.Dot((ray.Start - plane.Position), plane.Normal) / Vector3.Dot(ray.Direction, plane.Normal);
            if (result1 <= 0)
                continue;

            if (result1 < ray.Distance && result1 > 0.001f)
            {
                ray.Distance = result1;
                ray.Occluded = true;
            }
        }
        return ray;
    }

    //simple functions to check distance between two point and lenght of a vector
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