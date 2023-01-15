using SimpleEmployeeApp.Helpers.Model;
using System.Globalization;
using BetterConsoleTables;

namespace SimpleEmployeeApp.Helpers.Service
{
    public class Output
    {
        public static void Message(string message, Model.Message.Type type)
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

    public class EmployeeService
    {
        int employeeCounter = 1000;

        List<Employee> Employees = new List<Employee>();

        public EmployeeService()
        {
            Employees = new List<Employee>();
        }

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

        public void ListEmployee()
        {
            Console.Clear();
            Output.Message("Here is in-memory list of the created Employee(s):", Message.Type.Default);
            ShowAllEmployees();
        }

        public void ShowAllEmployees()
        {
            Table table = new Table("EmployeeId", "FullName", "BirthDate");
            Employees.ForEach(employee => table.AddRow(employee.EmployeeId, employee.FullName, employee.BirthDate.ToString("dd-MMM-yy")));
            table.Config = TableConfiguration.UnicodeAlt();
            Output.Message(table.ToString(), Message.Type.Info);
        }

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

        public bool IsDuplicate(string fullName)
        {
            return Employees.Any(e => e.FullName.ToLower() == fullName.ToLower());
        }
    }
}
