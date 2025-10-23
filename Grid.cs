using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PopesculG_tema04
{
    /// <summary>
    /// Grila pe planul XZ
    /// </summary>
    internal class Grid
    {
        private bool visible;
        private float size;
        private int divisions;
        private Color color;
        public Grid(float size, int divisions, Color color)
        {
            this.size = size;
            this.divisions = divisions;
            this.visible = true;
            this.color = color;
        }
        /// <summary>
        /// Comuta vizibilitatea grilei
        /// </summary>
        public void ToggleVisibility()
        {
            visible = !visible;
        }
        /// <summary>
        /// Seteaza culoarea grilei
        /// </summary>
        /// <param name="newColor"></param>
        public void SetColor(Color newColor)
        {
            color = newColor;
        }
        /// <summary>
        /// Deseneaza grila
        /// </summary>
        public void Draw()
        {
            if (visible) { 
                GL.Color3(color);
                GL.LineWidth(1);
                GL.Begin(PrimitiveType.Lines);
                float step = size / divisions;
                for (int i = -divisions; i <= divisions; i++)
                {
                    GL.Vertex3(i * step, 0, -size);
                    GL.Vertex3(i * step, 0, size);
                    GL.Vertex3(-size, 0, i * step);
                    GL.Vertex3(size, 0, i * step);
                }
                GL.End();
            }
        }
    }
}
