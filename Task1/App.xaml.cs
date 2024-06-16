using Common;
using System.Data;
using System.Windows;

namespace Task1;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    PersonListWnd? _personListWnd;
    AddPerson? _addPersonWnd = null;
    IPersonStorage _personStorage;

    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ShowLoginWindow();
        LoginWnd.OnLogin += LoginSuccessful;
    }

    private void ShowLoginWindow()
    {
        LoginWnd wnd = new LoginWnd(new Role[] { Role.Consultant, Role.Manager });
        wnd.Show();
    }

    private void LoginSuccessful(Role role)
    {
        _personStorage = new ExtendedJsonPersonStorage(role);
        ShowPersonList(role);
    }
    protected override void OnExit(ExitEventArgs e)
    {
        _personStorage.Save();
    }

    private void ShowPersonList(Role role)
    {
        if (_personListWnd == null)
        {
            _personListWnd = new PersonListWnd(role, _personStorage);
            _personListWnd.OnClickAddPerson += ShowAddPerson;
            _personListWnd.OnClickEditPerson += ShowEditPerson;
        }
        _personListWnd.Show();
    }
    private void ShowEditPerson(IPerson person)
    {
        if (_addPersonWnd != null)
        {
            _addPersonWnd.Close();
        }
        _addPersonWnd = new AddPerson(person);
        _addPersonWnd.OnFinishPersonEdit += SavePerson;
        _addPersonWnd.OnCloseWindow += DestroyAddPersonWnd;
        _addPersonWnd.Show();
    }

    private void ShowAddPerson()
    {
        if (_addPersonWnd != null)
        {
            _addPersonWnd.Close();
        }
        _addPersonWnd = new AddPerson();
        _addPersonWnd.OnFinishPersonEdit += SavePerson;
        _addPersonWnd.OnCloseWindow += DestroyAddPersonWnd;
        _addPersonWnd.Show();
    }

    private void DestroyAddPersonWnd()
    {
        if (_addPersonWnd == null)
        {
            return;
        }
        _addPersonWnd.OnFinishPersonEdit -= SavePerson;
        _addPersonWnd.OnCloseWindow -= DestroyAddPersonWnd;
        _addPersonWnd = null;
    }

    private void SavePerson(IPerson person)
    {
        _personStorage?.SavePerson(person);
    }
}