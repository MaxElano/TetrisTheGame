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
    GraphicsDeviceManager graphics;
    GameWorld gameWorld;

    public Menu(GraphicsDeviceManager _graphics)
    {
        graphics = _graphics;
        font = TetrisGame.ContentManager.Load<SpriteFont>("MenuFont");
        gameWorld = new GameWorld();
    }



    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        if (GameWorld.GetGameState() == GameWorld.GameState.MENU)
        {
            Text("Start game by clicking Mouse1!", spriteBatch, 130, 0);
            Text("Press Mouse2 to increase difficulty", spriteBatch, 130, 30);
        } else if (GameWorld.GetGameState() == GameWorld.GameState.GAMEOVER)
        {
            Text("GG Press Mouse1 to return to menu", spriteBatch, 130, 0);
            Text("Your reached level " + gameWorld.Level + "!", spriteBatch, 130, 30);
        }
        spriteBatch.End();
    }

    //Draw any text with any given position
    private void Text(string text, SpriteBatch spriteBatch, int offX = 0, int offY = 0)
    {
        textLength = font.MeasureString(text);
        position = new Vector2(TetrisGame.ScreenSize.X / 2 + offX, TetrisGame.ScreenSize.Y / 2 + offY);
        spriteBatch.DrawString(font, text, position - textLength / 2, Color.Black);
    }
}
