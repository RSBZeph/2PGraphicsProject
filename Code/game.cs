using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.IO;

namespace Template
{
    class Game
    {
        // member variables
        public Surface screen;
        int originx, originy, straal = 1103228 / 7801, slowdowncounter = 0;
        int LU = 45, LD = 135, RD = 225, RU = 315, angle = 0;

        Surface map;
        double scale, heightscale, squarewidth = 0.5, distancebetweensquareborders = 0.00;
        float[,] h;
        float cameraoffsetx = 0;
        float cameraoffsety = 0;
        float changingangle = 0, xangle = 0, yangle = 0, zangle = 0;
        float xdistance = 0, ydistance = 0, zdistance = -90;

        float[] vertexData;
        int vertexnr = 0;
        int VBO;
        int programID, vsID, fsID, vbo_pos, vbo_color;
        int attribute_vpos, attribute_vcol, uniform_mview;
        KeyboardState KBS;

        public void Init()
        {
            originx = screen.width / 2;
            originy = screen.height / 2;

            map = new Surface(".../.../assets/heightmap.png");
            h = new float[128, 128];
            for (int y = 0; y < 128; y++)
                for (int x = 0; x < 128; x++)
                    h[x, y] = ((float)(map.pixels[x + y * 128] & 255)) / 256;
            scale = 2 * squarewidth + distancebetweensquareborders;
            heightscale = (128 * squarewidth) / 2;

            vertexData = new float[128 * 128 * 4 * 3];
            for (int x = 0; x < 128; x++)
                for (int y = 0; y < 128; y++)
                {
                    FillInArray(x, y);
                }
            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData<float>
                (
                BufferTarget.ArrayBuffer,
                (IntPtr)(vertexData.Length * 4),
                vertexData,
                BufferUsageHint.StaticDraw
                );
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, 12, 0);
        }

        public void Tick()
        {
            screen.Clear(0);
            //screen.Print("hello world", 2, 2, 0xffffff);
            //screen.Line(2, 20, 160, 20, 0xff0000);

            //GradientSquare();
            //RotatingSquare();

            //slowdowncounter++;
            //if (slowdowncounter == 3)
            //{
            //    changingangle += 1;
            //    if (changingangle >= 360)
            //        changingangle -= 360;
            //    slowdowncounter = 0;
            //}


            //-----SHADER-----
            //makeprogram();
            //accesshadervariables();
            //linkpositionandcolor();

            //Matrix4 M = Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), xangle);
            //M *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), 1.9f);
            //M *= Matrix4.CreateTranslation(0, 0, -1);
            //M *= Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);

            //GL.UseProgram(programID);
            //GL.UniformMatrix4(uniform_mview, false, ref M);

            //GL.EnableVertexAttribArray(attribute_vpos);
            //GL.EnableVertexAttribArray(attribute_vcol);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 127 * 127 * 2 * 3);
        }

        //void GradientSquare()
        //{
        //    int xoffset = (screen.width - 256) / 2;
        //    int yoffset = (screen.height - 256) / 2;
        //    for (int x = xoffset; x < 256 + xoffset; x++)
        //        for (int y = yoffset; y < 256 + yoffset; y++)
        //        {
        //            screen.pixels[x + y * screen.width] = CreateColor(x - xoffset, y - yoffset, 0);
        //        }
        //}

        //int CreateColor(int red, int green, int blue)
        //{
        //    return (red << 16) + (green << 8) + blue;
        //}

        //void RotatingSquare()
        //{
        //    screen.Line(originx + TX(changingangle + LU), originy + TY(changingangle + LU), originx + TX(changingangle + RU), originy + TY(changingangle + RU), 0xf009f0);
        //    screen.Line(originx + TX(changingangle + RU), originy + TY(changingangle + RU), originx + TX(changingangle + RD), originy + TY(changingangle + RD), 0xf009f0);
        //    screen.Line(originx + TX(changingangle + RD), originy + TY(changingangle + RD), originx + TX(changingangle + LD), originy + TY(changingangle + LD), 0xffffff);
        //    screen.Line(originx + TX(changingangle + LD), originy + TY(changingangle + LD), originx + TX(changingangle + LU), originy + TY(changingangle + LU), 0xffffff);
        //    screen.pixels[originx + originy * screen.width] = CreateColor(255, 0, 0);
        //}

        //int TX(float a)
        //{
        //    float angle = (float)(Math.PI * a / 180.0);
        //    int x = (int)(Math.Cos(angle) * straal);
        //    return x;
        //}

        //int TY(float a)
        //{
        //    float angle = (float)(Math.PI * a / 180.0);
        //    int y = (int)(Math.Sin(angle) * straal);
        //    return y;
        //}

        public void RenderGL()
        {
            KBS = Keyboard.GetState();
            xangle = KBStoValue(Key.A, Key.D, xangle);
            yangle = KBStoValue(Key.W, Key.S, yangle);
            zangle = KBStoValue(Key.Q, Key.E, zangle);
            xdistance = KBStoValue(Key.Y, Key.U, xdistance);
            ydistance = KBStoValue(Key.H, Key.J, ydistance);
            zdistance = KBStoValue(Key.N, Key.M, zdistance);

            //var M = Matrix4.CreatePerspectiveFieldOfView(1.0f, 1f, .1f, 1000);
            //GL.LoadMatrix(ref M);
            //GL.Translate(xdistance, ydistance, zdistance);
            //GL.Rotate(300, 1, 0, 0);
            //GL.Rotate(xangle, 0, 1, 0);
            //GL.Rotate(yangle, 1, 0, 0);   
            //GL.Rotate(zangle, 0, 0, 1);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            //GL.DrawArrays(PrimitiveType.Quads, 0, 127 * 127 * 2 * 3);

            DrawMethod1();
            //DrawMethod2();
        }

        float KBStoValue(Key first, Key second, float value)
        {
            if (KBS.IsKeyDown(first))
                return value + 1;
            else if (KBS.IsKeyDown(second))
                return value - 1;
            return value;
        }

        void DrawMethod1()
        {
            var M = Matrix4.CreatePerspectiveFieldOfView(1.0f, 1f, .1f, 1000);
            GL.LoadMatrix(ref M);
            GL.Translate(xdistance, ydistance, zdistance);
            GL.Rotate(300, 1, 0, 0);
            GL.Rotate(xangle, 0, 1, 0);
            GL.Rotate(yangle, 1, 0, 0);
            GL.Rotate(zangle, 0, 0, 1);

            for (int x = 0; x < 128; x++)
                for (int y = 0; y < 128; y++)
                {
                    GL.Color3(0.5 * h[x, y], 2 * h[x, y], h[x, y]);
                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex3(cameraoffsetx + x * scale - 129 * squarewidth, cameraoffsety + y * scale - 129 * squarewidth, heightscale * (h[A(x), A(y)]));
                    GL.Vertex3(cameraoffsetx + x * scale - 127 * squarewidth, cameraoffsety + y * scale - 129 * squarewidth, heightscale * (h[x, A(y)]));
                    GL.Vertex3(cameraoffsetx + x * scale - 127 * squarewidth, cameraoffsety + y * scale - 127 * squarewidth, heightscale * (h[x, y]));
                    GL.Vertex3(cameraoffsetx + x * scale - 129 * squarewidth, cameraoffsety + y * scale - 127 * squarewidth, heightscale * (h[A(x), y]));
                    GL.End();
                }
        }

        void DrawMethod2()
        {
            var M = Matrix4.CreatePerspectiveFieldOfView(1.6f, 1f, .1f, 1000);
            GL.LoadMatrix(ref M);
            // ShaderPrep
            //x = -10 y = -130 z = 70
            GL.Translate(xdistance, ydistance, zdistance);
            GL.Rotate(300, 1, 0, 0);
            GL.Rotate(xangle, 0, 1, 0);
            GL.Rotate(yangle, 1, 0, 0);
            GL.Rotate(zangle, 0, 0, 1);

            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex3(-0.5f, -0.5f, 0);
            GL.Vertex3(0.5f, -0.5f, 0);
            GL.Vertex3(-0.5f, 0.5f, 0);
            GL.End();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.DrawArrays(PrimitiveType.Quads, 0, 127 * 127 * 2 * 3);
        }

        void ShaderPrep()
        {
            Matrix4 M = Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);
            M *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), 1.9f);
            M *= Matrix4.CreateTranslation(0, 0, -1);
            M *= Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);
            GL.UseProgram(programID);
            GL.UniformMatrix4(uniform_mview, false, ref M);
            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcol);
            GL.DrawArrays(PrimitiveType.Quads, 0, 127 * 127 * 2 * 3);
        }

        void makeprogram()
        {
            programID = GL.CreateProgram();
            LoadShader("../../shaders/vs.glsl", ShaderType.VertexShader, programID, out vsID);
            LoadShader("../../shaders/fs.glsl", ShaderType.FragmentShader, programID, out fsID);
            GL.LinkProgram(programID);
        }

        void accesshadervariables()
        {
            attribute_vpos = GL.GetAttribLocation(programID, "vPosition");
            attribute_vcol = GL.GetAttribLocation(programID, "vColor");
            uniform_mview = GL.GetUniformLocation(programID, "M");
        }

        void linkpositionandcolor()
        {
            vbo_pos = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_pos);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(vertexData.Length * 4), vertexData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            vbo_color = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(vertexData.Length * 4), vertexData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vcol, 3, VertexAttribPointerType.Float, false, 0, 0);
        }

        void LoadShader(String name, ShaderType type, int program, out int ID)
        {
            ID = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(name)) GL.ShaderSource(ID, sr.ReadToEnd());
            GL.CompileShader(ID);
            GL.AttachShader(program, ID);
            Console.WriteLine(GL.GetShaderInfoLog(ID));
        }

        void FillInArray(int x, int y)
        {
            vertexData[vertexnr] = (float)(cameraoffsetx + x * scale - 129 * squarewidth);
            vertexData[vertexnr + 1] = (float)(cameraoffsety + y * scale - 129 * squarewidth);
            vertexData[vertexnr + 2] = (float)(heightscale * (h[A(x), A(y)]));
            vertexnr += 3;
            vertexData[vertexnr] = (float)(cameraoffsetx + x * scale - 127 * squarewidth);
            vertexData[vertexnr + 1] = (float)(cameraoffsety + y * scale - 129 * squarewidth);
            vertexData[vertexnr + 2] = (float)(heightscale * (h[x, A(y)]));
            vertexnr += 3;
            vertexData[vertexnr] = (float)(cameraoffsetx + x * scale - 127 * squarewidth);
            vertexData[vertexnr + 1] = (float)(cameraoffsety + y * scale - 127 * squarewidth);
            vertexData[vertexnr + 2] = (float)(heightscale * (h[x, y]));
            vertexnr += 3;
            vertexData[vertexnr] = (float)(cameraoffsetx + x * scale - 129 * squarewidth);
            vertexData[vertexnr + 1] = (float)(cameraoffsety + y * scale - 127 * squarewidth);
            vertexData[vertexnr + 2] = (float)(heightscale * (h[A(x), y]));
            vertexnr += 3;
        }

        int A(double input)
        {
            return (int)Math.Max(0, input - 1);
        }
    }
}