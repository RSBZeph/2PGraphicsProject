using OpenTK;

class Light
{
    public Vector3 Position;
    public float Intensity;

    public Light(Vector3 pos, float intensity)
    {
        Position = pos;
        Intensity = intensity;
    }
}