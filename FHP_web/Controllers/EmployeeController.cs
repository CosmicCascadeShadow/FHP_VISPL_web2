using FHP_DL;
using FHP_Res.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http; // namespace for session
namespace FHP_web.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpPost]
        public ActionResult Validate([FromBody] UserModel user)
        {
            UserRepository repository = new UserRepository();
            List<UserModel> allUser = repository.GetAllUsers();
            UserModel? loggedInUser = allUser.Where(u => u.Id == user.Id && u.Password == user.Password).FirstOrDefault();
            if (loggedInUser == null)
            {
                return Json(" "); // not valid user
            }
            else
            {
                HttpContext.Session.SetString("loginUser", loggedInUser.Id.ToString());
                return Json("valid user"); // valid user
            }
        }
        // ----------------- function to return the first trainee
        public Trainee GetFirstTrainee()
        {
            TraineeRepository traineeRepository = new TraineeRepository();
            return traineeRepository.GetAllTrainee()[0];
        }
        // ----------------- function to return the last trainee
        public Trainee GetLastTrainee()
        {
            TraineeRepository traineeRepository = new TraineeRepository();
            List<Trainee> trainees = traineeRepository.GetAllTrainee();
            return trainees[trainees.Count - 1];
        }
        // ----------------- function to return the previous trainee
        public Trainee GetPreviousTrainee(int id)
        {
            TraineeRepository traineeRepository = new TraineeRepository();
            List<Trainee> trainees = traineeRepository.GetAllTrainee().Where(t => t.SerialNumber < id).ToList();
            if (trainees.Count == 0)
            {
                return null;
            }
            else
            {
                return trainees[trainees.Count - 1];
            }
        }
        //----------------- function to return the next trainee
        public Trainee GetNextTrainee(int id)
        {
            TraineeRepository traineeRepository = new TraineeRepository();
            List<Trainee> trainees = traineeRepository.GetAllTrainee().Where(t => t.SerialNumber > id).ToList();
            if (trainees.Count == 0)
            {
                return null;
            }
            else
            {
                return trainees.FirstOrDefault();
            }
        }

        public class ValidationResponse
        {
            public UserModel LoggedInUser { get; set; }
            public List<FHP_Res.Entity.Trainee> Trainees { get; set; }
        }
    }
}
