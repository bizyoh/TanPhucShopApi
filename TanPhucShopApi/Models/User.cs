﻿using Microsoft.AspNetCore.Identity;

namespace TanPhucShopApi.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string RefreshToken { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<Invoice> Invoices { get; set; }

    }
}
