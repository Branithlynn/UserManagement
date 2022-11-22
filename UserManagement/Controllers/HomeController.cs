using Microsoft.AspNetCore.Mvc;
using ProjectManager.Entity;
using ProjectManager.DataBaseAccess;
using ProjectManager.Repository;
using ProjectManager.ViewModel.UserVM;
using ProjectManager.ViewModel.Shareed;

namespace ProjectManager.Controllers
{
    public class HomeController : Controller
    {
        //-------------------------------------------------------//
        //------------------DISPLAYS ALL USERS-------------------//
        public IActionResult HomePage(IndexVM model)
        {
            
                model.Pager ??= new PagerVM();
                model.Filter ??= new FilterVM();
                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                            ? 10
                                            : model.Pager.ItemsPerPage;

                model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

                var filter = model.Filter.GetFilter();

                UsersRepository usersRepository = new UsersRepository();
                model.Items = usersRepository.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
                model.Pager.PagesCount = (int)Math.Ceiling(usersRepository.UsersCount(filter) / (double)model.Pager.ItemsPerPage);

                return View(model);
          
        }
        //-------------------------------------------------------//
        //------------------ADD METHOD---------------------------//
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateVM item)
        {
            if (!ModelState.IsValid)
                return View(item);

            UsersRepository userRepo = new UsersRepository();
            User user = new User();
            user.username = item.Username;
            user.password = item.Password;
            user.firstName = item.FirstName;
            user.lastName = item.LastName;
            user.IsAdmin = item.IsAdmin;

            userRepo.AddUser(user);

           
            return RedirectToAction("HomePage", "Home");
           
        }
        //------------------------------------------------------//
        //------------------DELETING USER METHOD----------------//
        public IActionResult DeleteUser(int id)
        {
            UsersRepository userRepo = new UsersRepository();

            userRepo.DeleteUser(id);

            return RedirectToAction("HomePage", "Home");
        }
        //------------------------------------------------------//
        //------------------UPDATE USER-------------------------//
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            Context context = new Context();
            User user = context.Users.Find(id);
            EditVM item = new EditVM();

            item.ID = user.ID;
            item.Username = user.username;
            item.Password = user.password;
            item.FirstName = user.firstName;
            item.LastName = user.lastName;
            item.IsAdmin = user.IsAdmin;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdateUser(EditVM item)
        {
            UsersRepository userRepo = new UsersRepository();
            User user = new User();
            user.ID = item.ID;
            user.username = item.Username;
            user.password = item.Password;
            user.firstName = item.FirstName;
            user.lastName = item.LastName;
            user.IsAdmin = item.IsAdmin;

            userRepo.UpdateUser(user);

            return RedirectToAction("HomePage", "Home");
        }
        //------------------------------------------------------//
    }
}
