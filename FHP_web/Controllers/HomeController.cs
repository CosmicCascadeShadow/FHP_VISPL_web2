using FHP_DL;
using FHP_Res.Entity;
using FHP_VO;
using FHP_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using static FHP_web.Controllers.EmployeeController;

namespace FHP_web.Controllers
{
    public class HomeController : Controller
    {
        // ----------------- main page
        public IActionResult Index()
        {
            VO viewObject = new VO();
            if (HttpContext.Session.GetString("loginUser") != null)
            {
               
                UserRepository userRepository = new UserRepository();
                UserModel user  = userRepository.GetAllUsers().Where(u => u.Id == HttpContext.Session.GetString("loginUser")).FirstOrDefault();
                viewObject.signinUser = user;
                viewObject.TraineeList = GetAllTrainee();
                return View(viewObject);
            }
            else
            {

                return View(viewObject);
            }
        }

        // ----------------- sign in page
        public ActionResult SignInPage()
        {
            return View();
        }

        // ----------------- new user registeration
        public ActionResult RegisterationPage() // implementation remaining 
        {
            return PartialView("~/Views/Shared/_RegisterationPage.cshtml");
        }
        [HttpPost]
        public ActionResult RegisterationPage(UserModel user)
        {
            UserRepository repository = new UserRepository();
            user.Role = 3; // by default the role will be guest user
            repository.Add(user);
            TempData["success"] = "Registeration successfull, please sign in";
            return RedirectToAction("Index", "Home");
        }
        public List<FHP_Res.Entity.Trainee> GetAllTrainee()
        {
            TraineeRepository repository = new TraineeRepository();
            List<FHP_Res.Entity.Trainee> trainees = repository.GetAllTrainee();
            return trainees;
        }
        // ----------------- Add or Update page
        public ActionResult Upsert(int? id)
        {
            // ---------- we will have to pass the qualification to the view for showing on the ui
            QualificationSQLDL qualificationSQLDL = new QualificationSQLDL();
            IEnumerable<SelectListItem> qualificationList = qualificationSQLDL.GetAllQualifications().Select(
                item => new SelectListItem
                {
                    Text = item.long_name,
                    Value = item.id.ToString()
                });
            ViewBag.QualificationList = qualificationList;

            //  if id == 0, then the operation is Addition
            if (id == 0 || id == null)
            {
                // -- we will have to pass the id for populating
                TraineeRepository repository = new TraineeRepository();
                List<FHP_Res.Entity.Trainee> trainees = repository.GetAllTrainee();
                TempData["currentElementId"] = trainees[trainees.Count - 1].SerialNumber + 1;
                return View();
            }
            else
            {
                TraineeRepository repository = new TraineeRepository();
                Trainee? trainee = repository.GetAllTrainee().Where(t => t.SerialNumber == id).FirstOrDefault();
                return View(trainee);
            }
        }
        //  ----------------- Add or Update page [POST]
        [HttpPost]
        public ActionResult Upsert(Trainee trainee)
        {
            //  addding
            if (trainee.SerialNumber == 0)
            {
                TraineeRepository repository = new TraineeRepository();
                if (repository.Add(trainee))
                {
                    TempData["success"] = "Employee added successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            //  updating 
            else
            {
                TraineeRepository repository = new TraineeRepository();
                if (repository.Update(trainee))
                {
                    TempData["success"] = "Employee updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }
        // ----------------- read-only view page
        public ActionResult ReadOnlyView(int? id)
        {
            TraineeRepository traineeRepository = new TraineeRepository();
            Trainee? trainee = traineeRepository.GetAllTrainee().Where(t => t.SerialNumber == id).FirstOrDefault();
            return View(trainee);
        }

        // ----------------- adding logout
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                //  invalid delete operation
                return RedirectToAction("Index");
            }
            else
            {
                TraineeRepository traineeRepository = new TraineeRepository();
                Trainee? trainee = traineeRepository.GetAllTrainee().Where(t => t.SerialNumber == id).FirstOrDefault();
                if (trainee != null)
                {
                    traineeRepository.Delete(trainee);
                    TempData["success"] = "Employee deleted successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    // trainee with corresponding id is not found
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
