namespace Task1;

public class Person : Common.PersonBase
{
    public Person(int id, string firstName, string lastName, string thirdName, string phoneNumber, string passportSeries, string passportNumber) :
        base(id, firstName, lastName, thirdName, phoneNumber, passportSeries, passportNumber)
    { }
}