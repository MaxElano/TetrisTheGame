using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class TetrisBlock
{
    int color;
    bool[,] arrayBlock = new bool[4, 4];
    Point position;
    static double previousTime = 2000;
    static double deltaTime = 1000, deltaDifference = 1.0;
    bool blockAction = true;


    public TetrisBlock()
	{
    }

    //Automaticly moves down the block
    public void Update(GameTime gameTime, TetrisGrid grid, int level)
    {
        if (gameTime.TotalGameTime.TotalMilliseconds - previousTime > deltaTime / (deltaDifference + 0.5*level))
        {
            previousTime = gameTime.TotalGameTime.TotalMilliseconds;
            PositionY += 1;
            if (!AllowedHere(grid.ArrayGrid))
            {
                PositionY -= 1;
                PlaceOnGrid(grid);
                blockAction = false;
                grid.FullRow();
            }
        }
    }

    public bool BlockAction { get { return blockAction; } }

    //Rotates the block 90 degrees * the amount of times
    public void Rotate(bool[,] block, int times)
    {
        //Amount of rotations
        for (int k = 0; k < times; k++)
        {
            //Mirror the array in the main diagonal
            for (int i = 0; i < Size; i++)
            {
                for (int j = i; j < Size; j++)
                {
                    bool temp = block[i, j];
                    block[i, j] = block[j, i];
                    block[j, i] = temp;
                }
            }
            //Mirror the array in the Y axis 
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < (Size / 2); j++)
                {
                    bool temp = block[i, j];
                    block[i, j] = block[i, Size - 1 - j];
                    block[i, Size - 1 - j] = temp;
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, TetrisGrid grid)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Color == 11)
                {
                    if (Array[j, i])
                        spriteBatch.Draw(grid.BombCell, new Vector2((position.X + i) * grid.CellSize, (position.Y + j) * grid.CellSize), grid.WhichColor(this.Color));
                }
                else
                {
                    if (Array[j, i])
                        spriteBatch.Draw(grid.EmptyCell, new Vector2((position.X + i) * grid.CellSize, (position.Y + j) * grid.CellSize), grid.WhichColor(this.Color));
                }
            }
        }
    }

    //Places the block that is moving on to the fixed grid
    public int[,] PlaceOnGrid(TetrisGrid grid)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Color == 11)
                {
                    if (position.Y + j >= 0 && position.Y + j < grid.Height && position.X + i >= 0 && position.X + i < grid.Width)
                    {
                        grid.ArrayGrid[j + position.Y, i + position.X] = 0;
                        grid.EffectGrid[j + position.Y, i + position.X] = 1;
                    }
                    grid.bombBool = true;
                }
                else
                    if (Array[j, i])
                        grid.ArrayGrid[j + position.Y, i + position.X] = Color;
            }
        }
        return grid.ArrayGrid;
    }

    //Checks whether the block is allowed on that location
    public bool AllowedHere(int[,] grid)
    {
        Vector4 actualBlock = ActualBlockGrid(Array);
        if (position.X + actualBlock.X < 0 || position.X + actualBlock.Y > grid.GetLength(1) - 1 || position.Y + actualBlock.Z > grid.GetLength(0) - 1)
        {
            return false;
        }
        for (int i = 0; i < Size; i++)
        {
            for (int j = (int)actualBlock.W; j < Size; j++)
            {
                if (Array[j, i])
                {
                    if (position.Y + j < 0)
                    {
                        return false;
                    }
                    if (grid[position.Y + j, position.X + i] != 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //Gets the "actual" size of the moving block, usefull for checking the grid borders with the "actual" borders of the block
    public Vector4 ActualBlockGrid(bool[,] array)
    {
        int leftSide = 0;
        int rightSide = 0;
        int botSide = 0;
        int topSide = 0;
        bool checkLeft = true;
        bool checkRight = true;
        bool checkBot = true;
        bool checkTop = true;

        for (int i = 0; i < Size && checkLeft; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (array[j, i])
                {
                    leftSide = i;
                    checkLeft = false;
                }
            }
        }

        for (int i = Size - 1; i >= 0 && checkRight; i--)
        {
            for (int j = Size - 1; j >= 0; j--)
            {
                if (array[j, i])
                {
                    rightSide = i;
                    checkRight = false;
                }
            }
        }

        for (int j = Size - 1; j >= 0 && checkBot; j--)
        {
            for (int i = 0; i < Size; i++)
            {
                if (array[j, i])
                {
                    botSide = j;
                    checkBot = false;
                }
            }
        }
        
        for (int j = 0; j < Size && checkTop; j++)
        {
            for (int i = 0; i < Size; i++)
            {
                if (array[j, i])
                {
                    topSide = j;
                    checkTop = false;
                }
            }
        }

        return new Vector4(leftSide, rightSide, botSide, topSide);
    }

    virtual public bool[,] Array { get { return arrayBlock; } }
    virtual public int Color { get { return color; } }
    virtual public int Size { get { return Array.GetLength(0); } }
    public int PositionX { get { return position.X; } set { position.X = value; } }
    public int PositionY { get { return position.Y; } set { position.Y = value; } }
}

public class L : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, true, false},
        {false, true, false},
        {false, true, true},    
    };
    //Color = Orange
    int color = 2;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class J : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, false, true},
        {false, false, true},
        {false, true, true},
    };
    //Color = Blue
    int color = 1;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class I : TetrisBlock
{
    bool[,] arrayBlock = new bool[4, 4]
    {
        {false, false, false, false},
        {true, true, true, true},
        {false, false, false, false},
        {false, false, false, false},
    };
    //Color = Cyan
    int color = 7;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class O : TetrisBlock
{
    bool[,] arrayBlock = new bool[2, 2]
    {
        {true, true},
        {true, true},
    };
    //Color = Yellow
    int color = 3;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class Z : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, false, false},
        {true, true, false},
        {false, true, true},
    };
    //Color = Red
    int color = 6;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class S : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, false, false},
        {false, true, true},
        {true, true, false},
    };
    //Color = Green
    int color = 4;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class T : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, false, false},
        {false, true, false},
        {true, true, true},
    };
    //Color = Purple
    int color = 5;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class U : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, false, false},
        {true, false, true},
        {true, true, true},
    };
    //Color = Gold
    int color = 8;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class P : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {true, false, false},
        {true, true, false},
        {false, true, true},
    };
    //Color = Brown
    int color = 9;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class Q : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, true, false},
        {true, true, true},
        {false, true, false},
    };
    //Color = Pink
    int color = 10;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}

public class Bomb : TetrisBlock
{
    bool[,] arrayBlock = new bool[3, 3]
    {
        {false, false, false},
        {false, true, false},
        {false, false, false},
    };
    //Color = Bomb
    int color = 11;
    override public bool[,] Array { get { return arrayBlock; } }
    override public int Color { get { return color; } }
    override public int Size { get { return Array.GetLength(0); } }
}