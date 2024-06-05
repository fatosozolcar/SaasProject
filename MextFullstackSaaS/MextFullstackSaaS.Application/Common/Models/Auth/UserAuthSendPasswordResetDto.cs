using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Common.Models.Auth
{
     public class UserAuthSendPasswordResetDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string ResetToken { get; set; }

        public UserAuthSendPasswordResetDto(string email, string firstName, string resetToken)
        {
            Email = email;
            FirstName = firstName;
            ResetToken = resetToken;
        }
    }
}