using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Classes
{
    public class Decibel
    {
        public int PlayDecibel;
        public int LowestDecibel;
        public bool FinalDecibelToPlay;
        public Decibel() { }
        public Decibel(int initialDecibel) { }


        public void DetermineNextDecibel(string value)
        {
            if (value == "true")
            {
                PlayDecibel = PlayDecibel - 10;
            }
            else
            {
                PlayDecibel = PlayDecibel + 5;
                LowestDecibel = PlayDecibel;
                FinalDecibelToPlay = true;
            }
        }
    }
}
