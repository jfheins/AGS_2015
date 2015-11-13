using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prolog
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Decode_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Encode_Click(object sender, RoutedEventArgs e)
		{
			var plaintext = PlaintextTxt.Text;
			var hex = new StringBuilder();

			byte lastcharcode = 0;

			foreach (var chr in plaintext)
			{
				if (char.IsLetterOrDigit(chr))
				{
					byte charcode = unchecked((byte)(chr - lastcharcode));
					hex.AppendFormat("{0:X2}", charcode);
					lastcharcode = (byte)chr;
				}
				else
				{
					hex.Append(chr);
					lastcharcode = 0;
				}
			}

			CiphertextTxt.Text = hex.ToString();
		}
	}
}
