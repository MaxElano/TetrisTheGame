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
            GameWorld.SetGameState(GameWorld.GameState.GAME);
            gameWorld.Reset();
        }
    }
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        text = "Start Game";
        MeasureText(text);
        position = new Vector2(TetrisGame.ScreenSize.X/2, TetrisGame.ScreenSize.Y/2);
        spriteBatch.Begin();
        spriteBatch.DrawString(font, text, position-textLength/2, Color.Black);
        spriteBatch.End();
    }

    private Vector2 MeasureText(string text)
    {
        textLength = font.MeasureString(text);
        return textLength;
    }
}
