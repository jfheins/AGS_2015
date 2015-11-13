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
		private static string hexchars = "0123456789ABCDEF";

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Decode_Click(object sender, RoutedEventArgs e)
		{
			var ciphertext = CiphertextTxt.Text;
			var plaintext = new StringBuilder();

			char? lastchar = null;
			byte lastcharcode = 0;

			foreach (var chr in ciphertext)
			{
				if (lastchar != null) // This must be the second nibble
				{
					if (hexchars.Contains(chr))
					{
						var charcode = byte.Parse("" + lastchar + chr, NumberStyles.HexNumber);
						charcode = unchecked((byte)(charcode + lastcharcode));
						plaintext.Append((char) charcode);
						lastcharcode = charcode;
						lastchar = null; // Reset
						continue;
					}
					lastchar = null; // Error in input
				}
				if (hexchars.Contains(chr)) // First nibble
				{
					lastchar = chr;
				}
				else // Punctuation
				{
					plaintext.Append(chr);
				}
			}

			PlaintextTxt.Text = plaintext.ToString();
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
					var charcode = unchecked((byte)(chr - lastcharcode));
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
