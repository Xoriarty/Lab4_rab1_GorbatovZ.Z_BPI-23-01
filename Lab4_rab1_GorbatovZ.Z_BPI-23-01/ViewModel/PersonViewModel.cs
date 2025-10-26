using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<PersonDPO> _persons;

        public ObservableCollection<PersonDPO> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public PersonViewModel()
        {
            LoadPersons();
        }

        private void LoadPersons()
        {
            var personList = new List<Person>
            {
                new Person(1, 1, "Иван", "Иванов", new DateTime(1980, 02, 28)),
                new Person(2, 2, "Петр", "Петров", new DateTime(1981, 03, 20)),
                new Person(3, 3, "Виктор", "Викторов", new DateTime(1982, 04, 15)),
                new Person(4, 3, "Сидор", "Сидоров", new DateTime(1983, 05, 10))
            };

            var roleList = new List<Role>
            {
                new Role(1, "Директор"),
                new Role(2, "Бухгалтер"),
                new Role(3, "Менеджер")
            };

            Persons = new ObservableCollection<PersonDPO>();

            foreach (var person in personList)
            {
                var finder = new FindRole(person.RoleId);
                var role = roleList.Find(new System.Predicate<Role>(finder.RolePredicate));

                Persons.Add(new PersonDPO
                {
                    Id = person.Id,
                    RoleName = role?.NameRole ?? "Неизвестно",
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Birthday = person.Birthday
                });
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}