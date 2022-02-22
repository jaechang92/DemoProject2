using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerColor
{
    Red,
    Blue,
    Green,
    Pink,
    Orange,
    Yellow,
    Black,
    White,
    Purple,
    Brown,
    Cyan,
    Lime,

    Max,
}

public class PlayerColor
{
    private static List<Color> colors = new List<Color>()
    {
        new Color(1.0f, 0.0f, 0.0f),
        new Color(0.1f, 0.1f, 1.0f),
        new Color(0.0f, 0.6f, 0.0f),
        new Color(1.0f, 0.3f, 0.9f),
        new Color(1.0f, 0.4f, 0.0f),
        new Color(1.0f, 0.9f, 0.1f),
        new Color(0.2f, 0.2f, 0.2f),
        new Color(0.9f, 1.0f, 1.0f),
        new Color(0.6f, 0.0f, 0.6f),
        new Color(0.7f, 0.2f, 0.0f),
        new Color(0.0f, 1.0f, 1.0f),
        new Color(0.1f, 1.0f, 0.1f),
    };


    public static Color GetColor(EPlayerColor playerColor) 
    {
        return colors[(int)playerColor];
    }



}
