using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTOs
{
    public class UserDto:LoginUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
       


    }
}
