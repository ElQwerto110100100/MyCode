using MarkTut1.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private object previouseKeyboardState;
        private object currentKeyboardState;
        Song backfroundMusic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SpriteSheet.Instance.AddSpriteSheet(
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.png",
                @"C:\Users\joshy\Desktop\Github\MyCode\Games\MonoGames\TRS\TRotS\TRotS\Resource\uipack_rpg_sheet.xml");
            // TODO: Add your initialization logic here
            //this will load in kenny's sprites and automatically be able to find them and refrence them by name
            RC_Framework.LineBatch.init(GraphicsDevice);
            SpriteSheet.Instance.SetGraphicsManager(graphics);
            MouseClass.Instance.SetTexture(this.Content.Load<Texture2D>("cursorGauntlet_blue"));
            MouseClass.Instance._content = this.Content;

            base.Initialize();
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
            //StateManager.Instance.AddScreen(new StartMenu(GraphicsDevice, graphics));
            StateManager.Instance.AddScreen(new IntroScreen(GraphicsDevice, graphics));

            this.backfroundMusic = Content.Load<Song>("Sounds/Music/cupheadOST");
            MediaPlayer.Play(backfroundMusic);
            //  Uncomment the following line will also loop the song
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.04f;
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
            if (TRotS.MouseClass.Instance.GetKeyState().IsKeyDown(Keys.B) && !TRotS.MouseClass.Instance.GetPrevKeyState().IsKeyDown(Keys.B))
            {
                Sprite.borderOn = !Sprite.borderOn;
            }

            if (StateManager.Instance._screens.Count == 0) Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //the mouse will be use universally accross the whole game
            MouseClass.Instance.Update();
            StateManager.Instance.Update(gameTime);
            SoundLib.Instance.Update();
            if (MouseClass.Instance.GetState().LeftButton == ButtonState.Pressed)
            {
                MouseClass.Instance.SetTexture(this.Content.Load<Texture2D>("cursorHand_blue"));
            } else
            {
                //the sprite isnt really ment for this but looks fine
                MouseClass.Instance.SetTexture(this.Content.Load<Texture2D>("cursorGauntlet_blue"));
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, 
                SamplerState.LinearClamp, DepthStencilState.Default, 
                RasterizerState.CullNone);
            StateManager.Instance.Draw(spriteBatch);
            MouseClass.Instance.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
