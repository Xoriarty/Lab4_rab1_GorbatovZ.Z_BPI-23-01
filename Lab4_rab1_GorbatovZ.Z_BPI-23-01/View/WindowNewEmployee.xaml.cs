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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Некорректно заполненные поля", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
