﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Template;

class Raytracer
{
    Camera C;
    public Surface Screen;
    Vector3 DebugOrigin;
    int scale = 50;

    public Raytracer()
    {
        C = new Camera();
    }

    public void Render()
    {
        DrawDebug();
    }

    void Draw3D()
    {
        for (int x = 0; x < Screen.width / 2; x++)
            for (int y = 0; y < Screen.height / 2; y++)
                for (int t = 0; t < 10; t++)
                {
                   //CheckIntersect(C.Position + t * CreateRayDirection(x, y));
                }
    }

    public void DrawDebug()
    {
        Screen.Line(512, 0, 512, 512, 0xff0000);

        GL.Color3(1f, 0f, 0f);
        GL.Begin(PrimitiveType.Triangles);
        GL.Vertex3(((Screen.width / 2) - 10f ) / Screen.width, (-(Screen.height / 2) - 10f) / Screen.height, 0);
        GL.Vertex3(((Screen.width / 2) + 10f) / Screen.width, (-(Screen.height / 2) - 10f) / Screen.height, 0);
        GL.Vertex3(((Screen.width / 2) - 0f) / Screen.width, (-(Screen.height / 2) + 10f) / Screen.height, 0);
        GL.End();

        DebugOrigin = new Vector3(0.5f, -0.5f, 0f);

        Screen.Line((int)((DebugOrigin.X * 512 * 3) + C.LeftScreen.P0.X * scale), (int)((DebugOrigin.Y * -256 + 256) - C.LeftScreen.DistanceToOrigin * scale), (int)((DebugOrigin.X * 512 * 3) + C.LeftScreen.P1.X * scale), (int)((DebugOrigin.Y * -256 + 256) - C.LeftScreen.DistanceToOrigin * scale), 0xff0000);
    }

    float Distance(Vector3 first, Vector3 second)
    {
        Vector3 Difference = second - first;
        return (float)Math.Sqrt(Math.Pow(Difference.X, 2) + Math.Pow(Difference.Y, 2) + Math.Pow(Difference.Z, 2));
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