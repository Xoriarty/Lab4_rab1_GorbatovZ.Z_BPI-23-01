using System.Windows;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.View
{
    /// <summary>
    /// Логика взаимодействия для WindowNewRole.xaml
    /// </summary>
    public partial class WindowNewRole : Window
    {
        public WindowNewRole()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNameRole.Text))
            {
                MessageBox.Show("Введите наименование должности!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNameRole.Focus();
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
