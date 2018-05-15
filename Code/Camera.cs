using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
class Camera
{
    Vector3 CameraPosition = new Vector3(0, 0, 0), CameraDirection = new Vector3(0, 0, 0), ScreenCentre;
    float FOV;
    Plane LeftScreen;

    public Camera()
    {
        LeftScreen = new Plane();
        LeftScreen.DistanceToOrigin = 20;
        ScreenCentre = CameraPosition + CameraDirection * LeftScreen.DistanceToOrigin;
        LeftScreen.P0 = ScreenCentre + new Vector3(-1, -1, 0);
        LeftScreen.P1 = ScreenCentre + new Vector3(1, -1, 0);
        LeftScreen.P2 = ScreenCentre + new Vector3(-1, 1, 0);
    }

    public void Tick()
    {

    }
}
