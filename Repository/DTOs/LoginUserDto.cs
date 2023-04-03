using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs
{
    public class LoginUserDto

    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        //                       @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        //
        //           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public string Password { get; set; }
    }
}
