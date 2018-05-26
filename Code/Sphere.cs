using OpenTK;

class Sphere : Primitive
{
    public float Radius;
    public bool Mirror;
    public Sphere(Vector3 pos, float radius, Vector3 Col, bool mirror)
    {
        Position = pos; //new Vector3(pos.X, 10 - pos.Y, pos.Z);
        Radius = radius;
        Color = Col;
        Mirror = mirror;
    }
}
