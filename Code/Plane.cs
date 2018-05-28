using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

class Plane : Primitive
{
    public Vector3 NPlane, P0, P1, P2, Distance;
    public float  width, height;

    public Plane(Vector3 normal, Vector3 distance, Vector3 Col)
    {
        NPlane = Vector3.Normalize(normal);
        Distance = distance;
        Color = Col;
        Position = new Vector3(0, 0, 0) + normal * distance;//?
    }
}