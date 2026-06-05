using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnisimovApp.Models
{
    public class LoginResponse
    {
        public int StaffId { get; set; }
        public string Surname { get; set; } = "";
        public string Name { get; set; } = "";
        public string Patronymic { get; set; } = "";
        public string Login { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
