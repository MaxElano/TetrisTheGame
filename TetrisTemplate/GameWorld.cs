using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
class GameWorld
{
    public static Random Random { get { return random; } }
    static Random random;
    int level = 1, previousLevel = 1;
    double timer;

    SpriteFont font;

    TetrisGrid grid;
    TetrisBlock currentBlock, nextBlock;

    InputHelper inputHelper;

    public enum GameState
    {
        MENU, GAME, GAMEOVER
    }
    private static GameState gameState = new GameState();

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public GameWorld()
    {
        random = new Random();
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        grid = new TetrisGrid();
        nextBlock = WhichBlock();
        inputHelper = new InputHelper();
        StartBlock();
    }

    //Movement
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Left))
        {
            currentBlock.PositionX -= 1;
            if(!currentBlock.AllowedHere(grid.ArrayGrid))
                currentBlock.PositionX += 1;
        }
        if (inputHelper.KeyPressed(Keys.Right))
        {
            currentBlock.PositionX += 1;
            if (!currentBlock.AllowedHere(grid.ArrayGrid))
                currentBlock.PositionX -= 1;
        }
        if (inputHelper.KeyPressed(Keys.Down))
        {
            currentBlock.PositionY += 1;
            if (!currentBlock.AllowedHere(grid.ArrayGrid))
            {
                currentBlock.PositionY -= 1;
                currentBlock.PlaceOnGrid(grid);
                grid.FullRow();
                StartBlock();
            }
        }
        if (inputHelper.KeyPressed(Keys.A))
        {
            currentBlock.Rotate(currentBlock.Array, 3);
            if (!currentBlock.AllowedHere(grid.ArrayGrid))
                currentBlock.Rotate(currentBlock.Array, 1);
        }
        if (inputHelper.KeyPressed(Keys.D))
        {
            currentBlock.Rotate(currentBlock.Array, 1);
            if (!currentBlock.AllowedHere(grid.ArrayGrid))
                currentBlock.Rotate(currentBlock.Array, 3);
        }
        if (inputHelper.KeyPressed(Keys.Space))
        {
            bool allTheWayDown = false;
            while (!allTheWayDown)
            {
                currentBlock.PositionY += 1;
                if (!currentBlock.AllowedHere(grid.ArrayGrid))
                {
                    currentBlock.PositionY -= 1;
                    currentBlock.PlaceOnGrid(grid);
                    grid.FullRow();
                    StartBlock();
                    allTheWayDown = true;
                }
            }
        }
    }

    public void Update(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && GetGameState() == GameState.MENU)
        {
            SetGameState(GameState.GAME);
            Reset();
        }

        if (inputHelper.MouseRightButtonPressed() && GetGameState() == GameState.MENU)
            TetrisGame.Score += 100;

        if (GetGameState() == GameState.GAME)
        {
            grid.Update(gameTime);
            currentBlock.Update(gameTime, grid, level);
        }
        //Create starting block
        if (!currentBlock.BlockAction)
        {
            StartBlock();
            if (!currentBlock.AllowedHere(grid.ArrayGrid))
            {
                SetGameState(GameState.GAMEOVER);
            }       
        }

        if (inputHelper.MouseLeftButtonPressed() && GetGameState() == GameState.GAMEOVER)
        {
            SetGameState(GameState.MENU);
            TetrisGame.Score = 0;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        currentBlock.Draw(spriteBatch, grid);
        nextBlock.Draw(spriteBatch, grid);
        spriteBatch.DrawString(font, "Score: " + TetrisGame.Score, new Vector2(11 * grid.CellSize, 7 * grid.CellSize), Color.Black);
        spriteBatch.DrawString(font, "Level: " + level, new Vector2(11 * grid.CellSize, 8 * grid.CellSize), Color.Black);
        LevelUp(spriteBatch, gameTime);
        spriteBatch.End();
    }

    public void Reset()
    {
        grid.Clear();
        timer = 0;
    }

    //Chooses a random (starting) block
    private TetrisBlock WhichBlock()
    {
        int amountOfObjects = 7;
        //if(...)
            amountOfObjects += 3;
            
        int number = (int)GameWorld.Random.Next(amountOfObjects);
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
            case (6):
                return new U();
            case (7):
                return new P();
            case (8):
                return new Q();
            case (9):
                return new Bomb();
            default:
                return new T();
        }
    }

    //This is the first block that starts on the screen and every new block used. It also places the block as high up as possible
    public void StartBlock()
    {
            currentBlock = nextBlock;
            nextBlock = WhichBlock();
            nextBlock.PositionX = 11;
            nextBlock.PositionY = 1;
            currentBlock.PositionX = 3;
            currentBlock.PositionY = -(int)currentBlock.ActualBlockGrid(currentBlock.Array).W;
    }

    //Method that checks if you have leveled up and displays it on the screen
    public int LevelUp(SpriteBatch spriteBatch, GameTime gameTime)
    {
        int score = TetrisGame.Score;
        level = score / 100;
        if (level == 0)
            level = 1;
        if (level == 1)
            return level;
        if (level != previousLevel)
            timer = 5;
        if (timer > 0)
        {
            spriteBatch.DrawString(font, "Level Up!", new Vector2(5 * grid.CellSize, 8 * grid.CellSize), Color.Firebrick);
            timer = timer - gameTime.ElapsedGameTime.TotalSeconds;
        }
        previousLevel = level;
        return level;
    }

    //Method that changes the current gamestate
    public static void SetGameState(GameState newGameState)
    {
        gameState = newGameState;
    }

    //Method that returns the current gamestate
    public static GameState GetGameState()
    {
        return gameState;
    }
}
