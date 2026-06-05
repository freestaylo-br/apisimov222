using System;
using System.Collections.Generic;

namespace apisimov222.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public int RoleId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
