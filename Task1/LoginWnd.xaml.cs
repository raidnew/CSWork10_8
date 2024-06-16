using Common;
using System.Windows;
using ComboBox = System.Windows.Controls.ComboBox;

namespace Task1;

public partial class LoginWnd : Window
{
    public static Action<Role> OnLogin;

    private ComboBox _roleCombo;

    public LoginWnd(Role[] allowRoles)
    {
        InitializeComponent();
        _roleCombo = (ComboBox)this.FindName("ComboRole");
        InitComboRoles(allowRoles);
    }

    private void InitComboRoles(Role[] allowRoles)
    {
        foreach (Role role in allowRoles)
            _roleCombo.Items.Add(role);
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        int indexRole = 0;
        foreach (Role role in (Role[])Enum.GetValues(typeof(Role)))
        {
            if(role.ToString() == _roleCombo.SelectedValue.ToString())
            {
                OnLogin?.Invoke((Role)_roleCombo.SelectedValue);
                Close();
                break;
            }
        }
    }
}
