using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using Template;


class Game
{
    public Surface screen;
    Application A;

    public void Init()
    {
        A = new Application();
        A.R.Screen = screen;
    }

    public void Tick()
    {
        for (double i = 0.0; i < 360; i++)
        {
            double angle = i * Math.PI / 180;
            int x = (int)(screen.width / 2 + 50 * Math.Cos(angle));
            int y = (int)(screen.height / 2 + 80 + 50 * Math.Sin(angle));
            int Location = x + y * screen.width;
            screen.pixels[Location] = 255;
        }
       // A.Tick();

      
    }

    public void RenderGL()
    {
       A.R.DrawDebug();
    }
}