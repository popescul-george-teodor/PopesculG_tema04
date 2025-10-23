using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace PopesculG_tema04
{

    internal class Window3D : GameWindow
    {
        KeyboardState previousKeys;
        MouseState previousMouse;
        
        public Window3D(): base(800,600,new GraphicsMode(32,24,0,8))
        {
            VSync = VSyncMode.On;
            Help();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Aqua); //culoare fundal
            // pentru randare 3d
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            // setari AA (antialiasing)
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // perspectiva
            GL.Viewport(0, 0, Width, Height);
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / (float)Height, 1, 256);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            // camera
            Matrix4 lookat = Matrix4.LookAt(30,30,30,0,0,0,0,1,0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState currentKeys = Keyboard.GetState();
            if (currentKeys.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (currentKeys.IsKeyDown(Key.H) && !previousKeys.IsKeyDown(Key.H))
            {
                Help();
            }

            previousKeys = currentKeys;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            // curatare buffere
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            // desenam scena
            SwapBuffers();
        }

        private void Help()
        {
            Console.WriteLine("Ajutor: ");
            Console.WriteLine("ESC - iesire");
            Console.WriteLine("H   - ajutor");
        }
    }
}
