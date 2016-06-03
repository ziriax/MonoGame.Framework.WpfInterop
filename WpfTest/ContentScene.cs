using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using System;

namespace WpfTest
{
	public class ContentScene : D3D11Host
	{
		private ContentManager _content;

		private Texture2D _texture;
		private IGraphicsDeviceService _graphicsDeviceManager;

		public override void Initialize()
		{
			_graphicsDeviceManager = new WpfGraphicsDeviceService(this);

			_content = new ContentManager(Services)
			{
				RootDirectory = "Content"
			};
			_texture = _content.Load<Texture2D>("hello");

			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		private SpriteBatch _spriteBatch;
		public override void Render(TimeSpan time)
		{
			GraphicsDevice.Clear(Color.Black);

			// since we share the GraphicsDevice with all hosts, we need to save and reset the states
			// this has to be done because spriteBatch internally sets states and doesn't reset themselves, fucking over any 3D rendering (which happens in the DemoScene)

			var blend = GraphicsDevice.BlendState;
			var depth = GraphicsDevice.DepthStencilState;
			var raster = GraphicsDevice.RasterizerState;
			var sampler = GraphicsDevice.SamplerStates[0];

			_spriteBatch.Begin();
			_spriteBatch.Draw(_texture, new Vector2(100, 100), Color.White);
			_spriteBatch.End();

			GraphicsDevice.BlendState = blend;
			GraphicsDevice.DepthStencilState = depth;
			GraphicsDevice.RasterizerState = raster;
			GraphicsDevice.SamplerStates[0] = sampler;

			base.Render(time);
		}

	}
}