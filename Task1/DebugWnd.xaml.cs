using System.Windows;
using System.Windows.Controls;
using TextBox = System.Windows.Controls.TextBox;

namespace Task1;

/// <summary>
/// Interaction logic for Debug.xaml
/// </summary>
public partial class DebugWnd : Window
{
    private static DebugWnd _debugWnd;
    private TextBox _textBox;
    private string _debugInfo;
    public DebugWnd()
    {
        InitializeComponent();
        _debugWnd = this;
        _textBox = (TextBox)this.FindName("TextDebug");

    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void addMessage(string message)
    {
        _debugInfo += message + "\n";
        _textBox.Text = _debugInfo;
    }

    public static void log(string message)
    {
        _debugWnd.addMessage(message);
    }
}
