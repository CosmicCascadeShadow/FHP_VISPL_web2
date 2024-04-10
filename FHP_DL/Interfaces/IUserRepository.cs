using FHP_Res.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHP_DL
{
    public interface IUserRepository
    {
        bool Add(UserModel user);
        List<UserModel> GetAllUsers();
        List<Role> GetAllRoles();
        bool Update(UserModel user);
        bool Delete(string id);
    }
}
