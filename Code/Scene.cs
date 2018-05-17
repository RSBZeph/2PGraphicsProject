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
        Sphere s = new Sphere(new Vector3(3, 4, 7), 3f);
        spheres.Add(s);
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
            Intersection i = new Intersection(), j = new Intersection();
            i.Object = sphere;
            i.Distance = t1;
            i.Position = Point1;
            intersections.Add(i);
            j.Object = sphere;
            j.Distance = t2;
            i.Position = Point2;
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

    float Distance(Vector3 first, Vector3 second)
    {
        Vector3 Difference = second - first;
        return (float)Math.Sqrt(Math.Pow(Difference.X, 2) + Math.Pow(Difference.Y, 2) + Math.Pow(Difference.Z, 2));
    }
}