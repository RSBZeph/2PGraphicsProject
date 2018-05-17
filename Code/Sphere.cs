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
    public int Color;

    public Sphere(int red = 255, int green = 255, int blue = 255)
    {
        Color = (red << 16) + (green << 8) + blue;
    }
}
