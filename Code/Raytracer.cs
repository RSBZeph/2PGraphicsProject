using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using OpenTK.Graphics;

class Raytracer
{
    Camera C;


    public Raytracer()
    {
        C = new Camera();
    }

    void Render()
    {

    }

struct Ray
{
    float3 O; // ray origin
    float3 D; // ray direction
    float t; // distance
};
}