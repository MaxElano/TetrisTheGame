using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TetrisBlock
{
    int color;
    bool[,] arrayBlock = new bool[4, 4];
    Point position;
    double previousTime;
    static double deltaTime = 2000, deltaDifference = 0.5;
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
                PlaceOnGrid(grid.ArrayGrid);
                grid.FullRow();
                blockAction = false;
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
                if (Array[j, i])
                    spriteBatch.Draw(grid.EmptyCell, new Vector2((position.X + i) * grid.CellSize, (position.Y + j) * grid.CellSize), grid.WhichColor(this.Color));
            }
        }
    }

    //Places the block that is moving on to the fixed grid
    public int[,] PlaceOnGrid(int[,] grid)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Array[j, i])
                    grid[j + position.Y, i + position.X] = Color;
            }
        }
        return grid;
    }

    //Checks whether the block is allowed on that location
    public bool AllowedHere(int[,] grid)
    {
        Vector3 actualBlock = ActualBlockGrid(Array);

        if (position.X + actualBlock.X < 0 || position.X + actualBlock.Y > grid.GetLength(1) - 1 || position.Y + actualBlock.Z > grid.GetLength(0) - 1 || position.Y < 0)
            return false;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Array[j, i])
                    if (grid[position.Y + j, position.X + i] != 0)
                        return false;
            }
        }
        return true;
    }

    //Gets the "actual" size of the moving block, usefull for checking the grid borders with the "actual" borders of the block
    private Vector3 ActualBlockGrid(bool[,] array)
    {
        int leftSide = 0;
        int rightSide = 0;
        int botSide = 0;
        bool checkLeft = true;
        bool checkRight = true;
        bool checkBot = true;

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

        return new Vector3(leftSide, rightSide, botSide);
    }

    virtual public bool[,] Array { get { return arrayBlock; } }
    virtual public int Color { get { return color; } }
    virtual public int Size { get { return Array.GetLength(0); } }
    public int PositionX { get { return position.X; } set { position.X = value; } }
    public int PositionY { get { return position.Y; } set { position.Y = value; } }
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
    override public int Size { get { return Array.GetLength(0); } }
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
    override public int Size { get { return Array.GetLength(0); } }
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
    override public int Size { get { return Array.GetLength(0); } }
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
    override public int Size { get { return Array.GetLength(0); } }
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
    override public int Size { get { return Array.GetLength(0); } }
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
    override public int Size { get { return Array.GetLength(0); } }
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
    override public int Size { get { return Array.GetLength(0); } }
}