using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class Menu
{
    Vector2 textLength;
    SpriteFont font;
    GameWorld gameWorld;
    Texture2D emptyCell;
    Texture2D tetrisLogo;
    Color chaosModeColor;

    public Menu(GameWorld _gameWorld)
    {
        font = TetrisGame.ContentManager.Load<SpriteFont>("MenuFont");
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        tetrisLogo = TetrisGame.ContentManager.Load<Texture2D>("TetrisLogo");
        gameWorld = _gameWorld;
    }

    public void Update(GameTime gameTime)
    {
        switch(gameWorld.ChaosMode)
        {
            case false:
                chaosModeColor = Color.Red;
                break;
            case true:
                chaosModeColor = Color.Green;
                break;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        if (GameWorld.GetGameState() == GameWorld.GameState.MENU)
        {
            spriteBatch.Draw(tetrisLogo, new Vector2(TetrisGame.ScreenSize.X / 2 - tetrisLogo.Width / 2, TetrisGame.ScreenSize.Y / 4 - tetrisLogo.Height / 2), Color.White);
            Square(new Vector2(400,300), new Vector2(8,2), Color.White, spriteBatch);
            Square(new Vector2(400,380), new Vector2(8,2), Color.White, spriteBatch);
            Square(new Vector2(400, 460), new Vector2(8, 2), Color.White, spriteBatch);
            Square(new Vector2(50, 550), new Vector2(2, 2), chaosModeColor, spriteBatch);
              
            Text("Play", spriteBatch, new Vector2(400, 300));
            Text("Exit", spriteBatch, new Vector2(400, 380));
            Text("Increase Starting Score (" + (TetrisGame.Score/100) + ")", spriteBatch, new Vector2(400, 460));
            Text("Chaos Mode", spriteBatch, new Vector2(140, 550));
        } else if (GameWorld.GetGameState() == GameWorld.GameState.GAMEOVER)
        {
            Square(new Vector2(400, 300), new Vector2(12, 3), Color.White, spriteBatch);
            Square(new Vector2(400, 380), new Vector2(12, 2), Color.White, spriteBatch);
            Text("GAME OVER", spriteBatch, new Vector2(400, 290));
            Text("You Reached Level " + (gameWorld.Level-1) + "!", spriteBatch, new Vector2(400, 310));
            Text("Return To Menu", spriteBatch, new Vector2(400, 380));
        }
        spriteBatch.End();
    }

    //Draw any text at any given position
    private void Text(string text, SpriteBatch spriteBatch, Vector2 position)
    {
        textLength = font.MeasureString(text);
        spriteBatch.DrawString(font, text, position - textLength / 2, Color.White);
    }

    //Draw any size square at any given position
    private void Square(Vector2 position, Vector2 scale, Color color, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(emptyCell, position - new Vector2(emptyCell.Width/2, emptyCell.Height/2) * scale, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
    }

    //Checks whether or not a certain given area is pressed or not
    public bool Check(Vector2 position, Vector2 scale, InputHelper inputHelper)
    {       
        if (inputHelper.MousePosition.X >= position.X - (emptyCell.Width / 2 * scale.X))
            if (inputHelper.MousePosition.X <= position.X + (emptyCell.Width / 2 * scale.X))
                if (inputHelper.MousePosition.Y >= position.Y - (emptyCell.Height / 2 * scale.Y))
                    if (inputHelper.MousePosition.Y <= position.Y + (emptyCell.Height / 2 * scale.Y))
                        return true;
        return false;
    }
}
