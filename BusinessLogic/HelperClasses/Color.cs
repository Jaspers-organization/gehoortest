﻿namespace BusinessLogic.HelperClasses;

public class Color
{
    public byte Alpha { get;set; }
    public byte Red { get;set; }
    public byte Green { get;set; }
    public byte Blue { get;set; }  
    public string Hex { get;set; }

    public Color(byte alpha,byte red, byte green, byte blue, string hex)
    {
        Alpha = alpha;
        Red = red;
        Green = green;
        Blue = blue;
        Hex = hex;
    }
}
