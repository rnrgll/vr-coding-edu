using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum PortType
    {
        Flow,
        Data,
    }
    public enum PortDirection
    {
        In,
        Out,
    }
    public enum DataType
    {
        None,
        Int,
        Bool,
        String,
    }

    public enum Layer
    {
        Interaction = 6,
    }

    public enum ColorName
    {
        White,
        Yellow,
        Green,
        Orange,
        DarkGray,
        Gray,
        Coral,
        SkyBlue,
        Blue,
        Red,
        Purple,
        LimeGreen,

    }
    public static readonly Dictionary<ColorName, Color> ColorDict = new()
    {
        { ColorName.White,     new Color32(255, 255, 255, 255) },
        { ColorName.Yellow,    new Color32(253, 195, 42, 255) },
        { ColorName.Green,     new Color32(45, 195, 129, 255) },
        { ColorName.Orange,    new Color32(226, 110, 40, 255) },
        { ColorName.DarkGray,  new Color32(57, 57, 57, 255) },
        { ColorName.Gray,      new Color32(99, 108, 104, 255) },
        { ColorName.Coral,     new Color32(251, 112, 113, 255) },
        { ColorName.SkyBlue,   new Color32(90, 209, 227, 255) },
        { ColorName.Blue,      new Color32(31, 116, 206, 255) },
        { ColorName.Red,       new Color32(239, 62, 69, 255) },
        { ColorName.Purple,    new Color32(144, 99, 217, 255) },
        { ColorName.LimeGreen, new Color32(99, 193, 50, 255) },

    };
    
    public enum CatAnimation
    {
        IdleC= 36,
        Jump = 50,
        Sit = 24,
        ATK1 = 37,
        Meow = 11,
        Walk = 21,
        
    }
}