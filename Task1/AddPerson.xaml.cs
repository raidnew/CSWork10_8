
using Common;
using System.Reflection;
using System.Windows;
using TextBox = System.Windows.Controls.TextBox;

namespace Task1;

/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
public partial class AddPerson : Window
{
    public Action<IPerson>? OnFinishPersonEdit;
    public Action? OnCloseWindow;

    private TextBox _firstNameField;
    private TextBox _lastNameField;
    private TextBox _thirdNameField;
    private TextBox _phoneNumberField;
    private TextBox _passportSeriesField;
    private TextBox _passportNumberField;

    private int _currentPersonID = -1;

    private IPerson _person;

    public AddPerson()
    {
        InitializeComponent();
        InitFields();
    }

    public AddPerson(IPerson person) : this()
    {
        _person = person;
        _currentPersonID = person.ID;
        CompleteField(_firstNameField, person.FirstName);
        CompleteField(_lastNameField, person.LastName);
        CompleteField(_thirdNameField, person.ThirdName);
        CompleteField(_phoneNumberField, person.PhoneNumber);
        CompleteField(_passportSeriesField, person.PassportSeries);
        CompleteField(_passportNumberField, person.PassportNumber);
    }

    private void CompleteField(TextBox field, string data)
    {
        if(data == null)
        {
            field.IsReadOnly = true;
            field.IsEnabled = false;
        }
        field.Text = data;
    }
    private void InitFields() {
        _firstNameField = (TextBox)this.FindName("FirstName");
        _lastNameField = (TextBox)this.FindName("LastName");
        _thirdNameField = (TextBox)this.FindName("ThirdName");
        _phoneNumberField = (TextBox)this.FindName("PhoneNumber");
        _passportSeriesField = (TextBox)this.FindName("PassportSeries");
        _passportNumberField = (TextBox)this.FindName("PassportNumber");
    }

    private void FinishEditPerson(object sender, RoutedEventArgs e)
    {
        string firstName = GetFieldValue(_firstNameField);
        string lastName = GetFieldValue(_lastNameField);
        string thirdName = GetFieldValue(_thirdNameField);
        string phoneNumber = GetFieldValue(_phoneNumberField);
        string passportSeries = GetFieldValue(_passportSeriesField);
        string passportNumber = GetFieldValue(_passportNumberField);
        if (_person != null)
        {
            Type type = _person.GetType();

            _person.FirstName = firstName;
            _person.LastName = lastName;
            _person.ThirdName = thirdName;
            _person.PhoneNumber = phoneNumber;
            _person.PassportSeries = passportSeries;
            _person.PassportNumber = passportNumber;
        }
        else
        {
            _person = new Person(_currentPersonID, firstName, lastName, thirdName, phoneNumber, passportSeries, passportNumber);
        }
        OnFinishPersonEdit?.Invoke(_person);
        CloseWindow();
    }
    private string? GetFieldValue(TextBox field)
    {
        if (field.Text == "") return null;
        return field.Text;
    }

    private void CancelEditPerson(object sender, RoutedEventArgs e)
    {
        CloseWindow();
    }

    private void CloseWindow()
    {
        OnCloseWindow?.Invoke();
        this.Close();
    }
}
