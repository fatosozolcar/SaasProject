using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Common.Models.Auth
{
    public class UserAuthForgotPasswordDto
    {
        public string Email { get; set; }

        public UserAuthForgotPasswordDto(string email)
        {
            Email = email;
        }
    }
}