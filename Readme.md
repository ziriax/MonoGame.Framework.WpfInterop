# MonoGame WPF Interop

This adds WPF support to MonoGame (tested with version 3.5.1.1679).

Note that you **need** MonoGame.Framework.WindowsDX (as this interop uses SharpDX). WPF only supports DirectX.

You can host as many MonoGame controls in Wpf as you want by using D3D11Host.

___
## Available on NuGet  [![NuGet Status](http://img.shields.io/nuget/v/MonoGame.Framework.WpfInterop.svg?style=flat)](https://www.nuget.org/packages/MonoGame.Framework.WpfInterop/)

https://nuget.org/packages/MonoGame.Framework.WpfInterop/

    PM> Install-Package MonoGame.Framework.WpfInterop
   
By adding the NuGet package to a project it is possible to host MonoGame inside WPF windows.

## Example

```csharp

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

```