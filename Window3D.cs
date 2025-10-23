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
    /// <summary>
    /// Fereastra principala
    /// </summary>

    internal class Window3D : GameWindow
    {
        // input tastatura
        KeyboardState previousKeys;
        //declarare obiecte
        Grid grid;
        Camera3D cam;
        Axis axis;
        Random random;

        public Window3D(): base(800,600,new GraphicsMode(32,24,0,8))
        {
            VSync = VSyncMode.On;
            // initializari
            grid = new Grid(30, 10,Color.Aqua);
            cam = new Camera3D(new Vector3(30, 30, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            axis = new Axis();
            random = new Random();
            // ajutor
            Help();
        }
        /// <summary>
        /// Atunci cand fereastra este incarcata
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black); //culoare fundal
            // pentru randare 3d
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            // setari AA (antialiasing)
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }
        /// <summary>
        /// Atunci cand fereastra este redimensionata
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // perspectiva
            GL.Viewport(0, 0, Width, Height);
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / (float)Height, 1, 256);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            // camera
            cam.UpdateCamera();
        }
        /// <summary>
        /// Pentru preluarea comenzilor de tastatura/mouse
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // comenzi tastatura
            KeyboardState currentKeys = Keyboard.GetState();
            if (currentKeys.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            if (currentKeys.IsKeyDown(Key.H) && !previousKeys.IsKeyDown(Key.H))
            {
                Help();
            }
            if (currentKeys.IsKeyDown(Key.G) && !previousKeys.IsKeyDown(Key.G))
            {
                grid.ToggleVisibility();
            }
            if (currentKeys.IsKeyDown(Key.R) && !previousKeys.IsKeyDown(Key.R))
            {
                grid.SetColor(Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));
            }
            if (currentKeys.IsKeyDown(Key.X) && !previousKeys.IsKeyDown(Key.X))
            {
                axis.ToggleVisibility();
            }
            // miscare camera
            if (currentKeys.IsKeyDown(Key.Q))
            {
                cam.Move(new Vector3(0, 1, 0));
                cam.UpdateCamera();
            }
            if (currentKeys.IsKeyDown(Key.E))
            {
                cam.Move(new Vector3(0, -1, 0));
                cam.UpdateCamera();
            }
            if (currentKeys.IsKeyDown(Key.A))
            {
                cam.Move(new Vector3(-1, 0, 0));
                cam.UpdateCamera();
            }
            if (currentKeys.IsKeyDown(Key.D))
            {
                cam.Move(new Vector3(1, 0, 0));
                cam.UpdateCamera();
            }
            if (currentKeys.IsKeyDown(Key.W))
            {
                cam.Move(new Vector3(0, 0, -1));
                cam.UpdateCamera();
            }
            if (currentKeys.IsKeyDown(Key.S))
            {
                cam.Move(new Vector3(0, 0, 1));
                cam.UpdateCamera();
            }
            previousKeys = currentKeys;
        }
        /// <summary>
        /// Pentru desenare in fereastra
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            // curatare buffere
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            // desenare obiecte
            grid.Draw();
            axis.Draw();

            // incarcare scena
            SwapBuffers();
        }
        /// <summary>
        /// Afiseaza ajutorul in consola
        /// </summary>
        private void Help()
        {
            Console.WriteLine("Ajutor: ");
            Console.WriteLine("ESC - iesire");
            Console.WriteLine("H - ajutor");
            Console.WriteLine("G - vizibilitate grid");
            Console.WriteLine("R - culoare grid aleatoare");
            Console.WriteLine("X - vizibilitate axe");
            Console.WriteLine("W,A,S,D,Q,E - miscare camera");
        }
    }
}
