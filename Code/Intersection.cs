using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using static Raytracer;

class Intersection
{
    public Vector3 Position = new Vector3();
    public Primitive Object = new Primitive();
    public float Distance;
    public Ray Ray;

    public Intersection(Primitive prim, float dis, Ray r)
    {
        Object = prim;
        Distance = dis;
        Ray = r;
        Position = Ray.Start + Ray.Direction * dis;
    }
}