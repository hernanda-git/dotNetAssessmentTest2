using SimpleEmployeeApp.Helpers.Model;
using SimpleEmployeeApp.Helpers.Service;
using System.Globalization;

namespace SimpleInMemoryCRUDEmployeeApp
{
    class Program
    {
        static EmployeeService employeeService = new EmployeeService();

        static void Main(string[] args)
        {

            while (true)
            {
                Output.Message("Welcome to Simple In-Memory CRUD Employee Application", Message.Type.Default);
                Output.Message("1. Create Employee", Message.Type.Default);
                Output.Message("2. Update Employee", Message.Type.Default);
                Output.Message("3. Delete Employee", Message.Type.Default);
                Output.Message("4. List Employees", Message.Type.Default);
                Output.Message("5. Exit\n\n", Message.Type.Default);
                Output.Message("Your choice: ", Message.Type.Default);

                string? strChoice;
                bool IsValidNumber;
                do
                {
                    try
                    {
                        strChoice = Console.ReadLine();
                        IsValidNumber = int.TryParse(strChoice, out int choice);
                        if (IsValidNumber)
                        {
                            switch (choice)
                            {
                                case 1:
                                    // Create Employee
                                    employeeService.CreateEmployee();
                                    break;
                                case 2:
                                    //Update Employee
                                    employeeService.UpdateEmployee();
                                    break;
                                case 3:
                                    //DeleteEmployee
                                    employeeService.DeleteEmployee();
                                    break;
                                case 4:
                                    //ListEmployees
                                    employeeService.ListEmployee();
                                    break;
                                case 5:
                                    return;
                                default:
                                    Output.Message("Invalid choice. Please try again.", Message.Type.Warning);
                                    IsValidNumber = false;
                                    break;
                            }
                        }
                        else if (string.IsNullOrEmpty(strChoice))
                        {
                            Output.Message("Please enter your choice.", Message.Type.Warning);
                        }
                        else
                        {
                            Output.Message("Invalid number. Please try again.", Message.Type.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        Output.Message($"Error: {ex.Message}", Message.Type.Error);
                        IsValidNumber = false;
                    }
                } while (!IsValidNumber);
            }
        }
    }
}