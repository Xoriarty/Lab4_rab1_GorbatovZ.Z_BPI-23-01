using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                //EditRole.CanExecute(true);
            }
        }
        public ObservableCollection<Role> ListRole
        {
            get => _listRole;
            set
            {
                _listRole = value;
                OnPropertyChanged();
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}