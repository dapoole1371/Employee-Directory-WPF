using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Employee_Directory_WPF.Models;


namespace Employee_Directory_Library
{
    public class SqliteDataAccess
    {
        public static List<Employee> LoadEmployees()  //Access the SQLite database to populate a list of employees from the data table Employees
        {
            using (IDbConnection conn = new SQLiteConnection("data source= ./Data/EmployeeDirectory.db"))
            {
                var output = conn.Query<Employee>("select * from Employees", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void NewEmployee(Employee employee) //Access the SQLite database to add a new employee to the Employees table
        {
            using (IDbConnection conn = new SQLiteConnection("data source= ./Data/EmployeeDirectory.db"))
            {
                conn.Execute("insert into Employees (FirstName, LastName, JobTitle) values (@FirstName, @LastName, @JobTitle)", employee);
            }
        }

        public static void EditEmployee(Employee employee) //Access the SQLite database to edit an employee in the Employees table
        {
            using (IDbConnection conn = new SQLiteConnection("data source= ./Data/EmployeeDirectory.db"))
            {
                conn.Execute($"update Employees set FirstName = @FirstName, LastName = @LastName, JobTitle = @JobTitle where ID = @ID", employee);
            }
        }

        public static void DeleteEmployee(Employee employee)  //Access the SQLite database to delete an employee from the Employees table
        {
            using (IDbConnection conn = new SQLiteConnection("data source= ./Data/EmployeeDirectory.db"))
            {
                conn.Execute($"delete from Employees where ID = {employee.ID}", employee);
            }
        }
    }
}
