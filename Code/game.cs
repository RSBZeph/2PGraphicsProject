using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using Template;

class Game
{
    public Surface screen;
    static Camera C;

    public void Init()
    {
        C = new Camera();
    }

    public void Tick()
    {
        screen.Clear(0);
        screen.Print("hello world", 2, 2, 0xffffff);
        screen.Line(2, 20, 160, 20, 0xff0000);
        C.Tick();
    }

    public void RenderGL()
    {
        GL.Color3(1.0f, 0.0f, 0.0f);
        GL.Begin(PrimitiveType.Triangles);
        GL.Vertex3(-0.5f, -0.5f, 0);
        GL.Vertex3(0.5f, -0.5f, 0);
        GL.Vertex3(-0.5f, 0.5f, 0);
        GL.End();
    }
}