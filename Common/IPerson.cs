namespace Common;


public interface IPerson
{
    int ID { get; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string ThirdName { get; set; }
    string PhoneNumber { get; set; }
    string PassportSeries { get; set; }
    string PassportNumber { get; set; }
}