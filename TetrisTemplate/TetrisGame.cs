using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

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
        MediaPlayer.Play(Content.Load<Song>("TetrisMusic"));
        gameWorld = new GameWorld();
        menu = new Menu(gameWorld);
        gameWorld.Reset();
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update(gameTime);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.1F;

        switch (GameWorld.GetGameState())
        {
            case GameWorld.GameState.MENU:
                gameWorld.Update(gameTime, inputHelper);
                menu.Update(gameTime);
                IsMouseVisible = true;
                if (inputHelper.MouseLeftButtonPressed() == true && menu.Check(new Vector2(400, 380), new Vector2(8, 2), inputHelper))
                    Exit();
                break;
            case GameWorld.GameState.GAME:
                gameWorld.HandleInput(gameTime, inputHelper);
                gameWorld.Update(gameTime, inputHelper);
                IsMouseVisible = false;
                break;
            case GameWorld.GameState.GAMEOVER:
                gameWorld.Update(gameTime, inputHelper);
                IsMouseVisible = true;
                break;
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        switch (GameWorld.GetGameState())
        {
            case GameWorld.GameState.MENU:
                menu.Draw(gameTime, spriteBatch);
                break;
            case GameWorld.GameState.GAME:
                GraphicsDevice.Clear(Color.White);
                gameWorld.Draw(gameTime, spriteBatch);
                break;
            case GameWorld.GameState.GAMEOVER:
                gameWorld.Draw(gameTime, spriteBatch);
                menu.Draw(gameTime, spriteBatch);
                break;

        }
    }
}

