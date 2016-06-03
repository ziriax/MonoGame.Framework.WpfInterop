# MonoGame Wpf Interop

This adds Wpf support to MonoGame.

You can host as many MonoGame controls in Wpf as you want by using D3D11Host.

Create a new class and derive it from D3D11Host.

Override the Initialize and Render methods and implement your own game routines.

## Example

public class ContentScene : D3D11Host
{
	private ContentManager _content;

	private Texture2D _texture;
	private IGraphicsDeviceService _graphicsDeviceManager;
	private SpriteBatch _spriteBatch;

	public override void Initialize()
	{
		_graphicsDeviceManager = new WpfGraphicsDeviceService(this);

		_content = new ContentManager(Services)
		{
			RootDirectory = "Content"
		};
		_texture = _content.Load<Texture2D>("textures/sample");

		_spriteBatch = new SpriteBatch(GraphicsDevice);
	}

	public override void Render(GameTime time)
	{
		GraphicsDevice.Clear(Color.Black);

		_spriteBatch.Begin();
		_spriteBatch.Draw(_texture, new Vector2(100, 100), Color.White);
		_spriteBatch.End();

		base.Render(time);
	}
}