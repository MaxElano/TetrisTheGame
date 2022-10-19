using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Menu
{
    Vector2 position;
    Vector2 textLength;
    SpriteFont font;
    string text;
    GraphicsDeviceManager graphics;
    GameWorld gameWorld;

    public Menu(GraphicsDeviceManager _graphics)
    {
        graphics = _graphics;
        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        gameWorld = new GameWorld();
    }

    public void Update(GameTime gameTime, InputHelper inputHelper)
    {
        
        if (inputHelper.MouseLeftButtonPressed())
        {
            gameWorld.Reset();
            GameWorld.SetGameState(GameWorld.GameState.GAME);
        }
    }
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        if (GameWorld.GetGameState() == GameWorld.GameState.MENU)
        {
            Text("Start game by clicking Mouse1!", spriteBatch);
        } else if (GameWorld.GetGameState() == GameWorld.GameState.GAMEOVER)
        {
            Text("GG Press space to return to menu", spriteBatch);
        }
        spriteBatch.End();
    }

    //Draw any text in the middle of the screen
    private void Text(string text, SpriteBatch spriteBatch)
    {
        textLength = font.MeasureString(text);
        position = new Vector2(TetrisGame.ScreenSize.X / 2, TetrisGame.ScreenSize.Y / 2);
        spriteBatch.DrawString(font, text, position - textLength / 2, Color.Black);
    }
}
