using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;

class Scene
{
    List<Sphere> spheres = new List<Sphere>();
    List<Plane> planes = new List<Plane>();
    List<Light> light = new List<Light>();
    Raytracer r = new Raytracer();

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

    void Intersect(Vector3 start, Vector3 direction)
    {

    }
}