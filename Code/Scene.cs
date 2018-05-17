using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using Template;
using static Raytracer;

class Scene
{
    List<Sphere> spheres = new List<Sphere>();
    List<Plane> planes = new List<Plane>();
    List<Light> lights = new List<Light>();
    public List<Intersection> intersections = new List<Intersection>();
    Intersection i, j;
    public Surface Screen;

    public Scene(Surface sur)
    {
        Screen = sur;
        FillLists();
    }

    void FillLists()
    {
        Sphere s1 = new Sphere(new Vector3(3, 4, 7), 1f, 100, 70, 0);
        spheres.Add(s1);

        Sphere s2 = new Sphere(new Vector3(6, 6, 5), 2f, 0, 150, 200);
        spheres.Add(s2);
    }

    public void DrawPrimitivesDebug()
    {
        int width = Screen.width / 2, height = Screen.height;
        int width1 = width / 10, height1 = height / 10;
        foreach (Sphere sphere in spheres)
        {
            for (double i = 0.0; i < 360; i++)
            {
                double angle = i * Math.PI / 180;
                int x = (int)(width + width1 * sphere.Position.X + width1 * sphere.Radius * Math.Cos(angle));
                int y = (int)(height - height1 * sphere.Position.Z + height1 * sphere.Radius * Math.Sin(angle));
                int Location = x + y * Screen.width;
                Screen.pixels[Location] = sphere.Color;
            }
        }
    }

    public void CheckIntersect(Ray ray)
    {
        foreach (Sphere sphere in spheres)
        {
            Vector3 L = sphere.Position - ray.Start;
            float Lfloat = (float)Math.Sqrt(Math.Pow(L.X, 2) + Math.Pow(L.Y, 2) + Math.Pow(L.Z, 2)); //lengte van LenthToCentre (ABC formule)
            float tc = Vector3.Dot(L, ray.Direction); //ray vanuit sphere.position evenwijdig aan ray en stopt bij ray.start
            if (tc < 0)
                continue;
            float CentreToRay = (float)Math.Sqrt(Math.Pow(Lfloat, 2) + Math.Pow(tc, 2)); //loodlijn tussen ray en sphere.position (ABC formule)
            if (CentreToRay > sphere.Radius)
                continue;
            float t1c = (float)Math.Sqrt(Math.Pow(sphere.Radius, 2) - Math.Pow(CentreToRay, 2)); //afstand snijpunt loodlijn - ray en snijpunt met cirkel (ABC formule)
            float t1 = tc - t1c, t2 = tc + t1c; // lengte van ray.start naar eerste en tweede snijpunt met de sphere
            Vector3 Point1 = ray.Start + ray.Direction * t1, Point2 = ray.Start + ray.Direction * t2; //eerste en tweede intersection point met de sphere            
            Intersection i = new Intersection(Point1, sphere, t1, ray), j = new Intersection(Point2, sphere, t2, ray);
            intersections.Add(i);
            intersections.Add(j);
            break;
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