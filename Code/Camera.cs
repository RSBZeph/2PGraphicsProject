using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
class Camera
{
    public Vector3 Position = new Vector3(0, 0, 0), Direction = new Vector3(0, 0, 0), ScreenCentre;
    float FOV;
    public Plane LeftScreen;

    public Camera()
    {
        LeftScreen = new Plane();
        LeftScreen.DistanceToOrigin = 20;
        ScreenCentre = Position + Direction * LeftScreen.DistanceToOrigin;
        LeftScreen.P0 = ScreenCentre + new Vector3(-1, -1, 0);
        LeftScreen.P1 = ScreenCentre + new Vector3(1, -1, 0);
        LeftScreen.P2 = ScreenCentre + new Vector3(-1, 1, 0);
    }

    public void Tick()
    {

    }
}
