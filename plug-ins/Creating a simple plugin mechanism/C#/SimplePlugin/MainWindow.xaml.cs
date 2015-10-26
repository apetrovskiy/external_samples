namespace SimplePlugin
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using PluginContracts;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
	    readonly Dictionary<string, IPlugin> _Plugins;

		public MainWindow()
		{
			InitializeComponent();

			_Plugins = new Dictionary<string, IPlugin>();
			var plugins = GenericPluginLoader<IPlugin>.LoadPlugins("Plugins");
			foreach(var item in plugins)
			{
				_Plugins.Add(item.Name, item);

			    var b = new Button {Content = item.Name};
			    b.Click += b_Click;
				PluginGrid.Children.Add(b);
			}
		}

		private void b_Click(object sender, RoutedEventArgs e)
		{
			var b = sender as Button;
		    if (b == null) return;
		    var key = b.Content.ToString();
		    if (!_Plugins.ContainsKey(key)) return;
		    var plugin = _Plugins[key];
		    plugin.Do();
		}
	}
}
