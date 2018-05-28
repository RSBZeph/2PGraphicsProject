using OpenTK;

class Sphere : Primitive
{
    public float Radius;
    public Sphere(Vector3 pos, float radius, Vector3 Col, bool mirror)
    {
        Position = pos;
        Radius = radius;
        Color = Col;
        Mirror = mirror;
    }
}
