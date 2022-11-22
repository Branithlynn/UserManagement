

using ProjectManager.DataBaseAccess;
using ProjectManager.Entity;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ProjectManager.Repository
{
    public class UsersRepository
    {
        static readonly Context context;
        public static List<User> Items { get; set; }
        static UsersRepository()
        {
            context = new Context();
            Items = context.Users.ToList();
        }
        public bool UserExisting(int id)
        {
            foreach (var item in context.Users)
            {
                if (item.ID == id)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddUser(User item)
        {
            User user = new User();

                user.username = item.username;
                user.password = item.password;
                user.firstName = item.firstName;
                user.lastName = item.lastName;   
                user.IsAdmin = item.IsAdmin;   
                context.Users.Add(user);

            context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
           
            User user = context.Users.Find(id);

            context.Users.Remove(user);
            context.SaveChanges();
        }
        public void UpdateUser(User item)
        {
            User user = context.Users.Find(item.ID);

            user.username = item.username;
            user.password = item.password;   
            user.firstName = item.firstName;
            user.lastName = item.lastName;
            user.IsAdmin = item.IsAdmin;
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }
        public List<User> GetAll(Expression<Func<User, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            IQueryable<User> query = context.Users;
            
            if (filter != null)
                query = query.Where(filter);

            return query.OrderBy(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public User getByUsernameAndPassword(string username,string password)
        {
            foreach (var item in context.Users)
            {
                if (item.username == username && item.password == password)
                {
                    return item;
                }
            }
            return null;
        }
        public int UsersCount(Expression<Func<User,bool>> filter = null)
        {
            IQueryable<User> query = context.Users;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }
        
    }
}