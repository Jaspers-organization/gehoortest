using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class Color
    {
        public byte Red { get;set; }
        public byte Green { get;set; }
        public byte Blue { get;set; }
        public byte Alpha { get;set; }
        public string Hex { get;set; }

        public Color(byte red, byte green, byte blue, byte alpha, string hex)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
            Hex = hex;
        }
    }
}
