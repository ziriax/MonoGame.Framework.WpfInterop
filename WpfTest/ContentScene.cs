using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;

namespace WpfTest
{
	public class ContentScene : D3D11Host
	{
		#region Fields

		private int posX = 100, posY = 100;
		private ContentManager _content;
		private bool _focused;
		private IGraphicsDeviceService _graphicsDeviceManager;
		private WpfKeyboard _keyboard;
		private KeyboardState _keyboardState;
		private WpfMouse _mouse;
		private bool _mouseDown;
		private MouseState _mouseState;
		private float _rotation;
		private SpriteBatch _spriteBatch;
		private Texture2D _texture;

		#endregion

		#region Methods

		public override void Initialize()
		{
			_graphicsDeviceManager = new WpfGraphicsDeviceService(this);

			_content = new ContentManager(Services)
			{
				RootDirectory = "Content"
			};
			_texture = _content.Load<Texture2D>("hello");

			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_keyboard = new WpfKeyboard(this);
			_mouse = new WpfMouse(this);
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
			_mouseState = _mouse.GetState();
			if (!_focused && IsMouseDirectlyOver && _mouseState.LeftButton == ButtonState.Pressed)
			{
				Focus();
				_focused = true;
			}
			else
			{
				_focused = false;
			}
			_keyboardState = _keyboard.GetState();

			if (_keyboardState.IsKeyDown(Keys.Right))
			{
				_rotation += 0.05f;
			}
			if (_keyboardState.IsKeyDown(Keys.Left))
			{
				_rotation -= 0.05f;
			}
			_mouseDown = _mouseState.LeftButton == ButtonState.Pressed;
		}

		#endregion
	}
}