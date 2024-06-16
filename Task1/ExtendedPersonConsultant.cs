using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1;

class ExtendedPersonConsultant : PersonConsultant
{
    public ExtendedPersonConsultant(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) :
    base(id, firstName, lastName, thirdName, phoneNumber, null, null)
    { }

    override public string FirstName
    {
        get => base.FirstName;
        set => base.FirstName = base.FirstName;
    }
    override public string LastName
    {
        get => base.LastName;
        set => base.LastName = base.LastName;
    }
    override public string ThirdName
    {
        get => base.ThirdName;
        set => base.ThirdName = base.ThirdName;
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
            if (value != null && value.Length > 0)
                base.PhoneNumber = value;
        }
    }
}
