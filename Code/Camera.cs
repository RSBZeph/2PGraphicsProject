using OpenTK;
using OpenTK.Graphics.ES10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Camera
{
    Vector3 CameraPosition = new Vector3(0, 0, 0), CameraDirection = new Vector3(0, 0, 1);

    public Camera()
    {

    }

    void CameraPlacement()
    {
        //var M = Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);
        //GL.LoadMatrix(ref M);
        //GL.Translate(0, 0, -1);
        //GL.Rotate(110, 1, 0, 0);
        //GL.Rotate(a * 180 / PI, 0, 0, 1);
    }
}
