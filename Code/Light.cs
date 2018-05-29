using OpenTK;

class Light
{
    //stores the values we need for light
    public Vector3 Position;
    public float Intensity;

    public Light(Vector3 pos, float intensity)
    {
        Position = pos;
        Intensity = intensity;
    }
}