using Lab4_rab1_GorbatovZ.Z_BPI_23_01.Model;

namespace Lab4_rab1_GorbatovZ.Z_BPI_23_01.Helper
{
    public class FindRole
    {
        private int _id;

        public FindRole(int id)
        {
            _id = id;
        }

        public bool RolePredicate(Role role)
        {
            return role.Id == _id;
        }
    }
}
