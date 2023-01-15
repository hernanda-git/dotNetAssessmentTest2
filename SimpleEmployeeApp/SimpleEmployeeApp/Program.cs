using SimpleEmployeeApp.Helpers.Model;
using SimpleEmployeeApp.Helpers.Service;
using System.Globalization;

namespace SimpleInMemoryCRUDEmployeeApp
{
    class Program
    {
        // initiate the Employee Service that contains List<Employee> which will be used to store and delegate the CRUD functions
        static EmployeeService employeeService = new EmployeeService();

        static void Main(string[] args)
        {
            // infinite loop for re-run the application when the task completed.
            while (true)
            {
                // initiate the program information
                Output.Message("Welcome to a Simple In-Memory CRUD Employee Application", Message.Type.Default);
                Output.Message("1. Create Employee", Message.Type.Default);
                Output.Message("2. Update Employee", Message.Type.Default);
                Output.Message("3. Delete Employee", Message.Type.Default);
                Output.Message("4. List Employees", Message.Type.Default);
                Output.Message("5. Exit\n\n", Message.Type.Default);
                Output.Message("Your choice: ", Message.Type.Default);

                // variable declaration for user input/number validation
                string? strChoice;
                bool IsValidNumber;

                // re-do the task if input was not a valid number
                do
                {
                    try
                    {
                        // read the user input and store it to a variable to be validated.
                        strChoice = Console.ReadLine();

                        // input validation
                        IsValidNumber = int.TryParse(strChoice, out int choice);
                        if (IsValidNumber)
                        {
                            switch (choice)
                            {
                                case 1:
                                    // create new record of employee task
                                    employeeService.CreateEmployee();
                                    break;
                                case 2:
                                    // update a single employee record task
                                    employeeService.UpdateEmployee();
                                    break;
                                case 3:
                                    // delete a single employee record task
                                    employeeService.DeleteEmployee();
                                    break;
                                case 4:
                                    // list of all employees
                                    employeeService.ListEmployee();
                                    break;
                                case 5:
                                    // exit / break the application
                                    return;
                                default:
                                    // send the output that user not type the option/choice correctly
                                    Output.Message("Invalid choice. Please try again.", Message.Type.Warning);
                                    IsValidNumber = false;
                                    break;
                            }
                        }
                        // if user send a null or empty input
                        else if (string.IsNullOrEmpty(strChoice))
                        {
                            Output.Message("Please enter your choice.", Message.Type.Warning);
                        }
                        // if user send non-integer input
                        else
                        {
                            Output.Message("Invalid number. Please try again.", Message.Type.Warning);
                        }
                    }
                    // if program detects error then throw the error exception
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