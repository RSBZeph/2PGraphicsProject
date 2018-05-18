using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using Template;
using OpenTK;


class Game
{
    public Surface screen;
    Application A;

    public void Init()
    {
        A = new Application(screen);
    }

    public void Tick()
    {

    }

    public void RenderGL()
    {
        A.Tick();
    }
}