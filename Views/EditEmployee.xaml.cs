using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Employee_Directory_WPF.Models;
using Employee_Directory_WPF.Validation;
using FluentValidation.Results;

namespace Employee_Directory_WPF.Views
{
    public partial class EditEmployee : Window
    {
        public List<Employee> employees = SqliteDataAccess.LoadEmployees();  //Create a list of employees from the SQLite database attached to application
        private Employee employee = new Employee();  //Creates a temporary employee object to hold values
        public EditEmployee()
        {
            InitializeComponent();
            EmployeeNames.ItemsSource = employees;  //Identifies the source of items to display in the dropdown menu
        }

        private void EmployeeNames_SelectionChanged(object sender, SelectionChangedEventArgs e)  //When a user selects a name from dropdown, the generic object from the dropdown is cast as an Employee object and then its values are saved into the temporary employee object.
        {
            employee = (Employee) EmployeeNames.SelectedItem;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)  //When user clicks Edit button
        {
            EmployeeExists exists = new EmployeeExists();
            
            //Check to see if an employee has been selected from the dropdown menu to edit
            var items = exists.Validate(employee);
            if (items.IsValid == false)  //Code when no employee has been selected
            {
                string messageBox = "";
                int count = 0;
                foreach (ValidationFailure failure in items.Errors)
                {
                    count++;
                }

                if (count > 0)
                {
                    MessageBox.Show("Please select an employee to edit!");
                }
            }

            if (items.IsValid == true) //If employee selected, replaces all properties of the temporary employee with the user inputs from the text boxes.
            {
                EmployeeValidator validator = new EmployeeValidator();
                employee.FirstName = FirstNameIn.Text;
                employee.LastName = LastNameIn.Text;
                employee.JobTitle = JobTitleIn.Text;
                var results = validator.Validate(employee);
                if (results.IsValid == false)
                {
                    string messageBox = "";
                    foreach (ValidationFailure failure in results.Errors)
                    {
                        messageBox += $"{failure.ErrorMessage}\n";
                    }

                    MessageBox.Show($"{messageBox}");
                }
                if (results.IsValid == true)
                {
                    SqliteDataAccess.EditEmployee(employee);  //Open connection string to database and modify employee data where ID matches the selected employee from the dropdown menu.
                    MessageBox.Show($"{employee.FullName} has been edited successfully!");
                    Close();
                }
            }
            items.Errors.Clear();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
