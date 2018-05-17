using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;

class Application
{
    public Raytracer R;

    public Application()
    {
        R = new Raytracer();
    }

    public void Tick()
    {
        R.Render();
    }
}