using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.WpfInterop;

namespace WpfTest
{
	public class ContentScene : D3D11Host
	{
		private ContentManager _content;

		private Texture2D _texture;
		private IGraphicsDeviceService _graphicsDeviceManager;
		private SpriteBatch _spriteBatch;

		private KeyboardState _keyboard;
		private MouseState _mouse;
		private int posX = 100, posY = 100;
		private float _rotation;
		private bool _mouseDown;

		public override void Initialize()
		{
			_graphicsDeviceManager = new WpfGraphicsDeviceService(this);

			_content = new ContentManager(Services)
			{
				RootDirectory = "Content"
			};
			_texture = _content.Load<Texture2D>("hello");

			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_keyboard = Keyboard.GetState();
		}

		public override void Render(GameTime time)
		{
			Update(time);

			GraphicsDevice.Clear(_mouseDown ? Color.CornflowerBlue : Color.Black);

			// since we share the GraphicsDevice with all hosts, we need to save and reset the states
			// this has to be done because spriteBatch internally sets states and doesn't reset themselves, fucking over any 3D rendering (which happens in the DemoScene)

			var blend = GraphicsDevice.BlendState;
			var depth = GraphicsDevice.DepthStencilState;
			var raster = GraphicsDevice.RasterizerState;
			var sampler = GraphicsDevice.SamplerStates[0];

			_spriteBatch.Begin();
			_spriteBatch.Draw(_texture, new Rectangle(posX, posY, 100, 20), null, Color.White, _rotation, new Vector2(_texture.Width, _texture.Height) / 2f, SpriteEffects.None, 0);
			_spriteBatch.End();

			GraphicsDevice.BlendState = blend;
			GraphicsDevice.DepthStencilState = depth;
			GraphicsDevice.RasterizerState = raster;
			GraphicsDevice.SamplerStates[0] = sampler;

			base.Render(time);
		}

		private void Update(GameTime time)
		{
			_keyboard = Keyboard.GetState();
			_mouse = Mouse.GetState();

			if (_keyboard.IsKeyDown(Keys.Right))
			{
				_rotation += 0.05f;
			}
			if (_keyboard.IsKeyDown(Keys.Left))
			{
				_rotation -= 0.05f;
			}
			_mouseDown = _mouse.LeftButton == ButtonState.Pressed;
		}
	}
}