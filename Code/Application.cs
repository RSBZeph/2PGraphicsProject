using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using template.Code;
using Template;

class Application
{
    public Raytracer R;

    public Application(Surface sur)
    {
        R = new Raytracer(sur);
    }

    public void Tick()
    {
        R.Render();
    }
}