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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Employee_Directory_WPF.Views;

namespace Employee_Directory_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Have a great day!");
            Environment.Exit(0);
        }

        private void NewEmployee_Click(object sender, RoutedEventArgs e)
        {
            NewEmployee newEmployee = new NewEmployee();
            newEmployee.Show();
        }

        private void DisplayList_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList employees = new EmployeeList();
            employees.Show();
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            EditEmployee editEmployee = new EditEmployee();
            editEmployee.Show();
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployee deleteEmployee = new DeleteEmployee();
            deleteEmployee.Show();
        }
    }
}