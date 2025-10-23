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
        // input periferice
        KeyboardState previousKeys;
        MouseState previousMouse;
        //declarare obiecte
        Grid grid;
        Camera3D cam;
        Axis axis;
        Random random;
        List<Obj3D> objs;

        public Window3D(): base(800,600,new GraphicsMode(32,24,0,8))
        {
            VSync = VSyncMode.On;
            // initializari
            grid = new Grid(100, 10);
            cam = new Camera3D(new Vector3(30, 30, 30), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            axis = new Axis();
            random = new Random();
            objs = new List<Obj3D>();
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

            // Actualizare pozitie obiecte
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
            // parasire program
            if (currentKeys.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            // mesaj ajutor
            if (currentKeys.IsKeyDown(Key.H) && !previousKeys.IsKeyDown(Key.H))
            {
                Help();
            }
            // vizibilitate grila
            if (currentKeys.IsKeyDown(Key.Z) && !previousKeys.IsKeyDown(Key.Z))
            {
                grid.ToggleVisibility();
            }
            // vizibilitate axe
            if (currentKeys.IsKeyDown(Key.X) && !previousKeys.IsKeyDown(Key.X))
            {
                axis.ToggleVisibility();
            }
            // culoare grila aleatoare
            if (currentKeys.IsKeyDown(Key.C) && !previousKeys.IsKeyDown(Key.C))
            {
                grid.SetColor(Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));
            }
            // vizibilitate obiecte
            if (currentKeys.IsKeyDown(Key.V) && !previousKeys.IsKeyDown(Key.V))
            {
                foreach (Obj3D obj in objs)
                {
                    obj.ToggleVisibility();
                }
            }
            // resetare scena
            if (currentKeys.IsKeyDown(Key.R) && !previousKeys.IsKeyDown(Key.R))
            {
                objs = new List<Obj3D>();
                grid.ResetColor();
            }

            // miscare camera pe axe
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
                cam.Move(new Vector3(0, 0, 1));
                cam.UpdateCamera();
            }
            if (currentKeys.IsKeyDown(Key.S))
            {
                cam.Move(new Vector3(0, 0, -1));
                cam.UpdateCamera();
            }
            previousKeys = currentKeys;

            // comenzi mouse
            MouseState currentMouse = Mouse.GetState();
            if (currentMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                objs.Add(new Obj3D());
            }
            previousMouse = currentMouse;

            foreach (Obj3D obj in objs)
            {
                obj.UpdatePosition();
            }
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
            foreach (Obj3D obj in objs)
            {
                obj.Draw();
            }

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
            Console.WriteLine("Z - vizibilitate grid");
            Console.WriteLine("X - vizibilitate axe");
            Console.WriteLine("C - culoare grid aleatoare");
            Console.WriteLine("V - vizibilitate obiecte");
            Console.WriteLine("R - resetare scena");
            Console.WriteLine("W,A,S,D,Q,E - miscare camera");
            Console.WriteLine("Click stanga mouse - creare obiect");
        }
    }
}
