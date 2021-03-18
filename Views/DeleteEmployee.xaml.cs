using Employee_Directory_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Employee_Directory_Library;
using Employee_Directory_WPF.Validation;
using FluentValidation.Results;

namespace Employee_Directory_WPF.Views
{
    public partial class DeleteEmployee : Window
    {
        public List<Employee> employees = SqliteDataAccess.LoadEmployees();  //Create a list of Employee objects from the SQLite database
        private Employee employee = new Employee(); //Create generic Employee object to store temporary values before sending to database

        public DeleteEmployee()
        {
            InitializeComponent();
            EmployeeNames.ItemsSource = employees;  //Populate the dropdown menu with Employees from the created list
        }

        private void EmployeeNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employee = (Employee) EmployeeNames.SelectedItem;  //Stores Employee properties in temporary employee based on user selection in dropdown menu
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeValidator validator = new EmployeeValidator();

            var results = validator.Validate(employee);  //Check to see if the user selected an employee from the list before allowing to delete
            if (results.IsValid == false)
            {
                string messageBox = "";
                int count = 0;
                foreach (ValidationFailure failure in results.Errors)
                {
                    count++;
                }

                if (count > 0)
                {
                    MessageBox.Show(this,"Please select an employee to delete.");
                }
                

            }
            //errors.Clear();
            if (results.IsValid == true) //Sends confirmation message to user prior to deleting Employee from database.  
            {
                MessageBoxResult result = MessageBox.Show(this,$"Are you sure you want to delete {employee.FullName}? \nWARNING, THIS ACTION CANNOT BE UNDONE", "Confirm Delete", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            SqliteDataAccess.DeleteEmployee(employee);
                            MessageBox.Show($"{employee.FullName} has been deleted.");
                            Close();
                            break;
                        case MessageBoxResult.No:
                            Close();
                            break;
                    }
            }

        }
    }
}
