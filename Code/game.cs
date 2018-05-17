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
        A = new Application(screen);
    }

    public void Tick()
    {
        //A.Tick();
        //for (double i = 0.0; i < 360; i++)
        //{
        //    double angle = i * Math.PI / 180;
        //    int x = (int)(750 + 50 * Math.Cos(angle));
        //    int y = (int)(300 + 50 * Math.Sin(angle));
        //    int Location = x + y * screen.width;
        //    screen.pixels[Location] = CreateColor(0, 100, 255);
        //}
        //A.R.DrawDebug();
    }

    int CreateColor(int red, int green, int blue)
    {
        return (red << 16) + (green << 8) + blue;
    }

    public void RenderGL()
    {
        A.Tick();
        //A.R.DrawDebug();
    }
}