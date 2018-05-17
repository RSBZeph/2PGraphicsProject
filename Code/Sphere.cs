using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;

class Sphere : Primitive
{
    public Vector3 Position;
    public float Radius;

    public Sphere(Vector3 pos, float rad)
    {
        Position = pos;
        Radius = rad;
    }
}
