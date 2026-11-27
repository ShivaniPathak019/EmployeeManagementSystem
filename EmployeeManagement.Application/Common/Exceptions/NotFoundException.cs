namespace EmployeeManagement.Application.Common.Exceptions;

//inheriting from Exception class
public class NotFoundException : Exception
{
    //constructor 
    public NotFoundException(string name, object key)
        : base($"{name} with id ({key}) was not found.")
    {
    }
}