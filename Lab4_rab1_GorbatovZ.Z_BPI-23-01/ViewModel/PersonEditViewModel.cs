using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel
{
    public class PersonEditViewModel : INotifyPropertyChanged, IClosable
    {
        private readonly PersonDPO _person;
        private readonly Window _window;
        public ObservableCollection<Role> Roles { get; }

        public PersonDPO Person => _person;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public PersonEditViewModel(PersonDPO person, ObservableCollection<Role> roles, Window window)
        {
            _person = person;
            Roles = roles;
            _window = window;

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(_person.FirstName) || string.IsNullOrWhiteSpace(_person.LastName))
            {
                MessageBox.Show("Некорректно заполненные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Close(true);
        }

        private void Cancel()
        {
            Close(false);
        }

        public void Close(bool? dialogResult)
        {
            _window.DialogResult = dialogResult;
            _window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}