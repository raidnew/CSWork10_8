using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task1;

class ExtendedJsonPersonStorage : JsonPersonStorage<ExtendedPerson>
{
    public ExtendedJsonPersonStorage(Role role) : base(role)
    { 
        
    }

    override protected void ModifyPerson(IPerson person)
    {
        ExtendedPerson foundPerson = GetPersonByID(person.ID) as ExtendedPerson;
        if (foundPerson == null) return;
        foundPerson.DateModify = DateTime.Now.ToFileTime().ToString();
        foundPerson.RoleModify = _role.ToString();
        foundPerson.ChangedData = "fn,ln,tn,ph,pm,ps";
        foundPerson.TypeModify = "Update";
        foundPerson.Clone(person);
        OnPersonsSaved?.Invoke(person);
    }

    override protected void CreateNewPerson(IPerson personData)
    {
        ExtendedPerson newPerson = new ExtendedPerson(_lastPersonId++,
            personData.FirstName,
            personData.LastName,
            personData.ThirdName,
            personData.PhoneNumber,
            personData.PassportSeries,
            personData.PassportNumber);

        newPerson.DateModify = DateTime.Now.ToFileTime().ToString();
        newPerson.RoleModify = _role.ToString();
        newPerson.ChangedData = "fn,ln,tn,ph,pm,ps";
        newPerson.TypeModify = "Add";

        _personsList.Add(newPerson);
        OnPersonsAdded?.Invoke(newPerson);
    }

    override protected IPerson CreatePerson(IPerson person)
    {
        IPerson retPerson = null;
        switch (_role)
        {
            case Role.Manager:
                retPerson = new ExtendedPerson(person.ID, person.FirstName, person.LastName, person.ThirdName, person.PhoneNumber, person.PassportSeries, person.PassportNumber);
                break;
            case Role.Consultant:
            default:
                retPerson = new ExtendedPersonConsultant(person.ID, person.FirstName, person.LastName, person.ThirdName, person.PhoneNumber, person.PassportSeries, person.PassportNumber);
                break;
        }
        return retPerson;
    }
    /*
    override protected string Serialize(ExtendedPerson person)
    {
        return JsonSerializer.Serialize(person);
    }

    override protected ExtendedPerson Deserialize(string jsonString)
    {
        return JsonSerializer.Deserialize<ExtendedPerson>(jsonString);
    }
    */
}
