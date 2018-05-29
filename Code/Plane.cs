using OpenTK;

class Plane : Primitive
{
    public Vector3 Dimension1, Dimension2, Normal;
    public float  width, height;
    public bool checkerboard = false;

    public Plane(Vector3 P0, Vector3 dimension1, Vector3 dimension2, Vector3 Col)
    {
        Position = P0;
        Dimension1 = Vector3.Normalize(dimension1);
        Dimension2 = Vector3.Normalize(dimension2);
        Normal = -Vector3.Normalize(Vector3.Cross(dimension1,dimension2));
        Color = Col;
    }
}