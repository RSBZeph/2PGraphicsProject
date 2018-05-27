using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

class Plane : Primitive
{
    public Vector3 NPlane, P0, P1, P2;
    public float Distance, width, height;

    public Plane(Vector3 normal, float distance, Vector3 Col)
    {
        Position = normal;//?
        NPlane = Vector3.Normalize(normal);
        Distance = distance;
        Color = Col;
    }
}