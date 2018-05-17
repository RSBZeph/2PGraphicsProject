using OpenTK;
using template.Code;

class Sphere : Primitive
{
    public Vector3 Position;
    public float Radius;

    public Sphere(Vector3 pos, float radius, int red = 255, int green = 255, int blue = 255)
    {
        Position = pos;
        Radius = radius;
        Color = (red << 16) + (green << 8) + blue;
    }
}
