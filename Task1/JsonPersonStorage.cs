using Common;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Task1;

public class JsonPersonStorage<T> : IPersonStorage
{
    public Action? OnPersonsLoad { get; set; }
    public Action<IPerson>? OnPersonsSaved { get; set; }
    public Action<IPerson>? OnPersonsAdded { get; set; }

    public bool IsReady { get; private set; }

    protected int _lastPersonId = 0;
    protected Role _role;
    protected List<IPerson> _personsList;

    private FileStorage _fileStorage;

    public JsonPersonStorage(Role role)
    {
        IsReady = false;
        _role = role;
        _personsList = new List<IPerson>();
        _fileStorage = new FileStorage("jsonstorage.json");
        _fileStorage.OnFileLoaded += OnDataLoad;
        _fileStorage.LoadFile();
    }

    public void SavePerson(IPerson person)
    {
        if (person.ID < 0)
        {
            if (CanRoleCreate())
                CreateNewPerson(person);
        }
        else
        {
            ModifyPerson(person);
        }

    }

    public int GetCountPersons()
    {
        return _personsList.Count;
    }

    public IPerson? GetPersonByID(int ID)
    {
        return _personsList.Find(person => person.ID == ID);
    }

    public IPerson GetPersonByIndex(int index)
    {
        return CreatePerson(_personsList[index]);
    }

    public void Save()
    {
        List<string> serializedPersons = new List<string>();
        foreach (IPerson person in _personsList)
        {
            serializedPersons.Add(Serialize((T)person));
        }
        _fileStorage.SaveFile(serializedPersons);
    }
    virtual protected void ModifyPerson(IPerson person)
    {
        Person foundPerson = GetPersonByID(person.ID) as Person;
        if (foundPerson == null) return;
        foundPerson.Clone(person);
        OnPersonsSaved?.Invoke(person);
    }

    virtual protected void CreateNewPerson(IPerson personData)
    {
        Person newPerson = new Person(_lastPersonId++,
            personData.FirstName,
            personData.LastName,
            personData.ThirdName,
            personData.PhoneNumber,
            personData.PassportSeries,
            personData.PassportNumber);
        _personsList.Add(newPerson);
        OnPersonsAdded?.Invoke(newPerson);
    }

    virtual protected IPerson CreatePerson(IPerson person)
    {
        IPerson retPerson = null;
        switch (_role)
        {
            case Role.Manager:
                retPerson = new Person(person.ID, person.FirstName, person.LastName, person.ThirdName, person.PhoneNumber, person.PassportSeries, person.PassportNumber);
                break;
            case Role.Consultant:
            default:
                retPerson = new PersonConsultant(person.ID, person.FirstName, person.LastName, person.ThirdName, person.PhoneNumber, person.PassportSeries, person.PassportNumber);
                break;
        }
        return retPerson;
    }

    virtual protected string Serialize(T person)
    {
        return JsonSerializer.Serialize(person);
    }

    virtual protected T? Deserialize(string jsonString)
    {
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    protected bool CanRoleCreate()
    {
        switch (_role)
        {
            case Role.Manager: return true;
            case Role.Consultant: return false;
        }
        return false;
    }


    private void InitLastID()
    {
        if (_personsList != null && _personsList.Count > 0)
            _lastPersonId = _personsList.Last().ID + 1;
    }

    private void OnDataLoad(List<string> serializedPersons)
    {
        foreach (string jsonPerson in serializedPersons)
        {
            _personsList.Add((IPerson)Deserialize(jsonPerson));
        }
        InitLastID();
        IsReady = true;
        OnPersonsLoad?.Invoke();
    }

}