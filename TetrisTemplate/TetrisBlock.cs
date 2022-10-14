using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class TetrisBlock
{
    int color;
    bool[,] arrayBlock = new bool[4, 4];
    public TetrisBlock()
	{
	}

    public void Rotate(int times)
    {
        int arrayLength = arrayBlock.GetLength(1);
        for (int k = 0; k < times; k++)
        {
            for (int i = 0; i < arrayLength; i++)
            {
                for (int j = 0; j < arrayLength; j++)
                {
                    bool temp = arrayBlock[i, j];
                    arrayBlock[i, j] = arrayBlock[j, i];
                    arrayBlock[j, i] = temp;
                }
            }

            for (int i = 0; i < arrayLength; i++)
            {
                for (int j = 0; j < (arrayLength / 2); j++)
                {
                    bool temp = arrayBlock[i, j];
                    arrayBlock[i, j] = arrayBlock[i, arrayLength - 1 - j];
                    arrayBlock[i, arrayLength - 1 - j] = temp;
                }
            }
        }
    }

    //public bool AllowedHere(int[,] arrayGrid, Point position, int cellSize)
    //{

    //}

    virtual  public bool[,] Array { get { return arrayBlock; } }
    virtual public int Color { get { return color; } }
}

public class L : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, true, false, false},
        {false, true, false, false},
        {false, true, true, false},
        {false, false, false, false},
    };
    //Color = Orange
    int color = 2;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
}

public class J : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, false, true, false},
        {false, false, true, false},
        {false, true, true, false},
        {false, false, false, false},
    };
    //Color = Blue
    int color = 1;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
}

public class I : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, true, false, false},
        {false, true, false, false},
        {false, true, false, false},
        {false, true, false, false},
    };
    //Color = Cyan
    int color = 7;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
}

public class O : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, false, false, false},
        {false, true, true, false},
        {false, true, true, false},
        {false, false, false, false},
    };
    //Color = Yellow
    int color = 3;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
}

public class Z : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, false, false, false},
        {true, true, false, false},
        {false, true, true, false},
        {false, false, false, false},

    };
    //Color = Red
    int color = 6;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
}

public class S : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, false, false, false},
        {false, true, true, false},
        {true, true, false, false},
        {false, false, false, false},
    };
    //Color = Green
    int color = 4;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
}

public class T : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, false, false, false},
        {false, true, false, false},
        {true, true, true, false},
        {false, false, false, false},
    };
    //Color = Purple
    int color = 5;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
}