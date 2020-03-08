using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRotS.GamesStates;
using TRotS.GamesStates.States;

namespace TRotS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteSheetExtract UiSpriteSheet;

        MouseState currentMouseState;
        MouseState previousMouseState;

        Texture2D mouseTexture;

        Button start;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //this will load in kenny's sprites and automatically be able to find them and refrence them by name
            UiSpriteSheet = new SpriteSheetExtract(graphics, 
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.png",
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.xml");
            base.Initialize();
            mouseTexture = this.Content.Load<Texture2D>("cursorGauntlet_blue");
            start = new Button(this.Content, UiSpriteSheet, "buttonLong_beige.png", "START", 200, 100, new Vector2(200, 200), "menuFont");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            StateManager.Instance.SetContent(this.Content);
            StateManager.Instance.AddScreen(new StartMenu(GraphicsDevice));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;


            if (start.IsPressed(currentMouseState))
            {
                StateManager.Instance.Draw(spriteBatch);
            }

            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                mouseTexture = this.Content.Load<Texture2D>("cursorHand_blue");
            } else
            {
                //the sprite isnt really ment for this but looks fine
                mouseTexture = this.Content.Load<Texture2D>("cursorGauntlet_blue");
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            start.Draw(spriteBatch);
            spriteBatch.Draw(mouseTexture, new Vector2(this.currentMouseState.X, this.currentMouseState.Y), null, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
