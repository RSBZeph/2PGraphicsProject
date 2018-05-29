using OpenTK;

class Sphere : Primitive
{
    //stores the values we need for sphere
    public float Radius;
    public Sphere(Vector3 pos, float radius, Vector3 Col, bool mirror)
    {
        //if we make a instance we fill in the values
        Position = pos;
        Radius = radius;
        Color = Col;
        Mirror = mirror;
    }
}
