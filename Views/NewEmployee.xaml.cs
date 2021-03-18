using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Employee_Directory_WPF
{
    public partial class NewEmployee : Window
    {
        public NewEmployee()
        {
            InitializeComponent();
        }

        private void AddEmpButton_Click(object sender, RoutedEventArgs e)
        {
            
            Employee emp = new Employee();
            emp.FirstName = FirstNameIn.Text;
            emp.LastName = LastNameIn.Text;
            emp.JobTitle = JobTitleIn.Text;

            EmployeeValidator validator = new EmployeeValidator();
            
            var results = validator.Validate(emp);
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
                SqliteDataAccess.NewEmployee(emp);
                MessageBox.Show($"{emp.FullName} has been added\nto your directory!");
                Close();
            }
        }
    }
}

