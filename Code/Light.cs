using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

class Light
{
    public Vector3 Position;
    public float Intensity;

    public Light(Vector3 pos, float intensity)
    {
        Position = pos;
        Intensity = intensity;
    }
}