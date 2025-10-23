using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopesculG_tema04
{
    /// <summary>
    /// Punctul de intrare in aplicatie
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Functia principala
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // pornire fereastra 3D
            using (Window3D window = new Window3D())
            {
                window.Run(30.0);
            }
        }
    }
}
