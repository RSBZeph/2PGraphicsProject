using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
class Camera
{
    Vector3 CameraPosition = new Vector3(0, 0, 0), CameraDirection = new Vector3(0, 0, 0);
    float LU, RU, LD, RD;
    KeyboardState KBS;

    public Camera()
    {
        KBS = new KeyboardState();
    }

    public void Tick()
    {
        CameraDirection += new Vector3(0, 0, 1);
        if (KBS.IsKeyDown(Key.W))
        {
            CameraDirection += new Vector3(0, 1, 0);
        }
        if (KBS.IsKeyDown(Key.S))
        {
            CameraDirection -= new Vector3(0, 1, 0);
        }
        if (KBS.IsKeyDown(Key.D))
        {
            CameraDirection += new Vector3(1, 0, 0);
        }
        if (KBS.IsKeyDown(Key.A))
        {
            CameraDirection -= new Vector3(1, 0, 0);
        }
        if (KBS.IsKeyDown(Key.Q))
        {
            CameraDirection += new Vector3(0, 0, 1);
        }
        if (KBS.IsKeyDown(Key.E))
        {
            CameraDirection -= new Vector3(0, 0, 1);
        }
    }

    public void CameraPlacement()
    {
        var M = Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);
        GL.LoadMatrix(ref M);
        GL.Translate(CameraPosition.X, CameraPosition.Y, CameraPosition.Z);
        GL.Rotate(1, CameraDirection.X, 0, 0);
        GL.Rotate(1, 0, CameraDirection.Y, 0);
        GL.Rotate(1, 0, 0, CameraDirection.Y);
    }
}
