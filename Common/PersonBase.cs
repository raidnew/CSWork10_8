namespace Common;

abstract public class PersonBase : IPerson
{
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _thirdName;
    private string _phoneNumber;
    private string _passportsSeries;
    private string _passportsNumber;

    public PersonBase(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSerias, string passportNumber)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _thirdName = thirdName;
        _phoneNumber = phoneNumber;
        _passportsSeries = passportSerias;
        _passportsNumber = passportNumber;
    }

    public int ID { 
        get => _id;
    }

    virtual public string FirstName
    {
        get => _firstName;
        set => _firstName = value;
    }

    virtual public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    virtual public string ThirdName
    {
        get => _thirdName;
        set => _thirdName = value;
    }

    virtual public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = value;
    }

    virtual public string PassportSeries
    {
        get => _passportsSeries;
        set => _passportsSeries = value;
    }

    virtual public string PassportNumber
    {
        get => _passportsNumber;
        set => _passportsNumber = value;
    }

    virtual public void Clone(IPerson person)
    {
        if (person.FirstName != null) _firstName = person.FirstName;
        if (person.LastName != null) _lastName = person.LastName;
        if (person.ThirdName != null) _thirdName = person.ThirdName;
        if (person.PhoneNumber != null) _phoneNumber = person.PhoneNumber;
        if (person.PassportSeries != null) _passportsSeries = person.PassportSeries;
        if (person.PassportNumber != null) _passportsNumber = person.PassportNumber;
    }
}