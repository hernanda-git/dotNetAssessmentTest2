using BetterConsoleTables;
using SimpleEmployeeApp.Helpers.Model;
using System.Globalization;

namespace SimpleEmployeeApp.Helpers.Service
{
    public class Output
    {
        // This function is responsible for sending output with based on the message type enum.
        // The message type will determine the color of the output.
        public static void Message(string message, Message.Type type)
        {
            switch (type)
            {
                case Model.Message.Type.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Model.Message.Type.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Model.Message.Type.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Model.Message.Type.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Model.Message.Type.Default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    // This function is the main CRUD (Create, Read, Update, and Delete) function for the Employee model.
    // It handles all operations related to managing employee records,
    // including creating new employee records, reading existing records, updating existing records, and deleting records.
    public class EmployeeService
    {
        // Initiate the employee counter for EmployeeId starts from 1000
        int employeeCounter = 1000;

        // Declare the In-Memory Employee storage
        List<Employee> Employees { get; set; }

        public EmployeeService()
        {
            // This is an instantiation of the List<Employee> class, which will be used to store and perform CRUD tasks on employee records.
            // All operations related to managing employee records such as adding, updating and deleting will be performed using this list instance.
            // Without instantiating the class, it would not be possible to create Employee object to that class and use it.
            Employees = new List<Employee>();
        }

        // This function is responsible for creating a new employee record.
        public void CreateEmployee()
        {
            try
            {
                Console.Clear();
                employeeCounter++;
                Employees.Add(new Employee
                {
                    EmployeeId = employeeCounter.ToString(),
                    FullName = GetFullName(),
                    BirthDate = GetBirthDate()
                });

                Console.Clear();

                Employee employee = Employees.Where(o => o.EmployeeId.Equals(employeeCounter.ToString())).FirstOrDefault();
                if (employee != null)
                {
                    Output.Message("Successfully created a new employee:", Message.Type.Success);
                    ShowAllEmployees();
                }
                else
                {
                    Output.Message("Failed to create new employee", Message.Type.Error);
                    employeeCounter--;
                }
            }
            catch (Exception ex)
            {
                Output.Message($"Error: {ex.Message}", Message.Type.Error);

            }
        }

        // This function is responsible for displaying a list of all employee records currently stored in Employees
        public void ListEmployee()
        {
            Console.Clear();
            Output.Message("Here is in-memory list of the created Employee(s):", Message.Type.Default);
            ShowAllEmployees();
        }

        // This function retrieves all employee records from Employees and output them as a table
        public void ShowAllEmployees()
        {
            Table table = new Table("EmployeeId", "FullName", "BirthDate");
            Employees.ForEach(employee => table.AddRow(employee.EmployeeId, employee.FullName, employee.BirthDate.ToString("dd-MMM-yy")));
            table.Config = TableConfiguration.UnicodeAlt();
            Output.Message(table.ToString(), Message.Type.Info);
        }

        // This function is responsible for deleting an employee record from Employees
        public void DeleteEmployee()
        {
            Console.Clear();
            try
            {

                ShowAllEmployees();
                Output.Message("\nEnter which EmployeeId (based on the data) you want to delete: ", Message.Type.Info);

                Employee employee = GetEmployee();

                if (employee != null)
                {
                    bool remove = Employees.Remove(employee);
                    if (remove)
                    {
                        Console.Clear();
                        Output.Message($"Successfully remove/delete employee with name {employee.FullName}.", Message.Type.Success);
                        ShowAllEmployees();
                    }
                    else
                    {
                        Output.Message("Failed to remove/delete employee. Please try again.", Message.Type.Warning);
                    }
                }
                else
                {
                    Output.Message($"Employee doesn't exist. Please try again.", Message.Type.Warning);
                }
            }
            catch (Exception ex)
            {
                Output.Message($"Error: {ex.Message}", Message.Type.Error);
            }
        }

        // This function is responsible for updating an existing employee record in Employees
        public void UpdateEmployee()
        {
            Console.Clear();
            try
            {
                ShowAllEmployees();
                Output.Message("\nEnter which EmployeeId (based on the data) you want to update: ", Message.Type.Default);

                Employee employee = GetEmployee();

                if (employee != null)
                {
                    employee.FullName = GetFullName();
                    employee.BirthDate = GetBirthDate();

                    Console.Clear();
                    Output.Message($"Successfully update employee with name {employee.FullName}.", Message.Type.Success);
                    ShowAllEmployees();
                }
                else
                {
                    Output.Message($"Employee doesn't exist. Please try again.", Message.Type.Warning);
                }
            }
            catch (Exception ex)
            {
                Output.Message($"Error: {ex.Message}", Message.Type.Error);
            }
        }

        // This function is responsible for retrieving a single employee record from Employees
        // And will be used in other function such as delete and update.
        public Employee GetEmployee()
        {
            Employee employee = new Employee();
            string EmployeeId = string.Empty;
            bool IsValidEmployeeId;
            bool IsAnyEmployee;

            do
            {
                IsValidEmployeeId = false;
                IsAnyEmployee = false;

                EmployeeId = Console.ReadLine();

                if (string.IsNullOrEmpty(EmployeeId))
                {
                    Output.Message("EmployeeId must be filled. Please try again.", Message.Type.Warning);
                }
                else
                {
                    IsValidEmployeeId = int.TryParse(EmployeeId, CultureInfo.InvariantCulture, out int EmpId);
                    if (!IsValidEmployeeId)
                    {
                        Output.Message("EmployeeId is invalid. Please try again.", Message.Type.Warning);
                    }
                    else
                    {
                        IsAnyEmployee = Employees.Any(o => o.EmployeeId.Equals(EmployeeId.ToString()));
                        if (IsAnyEmployee)
                        {
                            employee = Employees.FirstOrDefault(o => o.EmployeeId.Equals(EmployeeId.ToString()));
                        }
                        else
                        {
                            Output.Message($"Employee doesn't exist with id {EmployeeId}. Please try again.", Message.Type.Warning);
                        }
                    }
                }
            } while (string.IsNullOrEmpty(EmployeeId) || !IsValidEmployeeId || !IsAnyEmployee);

            return employee;
        }

        // This function is responsible for reading and obtaining a birth date from user input
        // and validating it as a valid date time (dd/MM/yyyy)
        public DateTime GetBirthDate()
        {
            string strBirthDate = string.Empty;
            DateTime birthDate = DateTime.MinValue;
            bool isValidDate = false;

            Output.Message("Enter birth date (dd/MM/yyyy): ", Message.Type.Default);
            do
            {
                strBirthDate = Console.ReadLine();
                if (string.IsNullOrEmpty(strBirthDate))
                {
                    Output.Message("Birth date must be filled. Please enter date in (dd/MM/yyyy) format.", Message.Type.Default);
                }
                else
                {
                    isValidDate = DateTime.TryParseExact(strBirthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
                    if (!isValidDate)
                    {
                        Output.Message("Invalid date format. Please enter date in (dd/MM/yyyy) format.", Message.Type.Warning);
                    }
                }
            } while (string.IsNullOrEmpty(strBirthDate) || !isValidDate);
            return birthDate;
        }

        // This function is responsible for reading and obtaining the user's input for a full name
        public string GetFullName()
        {
            string fullName = string.Empty;
            bool IsExist = false;

            Output.Message("Enter full name: ", Message.Type.Default);
            do
            {
                IsExist = false;
                fullName = Console.ReadLine();
                if (string.IsNullOrEmpty(fullName))
                {
                    Output.Message("Full name must be filled. Please try again.", Message.Type.Warning);
                }
                else if (IsDuplicate(fullName))
                {
                    IsExist = true;
                    Output.Message("Full name already registered. Please try again.", Message.Type.Warning);
                }
            } while (string.IsNullOrEmpty(fullName) || IsExist);
            return fullName;
        }

        // This function is responsible for checking for duplicate employee records based on the full name parameter
        public bool IsDuplicate(string fullName)
        {
            return Employees.Any(e => e.FullName.ToLower() == fullName.ToLower());
        }
    }
}
