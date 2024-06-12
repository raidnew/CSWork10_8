namespace Task1;

public class PersonConsultant : Common.PersonBase
{
    public PersonConsultant(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) : 
        base(id, firstName, lastName, thirdName, phoneNumber, null, null)
    {}

    override public string FirstName
    {
        get => base.FirstName;
        set => base.FirstName = value;
    }
    override public string LastName
    {
        get => base.LastName;
        set => base.LastName = value;
    }
    override public string ThirdName
    {
        get => base.ThirdName;
        set => base.ThirdName = value;
    }

    override public string? PassportSeries
    {
        get => null;
        set => base.PassportSeries = base.PassportSeries;
    }

    override public string? PassportNumber
    {
        get => null;
        set => base.PassportNumber = base.PassportNumber;
    }
    override public string PhoneNumber
    {
        get => base.PhoneNumber;
        set
        {
            if(value != null && value.Length > 0)
                base.PhoneNumber = value;
        }
    }
}
