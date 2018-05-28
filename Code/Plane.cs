using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

class Plane : Primitive
{
    public Vector3 Normal;
    public float  width, height;

    public Plane(Vector3 P0, Vector3 normal, Vector3 Col)
    {
        Position = P0;
        Normal = Vector3.Normalize(normal);
        Color = Col;
    }
}