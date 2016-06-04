using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGame.Framework.WpfInterop
{
	public class WpfGame : D3D11Host
	{
		private ContentManager _content;

		public ContentManager Content
		{
			get { return _content; }
			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_content = value;
			}
		}

		protected virtual void LoadContent() { }

		protected virtual void UnloadContent() { }

		public WpfGame()
		{
			Content = new ContentManager(Services);
		}

		protected override void Initialize()
		{
			base.Initialize();
			LoadContent();
		}

		protected override void Dispose(bool disposing)
		{
			Content?.Dispose();

			UnloadContent();
			base.Dispose();
		}

		protected sealed override void Render(GameTime time)
		{
			base.Render(time);

			// TODO: support different game modes (vsync, fixed timestep, ..)
			// for now just call update & draw as often as possible
			Update(time);
			Draw(time);
		}

		protected virtual void Update(GameTime gameTime) { }

		protected virtual void Draw(GameTime gameTime) { }
	}
}