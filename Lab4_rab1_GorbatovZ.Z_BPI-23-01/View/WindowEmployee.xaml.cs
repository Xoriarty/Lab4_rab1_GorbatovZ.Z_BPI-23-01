using Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel;
using System.Windows;
namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.View
{
    public partial class WindowEmployee : Window
    {
        public WindowEmployee()
        {
            InitializeComponent();
            DataContext = new PersonViewModel();
        }
    }
}
