using System.Windows;
using System.Windows.Input;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.View;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel
{
    class MainViewModel
    {
        public RelayCommand EmployeeCommand { get; }
        public RelayCommand RoleCommand { get; }

        public MainViewModel()
        {
            EmployeeCommand = new RelayCommand(_ => ShowEmployeeWindow());
            RoleCommand = new RelayCommand(_ => ShowRoleWindow());
        }

        private void ShowEmployeeWindow()
        {
            WindowEmployee wEmployee = new WindowEmployee();
            wEmployee.Show();
        }

        private void ShowRoleWindow()
        {
            WindowRole wRole = new WindowRole();
            wRole.Show();
        }
    }
}