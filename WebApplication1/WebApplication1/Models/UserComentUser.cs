using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class UserComentUser
{
    public int Id { get; set; }

    public int? IdUserComent { get; set; }

    public int? IdUser { get; set; }
}
