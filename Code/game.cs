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
        screen.Clear(0);
        screen.Print("hello world", 2, 2, 0xffffff);
        screen.Line(2, 20, 160, 20, 0xff0000);
    }
}