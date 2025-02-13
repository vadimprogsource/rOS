using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rOS.Uac.Wapi.Models;

public record NewUserModel
{
    public string? Login { get; set; } 
    public string? Email { get; set; }
    public string? Cellular { get; set; }

    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}
