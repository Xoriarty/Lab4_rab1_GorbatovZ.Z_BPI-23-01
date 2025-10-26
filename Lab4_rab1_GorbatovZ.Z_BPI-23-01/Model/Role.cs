using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model
{
    public class Role : INotifyPropertyChanged
    {

        public int Id { get; set; }
        private string nameRole;
        public string NameRole {
            get { return nameRole; }
            set
            {
                nameRole = value;
                OnPropertyChanged(nameof(NameRole));
            }
        }

        public Role() { }

        public Role(int id, string nameRole)
        {
            this.Id = id;
            this.NameRole = nameRole;
        }
        public Role ShallCopy()
        {
            return (Role) this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}