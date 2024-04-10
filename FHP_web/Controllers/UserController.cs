using FHP_DL;
using FHP_Res.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FHP_web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            List<UserModel> allUsers = _userRepository.GetAllUsers().ToList();
            return View(allUsers);
        }
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserModel user =  _userRepository.GetAllUsers().Where(u => u.Id == id).FirstOrDefault();
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<SelectListItem> roles = _userRepository.GetAllRoles().Select(
               item => new SelectListItem
               {
                   Text = item.Description,
                   Value = item.Id.ToString()
               });
                ViewBag.AllRoles = roles;
                return View(user);
            }
        }
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            _userRepository.Update(user);
            return RedirectToAction("index");
        }
        public IActionResult Delete(string? id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
