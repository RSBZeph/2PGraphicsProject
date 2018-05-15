using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using OpenTK.Graphics;
using OpenTK;

class Raytracer
{
    Camera C;
    Vector3 O, D;

    public Raytracer()
    {
        C = new Camera();
    }

    void Render()
    {

    }

    Vector3 CreateRayDirection(float x, float y)
    {
        Vector3 Direction, NDirection;
        Vector3 ScreenPoint = C.LeftScreen.P0 + x * (C.LeftScreen.P1 - C.LeftScreen.P0) + y * (C.LeftScreen.P2 - C.LeftScreen.P0);
        Direction = ScreenPoint - C.Position;
        NDirection = Vector3.Divide(Direction, (float)Math.Sqrt(Math.Pow(Direction.X, 2) + Math.Pow(Direction.Y, 2) + Math.Pow(Direction.Z, 2)));
        return Vector3.Divide(Direction, NDirection);
    }
}