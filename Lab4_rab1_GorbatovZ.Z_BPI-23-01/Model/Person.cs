using Lab4_rab1_GorbatovZ.Z_BPI_23_01.ViewModel;
using System;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model
{
    public class Person
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public Person() { }

        public Person(int id, int roleId, string firstName, string lastName, DateTime birthday)
        {
            this.Id = id;
            this.RoleId = roleId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
        }
        public Person CopyFromPersonDPO(PersonDPO personDpo)
        {
            Person person = new Person();
            person.Id = personDpo.Id;
            person.FirstName = personDpo.FirstName;
            person.LastName = personDpo.LastName;
            person.Birthday = personDpo.Birthday;

            RoleViewModel vmRole = new RoleViewModel();
            foreach (var r in vmRole.ListRole)
            {
                if (r.NameRole == personDpo.RoleName)
                {
                    person.RoleId = r.Id;
                    break;
                }
            }

            return person;
        }
    }
}