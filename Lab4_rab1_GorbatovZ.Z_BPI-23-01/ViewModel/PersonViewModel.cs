using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Person> ListPerson { get; set; } = new ObservableCollection<Person>();
        public ObservableCollection<PersonDPO> ListPersonDpo { get; set;} = new ObservableCollection<PersonDPO>();
        private PersonDPO selectedPersonDpo;
        public PersonDPO SelectedPersonDpo
        {
            get { return selectedPersonDpo; }
            set
            {
                selectedPersonDpo = value;
                OnPropertyChanged(nameof(SelectedPersonDpo));
            }
        }
        public ObservableCollection<PersonDPO> Persons
        {
            get => ListPersonDpo;
            set
            {
                ListPersonDpo = value;
                OnPropertyChanged(nameof(Persons));
            }
        }

        public PersonViewModel()
        {
            this.ListPerson.Add(new Person(1, 1, "Иван", "Иванов", new DateTime(1980, 02, 28)));
            this.ListPerson.Add(new Person(2, 2, "Петр", "Петров", new DateTime(1981, 03, 20)));
            this.ListPerson.Add(new Person(3, 3, "Виктор", "Викторов", new DateTime(1982, 04, 15)));
            this.ListPerson.Add(new Person(4, 3, "Сидор", "Сидоров", new DateTime(1983, 05, 10)));
            ListPersonDpo = GetListPersonDpo();
        }
        public ObservableCollection<PersonDPO> GetListPersonDpo()
        {
            foreach (var person in ListPerson)
            {
                PersonDPO p = new PersonDPO();
                p = p.CopyFromPerson(person);
                ListPersonDpo.Add(p);
            }
            return ListPersonDpo;
        }
        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListPerson)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                }
                ;
            }
            return max;
        }
        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return addPerson ??
                (addPerson = new RelayCommand(obj =>
                {
                WindowNewEmployee wnPerson = new WindowNewEmployee
                {
                    Title = "Новый сотрудник"
                };

                int maxIdPerson = MaxId() + 1;
                PersonDPO per = new PersonDPO
                {
                    Id = maxIdPerson,
                    Birthday = DateTime.Now
                };
                    wnPerson.DataContext = per;
                    if (wnPerson.ShowDialog() == true)
                    {
                        Role r = (Role)wnPerson.CbRole.SelectedItem;
                        per.RoleName = r.NameRole;
                        ListPersonDpo.Add(per);
                        Person p = new Person();
                        p = p.CopyFromPersonDPO(per);
                        ListPerson.Add(p);
                    }
                }, (obj) => true));
            }
        }
        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                return editPerson ??
                (editPerson = new RelayCommand(obj =>
                {
                WindowNewEmployee wnPerson = new WindowNewEmployee()
                {
                    Title = "Редактирование данных сотрудника",
                };
                PersonDPO personDpo = SelectedPersonDpo;
                PersonDPO tempPerson = new PersonDPO();
                tempPerson = personDpo.ShallowCopy();
                wnPerson.DataContext = tempPerson;
                if (wnPerson.ShowDialog() == true)
                {
                    Role r = (Role)wnPerson.CbRole.SelectedItem;
                        personDpo.RoleName = r.NameRole;
                    personDpo.FirstName = tempPerson.FirstName;
                    personDpo.LastName = tempPerson.LastName;
                    personDpo.Birthday = tempPerson.Birthday;

                    FindPerson finder = new FindPerson(personDpo.Id);
                    List<Person> listPerson = ListPerson.ToList();
                    Person p = listPerson.Find(new Predicate<Person > (finder.PersonPredicate));
                    p = p.CopyFromPersonDPO(personDpo);
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }
        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                return deletePerson ??
                (deletePerson = new RelayCommand(obj =>
                {
                    PersonDPO person = SelectedPersonDpo;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по сотруднику: \n" + person.LastName + " " + person.FirstName,"Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListPersonDpo.Remove(person);
                        Person per = new Person();
                        per = per.CopyFromPersonDPO(person);
                        ListPerson.Remove(per);
                    }
                }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}