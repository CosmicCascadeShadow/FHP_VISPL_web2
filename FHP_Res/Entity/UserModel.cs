using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHP_Res.Entity
{
    public class UserModel
    {
        [Required]
        public String Id { get; set; }

        [Required]
        public String Password { get; set; }

        public byte Role {  get; set; }
    }
}
