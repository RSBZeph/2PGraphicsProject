using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using Template;


class Game
{
    public Surface screen;
    int width, height;
    Application A;

    public void Init()
    {
        A = new Application();
        A.R.Screen = screen;
    }

    public void Tick()
    {
        A.Tick();
    }

    public void RenderGL()
    {
        A.R.DrawDebug();
    }
}