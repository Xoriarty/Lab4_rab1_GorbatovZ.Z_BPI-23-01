using Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel;
using System.Windows;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.View
{

    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee()
        {
            InitializeComponent();
            var vmRole = new RoleViewModel();
            CbRole.ItemsSource = vmRole.ListRole;
        }
    }
}
