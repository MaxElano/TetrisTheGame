using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

public class TetrisGrid
{
    Point positionBlock;
    Texture2D emptyCell;
    Vector2 positionCell;

    TetrisBlock block;
    
    enum FigureColors { empty, blue, orange, yellow, green, purple, red, cyan }
    FigureColors figureColor = new FigureColors();

    public int Width { get { return width; } }
    const int width = 10;
    public int Height { get { return height; } }
    const int height = 20;
    public int CellSize { get { return cellSize; } }
    public int cellSize;
    public Texture2D EmptyCell { get { return emptyCell; } }

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

    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        positionCell = Vector2.Zero;
        Clear();
        cellSize = emptyCell.Width;
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Draw grid for every position
        for(int i = 0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++)
            {
                positionCell = new Vector2(i*emptyCell.Width, j*emptyCell.Height);
                spriteBatch.Draw(emptyCell, positionCell, Color.White);


                spriteBatch.Draw(emptyCell, positionCell, WhichColor(arrayGrid[j, i]));
            }
        }
  
    }

    // Clears the grid.
    public void Clear()
    {
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
            default:
                return Color.White;
        }
    }
}

