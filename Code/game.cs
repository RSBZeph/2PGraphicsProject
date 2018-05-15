using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using Template;

class Game
{
    public Surface screen;

    public void Init()
    {

    }

    public void Tick()
    {
        for (double i = 0.0; i < 360; i++)
        {
            double angle = i * Math.PI / 180;
            int x = (int)(80 + 50 * Math.Cos(angle));
            int y = (int)(80 + 50 * Math.Sin(angle));
            int Location = x + y * screen.width;
            screen.pixels[Location] = 255;
        }
    }
}