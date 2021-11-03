using App.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
