using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Lesson_5___Add_an_Intro_Screen
{
    public class Game1 : Game
    {

        Random generator = new Random();

        Texture2D tribbleGreyTexture, tribbleBrownTexture, tribbleCreamTexture, tribbleOrangeTexture, tribbleIntroTexture, exitButtonTexture;

        Rectangle tribbleGreyRect, tribbleBrownRect, tribbleCreamRect, tribbleOrangeRect, exitButtonRect;

        Vector2 tribbleGreySpeed, tribbleBrownSpeed, tribbleCreamSpeed, tribbleOrangeSpeed;

        Color backColor, scoreColor;

        int randomX, randomY, randomBrownXSpeed, randomBrownYSpeed, bounceCount;
        int randomGreyX, randomGreyY, randomBrownStartXSpeed, randomBrownStartYSpeed;

        SpriteFont introFont, titleFont, scoreFont, warningFont, exitFont;

        Song introSong, gameSong, endSong;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        enum Screen
        {
            Intro,
            TribbleYard,
            End
        }

        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            this.Window.Title = "Animation";

            randomX = generator.Next(0, 700);
            randomY = generator.Next(0, 500);

            randomBrownStartXSpeed = generator.Next(1, 5);
            randomBrownStartYSpeed = generator.Next(1, 5);

            tribbleGreyRect = new Rectangle(randomX, randomY, 100, 100);
            tribbleBrownRect = new Rectangle(randomX, randomY, 100, 100);
            tribbleCreamRect = new Rectangle(randomX, randomY, 100, 100);
            tribbleOrangeRect = new Rectangle(randomX, randomY, 100, 100);

            exitButtonRect = new Rectangle(750, 25, 22, 18);

            tribbleGreySpeed = new Vector2(2, 2);
            tribbleBrownSpeed = new Vector2(randomBrownStartXSpeed, randomBrownStartYSpeed);
            tribbleCreamSpeed = new Vector2(5, 0);
            tribbleOrangeSpeed = new Vector2(0, 3);

            screen = Screen.Intro;

            backColor = Color.CornflowerBlue;
            scoreColor = Color.Black;

            base.Initialize();
            MediaPlayer.Play(introSong);


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");

            exitButtonTexture = Content.Load<Texture2D>("X");

            introFont = Content.Load<SpriteFont>("intro_instruction");
            titleFont = Content.Load<SpriteFont>("titleFont");
            scoreFont = Content.Load<SpriteFont>("score_counter");
            warningFont = Content.Load<SpriteFont>("warning");
            exitFont = Content.Load<SpriteFont>("exit");

            introSong = Content.Load<Song>("intro_music");
            gameSong = Content.Load<Song>("game_music");
            endSong = Content.Load<Song>("end_music");
        }

        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
    // INTRO SCREEN
            if (screen == Screen.Intro)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    MediaPlayer.Stop();
                    screen = Screen.TribbleYard;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }

    // TRIBBLE YARD SCREEN
            else if (screen == Screen.TribbleYard)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(gameSong); 


                // TRIBBLE MOVEMENT
                tribbleGreyRect.X += (int)tribbleGreySpeed.X;
                tribbleGreyRect.Y += (int)tribbleGreySpeed.Y;

                tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
                tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;

                tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
                tribbleCreamRect.Y += (int)tribbleCreamSpeed.Y;

                tribbleOrangeRect.X -= (int)tribbleOrangeSpeed.X;
                tribbleOrangeRect.Y -= (int)tribbleOrangeSpeed.Y;

                randomBrownXSpeed = generator.Next(1, 11);
                randomBrownYSpeed = generator.Next(1, 11);

                randomGreyX = generator.Next(1, 700);
                randomGreyY = generator.Next(1, 500);

                // EDGE DETECTION
                //grey
                if (tribbleGreyRect.Right > _graphics.PreferredBackBufferWidth || tribbleGreyRect.Left < 0)
                {
                    tribbleGreyRect.X = randomGreyX;
                    tribbleGreyRect.Y = randomGreyY;
                    tribbleGreySpeed.X = generator.Next(1, 6);
                    tribbleGreySpeed.Y = generator.Next(1, 6);
                    tribbleGreySpeed.X *= -1;
                    backColor = Color.Gray;
                }
                if (tribbleGreyRect.Bottom > _graphics.PreferredBackBufferHeight || tribbleGreyRect.Top < 0)
                {
                    tribbleGreyRect.X = randomGreyX;
                    tribbleGreyRect.Y = randomGreyY;
                    tribbleGreySpeed.X = generator.Next(1, 6);
                    tribbleGreySpeed.Y = generator.Next(1, 6);
                    tribbleGreySpeed.Y *= -1;
                    backColor = Color.Gray;
                }
                //brown
                if (tribbleBrownRect.Left > _graphics.PreferredBackBufferWidth || tribbleBrownRect.Right < 0)
                {
                    if (tribbleBrownRect.Left > _graphics.PreferredBackBufferWidth)
                        tribbleBrownRect.X = -100;
                    if (tribbleBrownRect.Right < 0)
                        tribbleBrownRect.X = 800;
                    tribbleBrownSpeed.X = randomBrownXSpeed;
                    backColor = Color.SaddleBrown;
                }
                if (tribbleBrownRect.Top > _graphics.PreferredBackBufferHeight || tribbleBrownRect.Bottom < 0)
                {
                    if (tribbleBrownRect.Top > _graphics.PreferredBackBufferHeight)
                        tribbleBrownRect.Y = -100;
                    if (tribbleBrownRect.Bottom < 0)
                        tribbleBrownRect.Y = 600;
                    tribbleBrownSpeed.Y = randomBrownYSpeed;
                    backColor = Color.SaddleBrown;
                }
                //cream
                if (tribbleCreamRect.Right > _graphics.PreferredBackBufferWidth || tribbleCreamRect.Left < 0)
                {
                    tribbleCreamSpeed.X *= -1;
                    backColor = Color.Beige;
                    bounceCount += 1;
                }
                if (tribbleCreamRect.Bottom > _graphics.PreferredBackBufferHeight || tribbleCreamRect.Top < 0)
                {
                    tribbleCreamSpeed.Y *= -1;
                    backColor = Color.Beige;
                    bounceCount += 1;
                }
                //orange
                if (tribbleOrangeRect.Right > _graphics.PreferredBackBufferWidth || tribbleOrangeRect.Left < 0)
                {
                    tribbleGreySpeed.X *= -1;
                    backColor = Color.Gray;
                    bounceCount += 1;
                }
                if (tribbleOrangeRect.Bottom > _graphics.PreferredBackBufferHeight || tribbleOrangeRect.Top < 0)
                {
                    tribbleOrangeSpeed.Y *= -1;
                    backColor = Color.Orange;
                    bounceCount += 1;
                }


                if (backColor == Color.SaddleBrown || backColor == Color.Orange)
                    scoreColor = Color.White;
                else 
                    scoreColor = Color.Black;

                if (bounceCount == 10)
                {
                    screen = Screen.End;
                    MediaPlayer.Stop();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }
    // END SCREEN
            else if (screen == Screen.End)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(endSong);

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Exit();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backColor);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 600), Color.White);
                _spriteBatch.DrawString(introFont, "Press SPACE to continue", new Vector2(190, 530), Color.White);
                _spriteBatch.DrawString(titleFont, "Tribble Time", new Vector2(50, 100), Color.White);
                _spriteBatch.DrawString(warningFont, "*Flash Warning*", new Vector2(50, 215), Color.White);
                _spriteBatch.Draw(exitButtonTexture, exitButtonRect, Color.Black);
                _spriteBatch.DrawString(exitFont, "esc", new Vector2(705, 18), Color.Black);
            }

            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, Color.White);
                _spriteBatch.Draw(tribbleBrownTexture, tribbleBrownRect, Color.White);
                _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.White);
                _spriteBatch.Draw(tribbleOrangeTexture, tribbleOrangeRect, Color.White);
                _spriteBatch.DrawString(scoreFont, $"bounces: {bounceCount}", new Vector2(50, 20), scoreColor);
                _spriteBatch.Draw(exitButtonTexture, exitButtonRect, Color.Black);
                _spriteBatch.DrawString(exitFont, "esc", new Vector2(710, 19), Color.Black);
            }

            else if (screen == Screen.End)
            {
                backColor = Color.DarkGray;
                _spriteBatch.DrawString(titleFont, "Game Over", new Vector2(50, 100), Color.White);
                _spriteBatch.DrawString(introFont, "Press SPACE to exit", new Vector2(220, 530), Color.White);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}