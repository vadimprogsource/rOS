using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rOS.Uac.Wapi.Models;

public class ChangePasswordModel
{
    public string NewPassword        { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;


    public bool IsValid =>  !(string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmNewPassword)) &&  string.Compare(NewPassword, ConfirmNewPassword,false) == 0;
}
