using Common;

namespace Task1;

public class ExtendedPerson : Person
{
    private string _roleModify = "";
    private string _typeModify = "";
    private string _dateModify = "";
    private string _chanchedData = "";

    public string RoleModify { get; set; }
    public string TypeModify { get; set; }
    public string DateModify { get; set; }
    public string ChangedData { get; set; }
    public ExtendedPerson(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) :
        base(id, firstName, lastName, thirdName, phoneNumber, passportSeries, passportNumber)
    { }

    override public void Clone(IPerson person)
    {
        base.Clone(person);
        if (person is ExtendedPerson)
        {
            ExtendedPerson exPerson = (ExtendedPerson)person;
            if (exPerson.RoleModify != null) _roleModify = exPerson.RoleModify;
            if (exPerson.TypeModify != null) _typeModify = exPerson.TypeModify;
            if (exPerson.DateModify != null) _dateModify = exPerson.DateModify;
            if (exPerson.ChangedData != null) _chanchedData = exPerson.ChangedData;
        }
    }
}
