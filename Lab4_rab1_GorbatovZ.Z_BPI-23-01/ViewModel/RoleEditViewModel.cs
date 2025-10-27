using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel
{
    public class RoleEditViewModel : INotifyPropertyChanged, IClosable
    {
        private readonly Role _role;
        private readonly Window _window;

        public Role Role => _role;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public RoleEditViewModel(Role role, Window window)
        {
            _role = role;
            _window = window;

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(_role.NameRole))
            {
                MessageBox.Show("Введите наименование должности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        public int Id
        {
            get => _role.Id;
            set => _role.Id = value;
        }

        public string NameRole
        {
            get => _role.NameRole;
            set
            {
                _role.NameRole = value;
                OnPropertyChanged();
            }
        }
    }
}