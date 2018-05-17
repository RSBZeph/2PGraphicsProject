using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using Template;

class Sphere : Primitive
{
    public Vector3 Position;
    public float Radius;

    public Sphere(Vector3 pos, float rad)
    {
        Position = pos;
        Radius = rad;
    }

    public void Draw()
    {




    }
}
