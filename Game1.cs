using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameFallingSand.scripts;
using MonogameFallingSand.scripts.elements;
using MonogameFallingSand.scripts.elements.Liquid;
using MonogameFallingSand.scripts.elements.Solid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MonogameFallingSand
{
    public class Game1 : Game
    {
        #region Boring Stuff
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Random Random = new Random();
        private Tilemap tilemap;
        private FrameCounter _frameCounter = new FrameCounter();
        private SpriteFont _font;

        private Vector2 mousePos = new Vector2();
        #endregion

        Element hoveredElement;

        private int BrushSize = 5;
        string[] elements = { "Sand", "Water", "Stone"};
        int elementIndex = 0;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            int screenSize = 640;
            _graphics.PreferredBackBufferWidth = screenSize;
            _graphics.PreferredBackBufferHeight = screenSize;
            _graphics.ApplyChanges();

            tilemap = new Tilemap(screenSize, 4); //Initialize the tilemap
            Debug.WriteLine(tilemap.tilemap.Length);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("m6x11");
            // TODO: use this.Content to load your game content here
        }

        int previousScrollValue = 0;
        protected override void Update(GameTime gameTime)
        {
            tilemap.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState currentMouseState = Mouse.GetState();
            int xPos = Math.Clamp(currentMouseState.X / tilemap.tileSize, 0, tilemap.tilemap.GetLength(0) - 1);
            int yPos = Math.Clamp(currentMouseState.Y / tilemap.tileSize, 0, tilemap.tilemap.GetLength(1) - 1);
            mousePos.X = xPos; mousePos.Y = yPos;
            hoveredElement = tilemap.GetElementAtIndex(xPos, yPos);
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = (int)-BrushSize / 2; i < (int)BrushSize / 2; i++)
                {
                    for (int j = (int)-BrushSize / 2; j < (int)BrushSize / 2; j++)
                    {
                        int finalXPos = Math.Clamp(xPos + i, 0, tilemap.tilemap.GetLength(0) - 1);
                        int finalYPos = Math.Clamp(yPos + j, 0, tilemap.tilemap.GetLength(1) - 1);
                        if(tilemap.GetElementAtIndex(finalXPos, finalYPos) == null)
                        {
                            switch(elements[elementIndex])
                            {
                                case "Sand":
                                    tilemap.SetElementAtIndex(finalXPos, finalYPos, new Sand(_graphics.GraphicsDevice, tilemap, finalXPos, finalYPos));
                                    break;
                                case "Water":
                                    tilemap.SetElementAtIndex(finalXPos, finalYPos, new Water(_graphics.GraphicsDevice, tilemap, finalXPos, finalYPos));
                                    break;
                                case "Stone":
                                    tilemap.SetElementAtIndex(finalXPos, finalYPos, new Stone(_graphics.GraphicsDevice, tilemap, finalXPos, finalYPos));
                                    break;
                            }
                        }
                    }
                }
            }
            if (currentMouseState.RightButton == ButtonState.Pressed)
            {
                for (int i = (int)-BrushSize / 2; i < (int)BrushSize / 2; i++)
                {
                    for (int j = (int)-BrushSize / 2; j < (int)BrushSize / 2; j++)
                    {
                        int finalXPos = Math.Clamp(xPos + i, 0, tilemap.tilemap.GetLength(0) - 1);
                        int finalYPos = Math.Clamp(yPos + j, 0, tilemap.tilemap.GetLength(1) - 1);
                        tilemap.GetElementAtIndex(finalXPos, finalYPos)?.texture.Dispose();
                        tilemap.SetElementAtIndex(finalXPos, finalYPos, null);
                    }
                }
            }

            if (currentMouseState.ScrollWheelValue < previousScrollValue)
            {
                elementIndex = (elementIndex + 1) % elements.Length;
            }
            else if (currentMouseState.ScrollWheelValue > previousScrollValue)
            {
                elementIndex = (elementIndex - 1 + elements.Length) % elements.Length;
            }

            previousScrollValue = currentMouseState.ScrollWheelValue;
            base.Update(gameTime);
        }

        bool showMoreInformation = false;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 61, 82, 255));

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            tilemap.Draw(_spriteBatch);
            string elementHovered = hoveredElement != null ? hoveredElement.name : string.Empty;
            _spriteBatch.DrawString(_font, $"Selected Element: {elements[elementIndex]}", new Vector2(0, 0), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_font, elementHovered, mousePos * tilemap.tileSize + new Vector2(-elementHovered.Length * 3, -20), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            _spriteBatch.End();

            #region FPSCOUNTER
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
            Window.Title = $"{fps}, ELEMENTS: {tilemap.ElementCount} {elementIndex}";
            #endregion
            base.Draw(gameTime);
        }
    }

    public class FrameCounter
    {
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MaximumSamples = 100;

        private Queue<float> _sampleBuffer = new();

        public void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MaximumSamples)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
        }
    }
}