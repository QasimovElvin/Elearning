using System.ComponentModel.DataAnnotations;

namespace Elearning.ViewModels.Account;

public class LoginVM
{
    public string EmailOrUsername { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
