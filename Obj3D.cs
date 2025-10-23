using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PopesculG_tema04
{
    /// <summary>
    /// Obiect 3D care va "cadea" 
    /// </summary>
    internal class Obj3D
    {
        private bool visible;
        private bool gravity;
        private Color color;
        private List<Vector3> coords;
        private Random random;
        private const int GRAVITY_OFFSET = 1;

        public Obj3D()
        {
            //initializari
            visible = true;
            gravity = true;
            random = new Random();
            color = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            coords = new List<Vector3>();

            int size_offset = random.Next(1, 5); // pentru creare obiecte de dimensiuni diferite
            int height_offset = random.Next(7, 25); // pentru crearea obiecte la inaltimi diferite
            int horizontal_offset = random.Next(-50, 50); // pentru crearea obiecte la pozitii diferite pe axa X
            int depth_offset = random.Next(-50, 50); // pentru crearea obiecte la pozitii diferite pe axa Z
            //coordonate pt un cub
            coords.Add(new Vector3(0 * size_offset + horizontal_offset, 0 * size_offset + height_offset ,1 * size_offset + depth_offset));
            coords.Add(new Vector3(0 * size_offset + horizontal_offset, 0 * size_offset + height_offset, 0 * size_offset + depth_offset));
            coords.Add(new Vector3(1 * size_offset + horizontal_offset, 0 * size_offset + height_offset, 1 * size_offset + depth_offset));
            coords.Add(new Vector3(1 * size_offset + horizontal_offset, 0 * size_offset + height_offset, 0 * size_offset + depth_offset));
            coords.Add(new Vector3(1 * size_offset + horizontal_offset, 1 * size_offset + height_offset, 1 * size_offset + depth_offset));
            coords.Add(new Vector3(1 * size_offset + horizontal_offset, 1 * size_offset + height_offset, 0 * size_offset + depth_offset));
            coords.Add(new Vector3(0 * size_offset + horizontal_offset, 1 * size_offset + height_offset, 1 * size_offset + depth_offset));
            coords.Add(new Vector3(0 * size_offset + horizontal_offset, 1 * size_offset + height_offset, 0 * size_offset + depth_offset));
            coords.Add(new Vector3(0 * size_offset + horizontal_offset, 0 * size_offset + height_offset, 1 * size_offset + depth_offset));
            coords.Add(new Vector3(0 * size_offset + horizontal_offset, 0 * size_offset + height_offset, 0 * size_offset + depth_offset));
        }
        /// <summary>
        /// Comuta vizibilitatea obiectului
        /// </summary>
        public void ToggleVisibility()
        {
            visible = !visible;
        }
        /// <summary>
        /// Comuta efectul gravitatiei asupra obiectului
        /// </summary>
        public void ToggleGravity() {
            gravity = !gravity;
        }
        /// <summary>
        /// Actualizeaza pozitia obiectului in scena
        /// </summary>
        public void UpdatePosition()
        {
            if (visible && gravity && !GroundCollision())
            {
                for (int i = 0; i < coords.Count; i++)
                {
                    coords[i] = new Vector3(coords[i].X, coords[i].Y - GRAVITY_OFFSET, coords[i].Z);
                }
            }
        }
        /// <summary>
        /// Verifica coliziunea cu solul (planul Y=0)
        /// </summary>
        /// <returns></returns>
        public bool GroundCollision()
        {
            foreach (Vector3 v in coords)
            {
                if (v.Y <= 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Deseneaza obiectul 3D
        /// </summary>
        public void Draw()
        {
            if (visible)
            {
                GL.Color3(color);
                GL.Begin(PrimitiveType.QuadStrip);
                foreach (Vector3 v in coords)
                {
                    GL.Vertex3(v);
                }
                GL.End();
                
            }
        }
    }

}
