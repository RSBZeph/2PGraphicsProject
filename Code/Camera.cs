using OpenTK;
using OpenTK.Graphics.OpenGL;
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
        var M = Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);
        GL.LoadMatrix(ref M);
        GL.Translate(CameraPosition.X, CameraPosition.Y, CameraPosition.Z);
        GL.Rotate(360, CameraDirection.X, 0, 0);
        GL.Rotate(360, 0, CameraDirection.Y, 0);
        GL.Rotate(360, 0, 0, CameraDirection.Y);
    }
}
