using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata.Ecma335;

public class TetrisGrid
{
    Texture2D emptyCell, bombCell, explosionCell;
    Vector2 positionCell;
    SoundEffect clearSound;
    
    enum FigureColors { empty, blue, orange, yellow, green, purple, red, cyan, gold, brown, pink, bomb, explosion }

    public bool bombBool { get; set; }
    double bombTimer = 0.3;

    public int Width { get { return width; } }
    const int width = 10;
    public int Height { get { return height; } }
    const int height = 20;
    public int CellSize { get { return cellSize; } }
    public int cellSize;
    public Texture2D EmptyCell { get { return emptyCell; } }
    public Texture2D BombCell { get { return bombCell; } }


    int[,] arrayGrid = new int[height, width] 
    { 
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    public int[,] ArrayGrid { get { return arrayGrid; } }

    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        bombCell = TetrisGame.ContentManager.Load<Texture2D>("TNT");
        explosionCell = TetrisGame.ContentManager.Load<Texture2D>("explosion");
        clearSound = TetrisGame.ContentManager.Load<SoundEffect>("TetrisClear");
        positionCell = Vector2.Zero;
        Clear();
        cellSize = emptyCell.Width;
    }

    public void Update(GameTime gameTime)
    {
        if(bombBool)
        {
            bombTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (bombTimer <= 0)
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        if (arrayGrid[j, i] == 12)
                            arrayGrid[j, i] = 0;
                    }
                }
                bombBool = false;
                bombTimer = 0.3;
            }
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Draw grid for every position
        for(int i = 0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++)
            {
                positionCell = new Vector2(i*emptyCell.Width, j*emptyCell.Height);

                if (arrayGrid[j, i] == (int)FigureColors.explosion)
                    spriteBatch.Draw(explosionCell, positionCell, WhichColor(arrayGrid[j, i]));
                else
                    spriteBatch.Draw(emptyCell, positionCell, WhichColor(arrayGrid[j, i]));
            }
        }
  
    }

    // Clears the grid.
    public void Clear()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                arrayGrid[j, i] = 0;
            }
        }
    }

    //Changes the ints to colors
    public Color WhichColor(int j)
    {
        FigureColors color = (FigureColors)j;
        switch (color)
        {
            case (FigureColors.cyan):
                return Color.Cyan;
            case (FigureColors.blue):
                return Color.Blue;
            case (FigureColors.orange):
                return Color.Orange;
            case (FigureColors.yellow):
                return Color.Yellow;
            case (FigureColors.green):
                return Color.Green;
            case (FigureColors.purple):
                return Color.Purple;
            case (FigureColors.red):
                return Color.Red;
            case (FigureColors.gold):
                return Color.DarkGoldenrod;
            case (FigureColors.brown):
                return Color.Brown;
            case (FigureColors.pink):
                return Color.DeepPink;
            case (FigureColors.bomb):
                return Color.White;
            case (FigureColors.explosion):
                return Color.White;
            default:
                return Color.White;
        }
    }

    public void FullRow()
    {
        int fullRows = 0;
        bool rowFull;

        //Check every row in the grid
        for (int j = 0; j < Height; j++)
        {
            rowFull = true;
            //Check if the row is not full
            for (int i = 0; i < Width; i++)
            {
                if (arrayGrid[j, i] == 0)
                        rowFull = false;
            }
            //If the row is full then move every row above that one down
            if (rowFull)
            {
                fullRows++;
                for (int b = j; b > 0; b--)
                {
                    for (int i = 0; i < Width; i++)
                    {
                        arrayGrid[b, i] = arrayGrid[b - 1, i];
                    }
                }
                j--;
                clearSound.Play();
            }
                
        }
        TetrisGame.Score += fullRows * fullRows * 10;
    }

    //Grays out all of the currently existing blocks
    public void GrayGrid(Color color, SpriteBatch spriteBatch)
    {
        Vector2 pos;
        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height; j++)
            {
                
                if (arrayGrid[j,i] != 0)
                {
                    pos = new Vector2(i*cellSize, j*cellSize);
                    spriteBatch.Draw(emptyCell, pos, Color.Gray);
                }
            } 
    }
}

