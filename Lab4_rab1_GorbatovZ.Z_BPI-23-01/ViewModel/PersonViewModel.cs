using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;
using Lab4_rab1_GorbatovZ.Z_BPI_23_01.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Person> ListPerson { get; set; } = new ObservableCollection<Person>();
        public ObservableCollection<PersonDPO> ListPersonDpo { get; set; } = new ObservableCollection<PersonDPO>();

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
            ListPerson.Add(new Person(1, 1, "Иван", "Иванов", new DateTime(1980, 02, 28)));
            ListPerson.Add(new Person(2, 2, "Петр", "Петров", new DateTime(1981, 03, 20)));
            ListPerson.Add(new Person(3, 3, "Виктор", "Викторов", new DateTime(1982, 04, 15)));
            ListPerson.Add(new Person(4, 3, "Сидор", "Сидоров", new DateTime(1983, 05, 10)));
            ListPersonDpo = GetListPersonDpo();
        }

        public ObservableCollection<PersonDPO> GetListPersonDpo()
        {
            ListPersonDpo.Clear();
            foreach (var person in ListPerson)
            {
                var p = new PersonDPO();
                p = p.CopyFromPerson(person);
                ListPersonDpo.Add(p);
            }
            return ListPersonDpo;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in ListPerson)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                }
            }
            return max;
        }

        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                if (addPerson == null)
                {
                    addPerson = new RelayCommand(obj =>
                    {
                        var maxIdPerson = MaxId() + 1;
                        var newPersonDpo = new PersonDPO
                        {
                            Id = maxIdPerson,
                            Birthday = DateTime.Now
                        };

                        var roleVm = new RoleViewModel();
                        var window = new WindowNewEmployee();
                        var vm = new PersonEditViewModel(newPersonDpo, roleVm.ListRole, window);
                        window.DataContext = vm;
                        window.Title = "Новый сотрудник";

                        if (window.ShowDialog() == true)
                        {
                            var roleId = 1;
                            foreach (var role in roleVm.ListRole)
                            {
                                if (role.NameRole == newPersonDpo.RoleName)
                                {
                                    roleId = role.Id;
                                    break;
                                }
                            }

                            ListPersonDpo.Add(newPersonDpo);
                            ListPerson.Add(new Person
                            {
                                Id = newPersonDpo.Id,
                                FirstName = newPersonDpo.FirstName,
                                LastName = newPersonDpo.LastName,
                                Birthday = newPersonDpo.Birthday,
                                RoleId = roleId
                            });
                        }
                    }, _ => true);
                }
                return addPerson;
            }
        }

        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                if (editPerson == null)
                {
                    editPerson = new RelayCommand(obj =>
                    {
                        if (SelectedPersonDpo == null) return;

                        var original = SelectedPersonDpo;
                        var temp = original.ShallowCopy();

                        var roleVm = new RoleViewModel();
                        var window = new WindowNewEmployee();
                        var vm = new PersonEditViewModel(temp, roleVm.ListRole, window);
                        window.DataContext = vm;
                        window.Title = "Редактирование данных сотрудника";

                        if (window.ShowDialog() == true)
                        {
                            original.FirstName = temp.FirstName;
                            original.LastName = temp.LastName;
                            original.Birthday = temp.Birthday;
                            original.RoleName = temp.RoleName;

                            foreach (var person in ListPerson)
                            {
                                if (person.Id == original.Id)
                                {
                                    var roleId = 1;
                                    foreach (var role in roleVm.ListRole)
                                    {
                                        if (role.NameRole == temp.RoleName)
                                        {
                                            roleId = role.Id;
                                            break;
                                        }
                                    }
                                    person.FirstName = temp.FirstName;
                                    person.LastName = temp.LastName;
                                    person.Birthday = temp.Birthday;
                                    person.RoleId = roleId;
                                    break;
                                }
                            }
                        }
                    }, _ => SelectedPersonDpo != null && ListPersonDpo.Count > 0);
                }
                return editPerson;
            }
        }

        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                if (deletePerson == null)
                {
                    deletePerson = new RelayCommand(obj =>
                    {
                        var person = SelectedPersonDpo;
                        if (person == null) return;

                        var result = MessageBox.Show(
                            $"Удалить данные по сотруднику:\n{person.LastName} {person.FirstName}",
                            "Предупреждение",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.OK)
                        {
                            ListPersonDpo.Remove(person);


                            for (int i = ListPerson.Count - 1; i >= 0; i--)
                            {
                                if (ListPerson[i].Id == person.Id)
                                {
                                    ListPerson.RemoveAt(i);
                                    break;
                                }
                            }

                            SelectedPersonDpo = null;
                        }
                    }, _ => SelectedPersonDpo != null && ListPersonDpo.Count > 0);
                }
                return deletePerson;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}