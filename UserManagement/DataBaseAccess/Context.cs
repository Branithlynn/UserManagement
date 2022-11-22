
using ProjectManager.Entity;
using System.Data.Entity;

namespace ProjectManager.DataBaseAccess
{
    public class Context :DbContext
    {

        public DbSet<User> Users { get; set; }

        public Context() : base("Server = localhost\\sqlexpress; Database=UserManagement;Trusted_Connection=True;")
        {
            Users = this.Set<User>();
        }
    }
}
