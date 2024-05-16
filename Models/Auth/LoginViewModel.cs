using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.Auth;

    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required!")]
    public string Username { get; set; } = default!;

    [Required(ErrorMessage ="Password is required!")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
    }
