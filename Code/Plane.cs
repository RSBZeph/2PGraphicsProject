using OpenTK;

class Plane : Primitive
{
    //stores the values we need for plane
    public Vector3 Dimension1, Dimension2, Normal;
    public float  width, height;
    public bool checkerboard = false, finite = true;

    public Plane(Vector3 P0, Vector3 dimension1, Vector3 dimension2, Vector3 Col)
    {
        //if we make a instance we fill in the values
        Position = P0;
        Dimension1 = Vector3.Normalize(dimension1);
        Dimension2 = Vector3.Normalize(dimension2);
        if (Dimension1 == Vector3.Zero || Dimension2 == Vector3.Zero)
            finite = false;
        Normal = -Vector3.Normalize(Vector3.Cross(dimension1,dimension2));
        Color = Col;
    }
}