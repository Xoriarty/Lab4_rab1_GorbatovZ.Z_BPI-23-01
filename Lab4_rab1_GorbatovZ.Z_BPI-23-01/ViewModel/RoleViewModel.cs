using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Role> _listRole;

        private Role selectedRole;
        public Role SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
                EditRole.CanExecute(true);
            }
        }
        public ObservableCollection<Role> ListRole
        {
            get => _listRole;
            set
            {
                _listRole = value;
                OnPropertyChanged(nameof(ListRole));
            }
        }

        public RoleViewModel()
        {
            ListRole = new ObservableCollection<Role>
            {
                new Role { Id = 1, NameRole = "Директор" },
                new Role { Id = 2, NameRole = "Бухгалтер" },
                new Role { Id = 3, NameRole = "Менеджер" }
            };
        }
        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListRole)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                }
                ;
            }
            return max;
        }

        private RelayCommand addRole;
        public RelayCommand AddRole
        {
            get
            {
                if (addRole == null)
                {
                    addRole = new RelayCommand(obj =>
                    {
                        var newRole = new Role { Id = MaxId() + 1 };
                        var window = new WindowNewRole();
                        var vm = new RoleEditViewModel(newRole, window);
                        window.DataContext = vm;

                        if (window.ShowDialog() == true)
                        {
                            ListRole.Add(newRole);
                            SelectedRole = newRole;
                        }
                    });
                }
                return addRole;
            }
        }
        private RelayCommand editRole;
        public RelayCommand EditRole
        {
            get
            {
                if (editRole == null)
                {
                    editRole = new RelayCommand(obj =>
                    {
                        if (SelectedRole == null) return;

                        var originalRole = SelectedRole;
                        var tempRole = originalRole.ShallowCopy();
                        var window = new WindowNewRole();
                        var vm = new RoleEditViewModel(tempRole, window);
                        window.DataContext = vm;

                        if (window.ShowDialog() == true)
                        {
                            originalRole.NameRole = tempRole.NameRole;
                        }
                    }, _ => SelectedRole != null && ListRole.Count > 0);
                }
                return editRole;
            }
        }

        private RelayCommand deleteRole;
        public RelayCommand DeleteRole
        {
            get
            {
                return deleteRole ??
                (deleteRole = new RelayCommand(obj =>
                {
                    Role role = SelectedRole;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по должности: " + role.NameRole, "Предупреждение", MessageBoxButton.OKCancel,MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListRole.Remove(role);
                    }
                }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}