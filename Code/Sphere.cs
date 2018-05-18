using OpenTK;

class Sphere : Primitive
{
    public Vector3 Position;
    public float Radius;

    public Sphere(Vector3 pos, float radius, Vector3 Col)
    {
        Position = pos;
        Radius = radius;
        Color = Col;
    }
}
