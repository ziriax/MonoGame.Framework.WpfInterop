using System.Windows;

namespace WpfTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		#region Constructors

		public MainWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void Launch_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new SecondWindow();
			window.Show();
		}

		#endregion
	}
}