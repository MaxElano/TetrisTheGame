using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
class GameWorld
{
    public static Random Random { get { return random; } }
    static Random random;

    SpriteFont font;

    bool blockAction = false;

    TetrisGrid grid;
    TetrisBlock block;
    Point blockPosition;

    GameState gameState = new GameState();

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.game;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();

    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Left))
        {
            blockPosition.X -= 1;
            //if(!block.AllowedHere(grid.ArrayGrid, blockPosition))
            //    blockPosition.X += 1;
        }
        if (inputHelper.KeyPressed(Keys.Right))
        {
            blockPosition.X += 1;
            //if (!block.AllowedHere(grid.ArrayGrid, blockPosition))
            //    blockPosition.X -= 1;
        }
        if (inputHelper.KeyPressed(Keys.Down))
        {
            blockPosition.Y += 1;
            //if (!block.AllowedHere(grid.ArrayGrid, blockPosition))
            //    blockPosition.Y -= 1;
        }
        if (inputHelper.KeyPressed(Keys.A))
        {
            block.Rotate(3);
            //check if v
        }
        if (inputHelper.KeyPressed(Keys.D))
        {
            block.Rotate(1);
        }
    }

    public void Update(GameTime gameTime)
    {
        grid.Update(gameTime);
        
            //Create starting block
        if (!blockAction)
        {
            StartBlock();
            blockAction = true;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        
        grid.Draw(gameTime, spriteBatch);
        BlockDraw(spriteBatch, blockPosition);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

    //Chooses a random (starting) block
    private TetrisBlock WhichBlock()
    {
        int number = (int)GameWorld.Random.Next(7);
        switch (number)
        {
            case (0):
                return new L();
            case (1):
                return new J();
            case (2):
                return new I();
            case (3):
                return new O();
            case (4):
                return new Z();
            case (5):
                return new S();
            default:
                return new T();
        }
    }

    private void StartBlock()
    {
        block = WhichBlock();
        blockPosition.X = 3;
        bool line = false;
        int yOffset = 0;
        for (int j = 0; line == false && j < 4 ; j++)
        {
            for (int i = 0; line == false && i < 4; i++)
            {
                if (block.Array[j, i])
                {
                    line = true;
                    yOffset = -j;
                }
            }
        }
        blockPosition.Y = yOffset;
    }

    private void BlockDraw(SpriteBatch spriteBatch, Point position)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (block.Array[j, i] == true)
                    spriteBatch.Draw(grid.EmptyCell, new Vector2(position.X * grid.CellSize + i * grid.CellSize, position.Y * grid.CellSize + j * grid.CellSize), grid.WhichColor(block.Color));
            }
        }
    }
}
