using FHP_DL;
using FHP_Res;
using FHP_Res.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHP_VO
{
    public class VO
    {
        public UserModel signinUser;
        public List<Trainee> TraineeList { get; set; }
        public bool IsDelete { get; set; } = false;
        public VOResource.EditMode EditMode { get; set; } = VOResource.EditMode.Add;
    }
}
