using OpenTK.Input;
using Template;

class Application
{
    KeyboardState KBS;
    Camera C;
    public Raytracer R;

    public Application(Surface sur)
    {
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
        KBS = Keyboard.GetState();
        if (KBS.IsKeyDown(Key.Plus))
        {
            R.angle += 10;
        }
        if (KBS.IsKeyDown(Key.Minus))
        {
            R.angle -= 10;
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
}