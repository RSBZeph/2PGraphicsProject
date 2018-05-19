using OpenTK;

class Sphere : Primitive
{
    public Vector3 Position;
    public float Radius;

    public Sphere(Vector3 pos, float radius, Vector3 Col)
    {
        Position = new Vector3(pos.X, 10 - pos.Y, pos.Z);
        Radius = radius;
        Color = Col;
    }
}
