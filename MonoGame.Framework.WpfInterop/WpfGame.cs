using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGame.Framework.WpfInterop
{
	public class WpfGame : D3D11Host
	{
		#region Fields

		private ContentManager _content;

		#endregion

		#region Constructors

		public WpfGame()
		{
			Content = new ContentManager(Services, "Content");
			Focusable = true;
		}

		#endregion

		#region Properties

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

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			Content?.Dispose();

			UnloadContent();
			base.Dispose();
		}

		protected virtual void Draw(GameTime gameTime)
		{
		}

		protected override void Initialize()
		{
			base.Initialize();
			LoadContent();
		}

		protected virtual void LoadContent()
		{
		}

		protected sealed override void Render(GameTime time)
		{
			base.Render(time);

			// TODO: support different game modes (vsync, fixed timestep, ..)
			// for now just call update & draw as often as possible
			Update(time);
			Draw(time);
		}

		protected virtual void UnloadContent()
		{
		}

		protected virtual void Update(GameTime gameTime)
		{
		}

		#endregion
	}
}