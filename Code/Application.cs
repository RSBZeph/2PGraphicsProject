using OpenTK;
using OpenTK.Input;
using System;
using Template;

class Application
{
    KeyboardState KBS;
    Camera C;
    public Raytracer R;
    Surface Screen;

    public Application(Surface sur)
    {
        Screen = sur;
        R = new Raytracer(sur);
        C = Camera.Instance();
    }

    public void Tick()
    {
        Input();
        R.Render();
    }

    //checks for input and moves or turns the camera accordingly
    void Input()
    {
        bool horizontal = false, vertical = false;
        KBS = Keyboard.GetState();
        if (KBS.IsKeyDown(Key.I))
        {
            R.yangle += 10;
            vertical = true;
        }
        if (KBS.IsKeyDown(Key.K))
        {
            R.yangle -= 10;
            vertical = true;
        }
        if (KBS.IsKeyDown(Key.J))
        {
            R.angle += 10;
            horizontal = true;
        }
        if (KBS.IsKeyDown(Key.L))
        {
            R.angle -= 10;
            horizontal = true;
        }
        if (KBS.IsKeyDown(Key.Plus))
        {
            C.FOV+= 10;
        }
        if (KBS.IsKeyDown(Key.Minus))
        {
            C.FOV-= 10;
        }

        if (horizontal)
        {
            C.Direction.X = pointoncircle(R.angle).X;
            C.Direction.Z = pointoncircle(R.angle).Y;
            C.Direction = Vector3.Normalize(C.Direction);
            C.Right = Vector3.Normalize(-Vector3.Cross(C.Direction, C.Up));
        }
        if (vertical)
        {
            C.Direction.Z = pointoncircle(R.yangle, false).X;
            //C.Direction.Y = pointoncircle(R.yangle, false).Y;
            C.Direction = Vector3.Normalize(C.Direction);
            C.Up = Vector3.Normalize(Vector3.Cross(C.Direction, C.Right));
        }

        if (KBS.IsKeyDown(Key.A))
        {
            C.Position -= 0.25f * C.Right;
            C.P0 -= 0.25f * C.Right;
            C.P1 -= 0.25f * C.Right;
            C.P2 -= 0.25f * C.Right;
        }
        else if (KBS.IsKeyDown(Key.D))
        {
            C.Position += 0.25f * C.Right;
            C.P0 += 0.25f * C.Right;
            C.P1 += 0.25f * C.Right;
            C.P2 += 0.25f * C.Right;
        }
        if (KBS.IsKeyDown(Key.S))
        {
            C.Position -= 0.25f * C.Direction;
            C.P0 -= 0.25f * C.Direction;
            C.P1 -= 0.25f * C.Direction;
            C.P2 -= 0.25f * C.Direction;
        }
        else if (KBS.IsKeyDown(Key.W))
        {
            C.Position += 0.25f * C.Direction;
            C.P0 += 0.25f * C.Direction;
            C.P1 += 0.25f * C.Direction;
            C.P2 += 0.25f * C.Direction;
        }
        if (KBS.IsKeyDown(Key.Q))
        {
            C.Position -= 0.25f * C.Up;
            C.P0 -= 0.25f * C.Up;
            C.P1 -= 0.25f * C.Up;
            C.P2 -= 0.25f * C.Up;
        }
        else if (KBS.IsKeyDown(Key.E))
        {
            C.Position += 0.25f * C.Up;
            C.P0 += 0.25f * C.Up;
            C.P1 += 0.25f * C.Up;
            C.P2 += 0.25f * C.Up;
        }
    }

    Vector2 pointoncircle(double angle, bool horizontal = true)
    {
        float radius = (float)Math.Sqrt(C.DistanceToOrigin * C.DistanceToOrigin + 1);
        double radangle = angle * Math.PI / 180;
        float x = (int)(C.Position.X + radius * Math.Cos(radangle));
        float z = (int)(C.Position.Z + radius * Math.Sin(radangle));
        if (!horizontal)
            return new Vector2(x, z) - new Vector2(C.Position.X, C.Position.Y);
        return new Vector2(x, z) - new Vector2(C.Position.X, C.Position.Z);
    }
}