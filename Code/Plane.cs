using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using OpenTK;

class Plane : Primitive
{
    public Vector3 NPlane, P0, P1, P2;
    public float DistanceToOrigin, width, height;

    public Plane()
    {

    }
}