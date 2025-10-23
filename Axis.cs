using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PopesculG_tema04
{
    /// <summary>
    /// Axele sistemului de coordonate
    /// </summary>
    internal class Axis
    {
        private const int length = 100;
        private bool visible;
        public Axis()
        {
            visible = true;
        }
        /// <summary>
        /// Comuta vizibilitatea axelor
        /// </summary>
        public void ToggleVisibility()
        {
            visible = !visible;
        }
        /// <summary>
        /// Deseneaza axele X, Y, Z
        /// </summary>
        public void Draw()
        {
            if (visible)
            {
                GL.LineWidth(3);
                GL.Begin(PrimitiveType.Lines);
                // Axa X - Rosu
                GL.Color3(Color.Red);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(length, 0, 0);
                // Axa Y - Verde
                GL.Color3(Color.Green);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, length, 0);
                // Axa Z - Albastru
                GL.Color3(Color.Blue);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, 0, length);
                GL.End();
            }
        }
    }
}
