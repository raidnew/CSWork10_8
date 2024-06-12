using Common;
using System.Windows;
using ListView = System.Windows.Controls.ListView;

namespace Task1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class PersonListWnd : Window
{
    public Action? OnClickAddPerson;
    public Action<IPerson>? OnClickEditPerson;
    public List<IPerson>? _personList;

    private IPersonStorage _personStorage;
    private ListView _listView;
    private Role _currentRole;

    public PersonListWnd(Role role, IPersonStorage storage)
    {
        _personStorage = storage;
        _currentRole = role;
        InitializeComponent();
        InitListView();
        InitStorageEvents();
        CompletePersonList();
    }
    private void InitListView()
    {
        _listView = (ListView)this.FindName("PersonList");
    }

    private void InitStorageEvents()
    {
        _personStorage.OnPersonsLoad += CompletePersonList;
        _personStorage.OnPersonsAdded += AddPersonToList;
        _personStorage.OnPersonsSaved += SetPersonInList;
    }

    private void AddPersonToList(IPerson person)
    {
        List<IPerson> list = (List<IPerson>)_listView.ItemsSource;
        if (list == null) list = new List<IPerson>();
        list.Add(person);
        SetPersonsList(list);
    }

    private void SetPersonInList(IPerson editedPerson)
    {
        List<IPerson> list = (List<IPerson>)_listView.ItemsSource;
        int index = list.FindIndex(person => person.ID == editedPerson.ID);
        if (index >= 0)
        {
            ((PersonBase)list[index]).Clone(editedPerson);
            SetPersonsList(list);
        }
    }

    private void CompletePersonList()
    {
        int personCount = _personStorage.GetCountPersons();
        for (int i = 0; i<personCount; i++)
        {
            AddPersonToList(_personStorage.GetPersonByIndex(i));
        }
    }
    
    private void SetPersonsList(List<IPerson> list)
    {
        _personList = list;
        _listView.ItemsSource = null;
        _listView.ItemsSource = _personList;
    }
    
    private void AddPerson_Click(object sender, RoutedEventArgs e)
    {
        if (!RoleAccess.CanCreate(_currentRole)) return;
        if (_personStorage == null) return;
        OnClickAddPerson?.Invoke();
    }

    private void EditPerson_Click(object sender, RoutedEventArgs e)
    {
        if (_personStorage == null) return;
        if ((IPerson)_listView.SelectedItem != null)
            OnClickEditPerson?.Invoke((IPerson)_listView.SelectedItem);
    }
}