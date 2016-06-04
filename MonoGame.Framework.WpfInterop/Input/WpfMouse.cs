using Microsoft.Xna.Framework.Input;
using System;
using System.Windows;
using System.Windows.Input;

namespace MonoGame.Framework.WpfInterop.Input
{
	/// <summary>
	/// Helper class that converts WPF mouse input to the XNA/MonoGame <see cref="_mouseState"/>.
	/// </summary>
	public class WpfMouse
	{
		#region Fields

		private readonly IInputElement _focusElement;

		private MouseState _mouseState;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of the keyboard helper.
		/// </summary>
		/// <param name="focusElement">The element that will be used as the focus point. Only if this element is correctly focused, mouse events will be handled.</param>
		public WpfMouse(IInputElement focusElement)
		{
			if (focusElement == null)
				throw new ArgumentNullException(nameof(focusElement));

			_focusElement = focusElement;
			_focusElement.MouseWheel += HandleMouse;
			// movement
			_focusElement.MouseMove += HandleMouse;
			_focusElement.MouseEnter += HandleMouse;
			_focusElement.MouseLeave += HandleMouse;
			// clicks
			_focusElement.MouseLeftButtonDown += HandleMouse;
			_focusElement.MouseLeftButtonUp += HandleMouse;
			_focusElement.MouseRightButtonDown += HandleMouse;
			_focusElement.MouseRightButtonUp += HandleMouse;
		}

		#endregion

		#region Methods

		public MouseState GetState() => _mouseState;

		private void HandleMouse(object sender, MouseEventArgs e)
		{
			if (e.Handled || !_focusElement.IsMouseDirectlyOver)
				return;

			e.Handled = true;
			var m = _mouseState;
			var pos = e.GetPosition(_focusElement);
			var w = e as MouseWheelEventArgs;
			_mouseState = new MouseState((int)pos.X, (int)pos.Y, m.ScrollWheelValue + w?.Delta ?? 0, (ButtonState)e.LeftButton, (ButtonState)e.MiddleButton, (ButtonState)e.RightButton, (ButtonState)e.XButton1, (ButtonState)e.XButton2);
		}

		#endregion
	}
}