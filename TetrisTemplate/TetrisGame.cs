using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class TetrisGame : Game
{
    SpriteBatch spriteBatch;
    InputHelper inputHelper;
    GameWorld gameWorld;
    Menu menu;
    GraphicsDeviceManager graphics;
    


    public static int Score { get; set; }
    public static ContentManager ContentManager { get; private set; }
    
    public static Point ScreenSize { get; private set; }

    [STAThread]
    static void Main(string[] args)
    {
        TetrisGame game = new TetrisGame();
        game.Run();
    }

    public enum GameState
    {
        preGame, game, postGame
    }

    public TetrisGame()
    {        
        GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

        ContentManager = Content;
        
        Content.RootDirectory = "Content";

        ScreenSize = new Point(800, 600);
        graphics.PreferredBackBufferWidth = ScreenSize.X;
        graphics.PreferredBackBufferHeight = ScreenSize.Y;

        inputHelper = new InputHelper();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        gameWorld = new GameWorld();
        menu = new Menu(graphics);
        gameWorld.Reset();
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update(gameTime);
        
        if (GameWorld.GetGameState() == GameWorld.GameState.MENU)
        {
            menu.Update(gameTime, inputHelper);
            IsMouseVisible = true;
        }
        else
        {
            IsMouseVisible = false;
        }

        if (GameWorld.GetGameState() == GameWorld.GameState.GAME)
        {
            gameWorld.HandleInput(gameTime, inputHelper);
            gameWorld.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        
        switch (GameWorld.GetGameState())
        {
            case GameWorld.GameState.MENU:
                menu.Draw(gameTime, spriteBatch);
                gameWorld.Reset();
                break;
            case GameWorld.GameState.GAME:
                gameWorld.Draw(gameTime, spriteBatch);
                break;
            case GameWorld.GameState.GAMEOVER:
                break;

        }
    }
}

